using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.Attack.Area;
using _Project.Develop.Runtime.Gameplay.Features.Attack.Shoot;
using _Project.Develop.Runtime.Gameplay.Features.BounceFeature;
using _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using _Project.Develop.Runtime.Gameplay.Features.Energy;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using _Project.Develop.Runtime.Gameplay.Features.LootFeature;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
using _Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using _Project.Develop.Runtime.Gameplay.Features.StatFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Gameplay.Features.Teleport;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly CollidersRegistryService _collidersRegistryService;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
            _collidersRegistryService = _container.Resolve<CollidersRegistryService>();
        }

        public Entity CreateHero(Vector3 position, HeroConfig config, Dictionary<StatTypes, float> baseStats)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            Dictionary<StatTypes, float> modifiedStats = new(baseStats);

            //StatsEffectsList statsEffectsList = new StatsEffectsList();
            //statsEffectsList.Add(new StatsEffect(StatTypes.MoveSpeed, stat => stat * 4));
            //statsEffectsList.Add(new StatsEffect(StatTypes.MaxHealth, stat => stat * 15));

            entity
                .AddStatsEffects() //statsEffectsList)
                .AddBaseStats(baseStats)
                .AddModifiedStats(modifiedStats)
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(baseStats[StatTypes.MoveSpeed]))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddMaxHealth(new ReactiveVariable<float>(baseStats[StatTypes.MaxHealth]))
                .AddCurrentHealth(new ReactiveVariable<float>(baseStats[StatTypes.MaxHealth]))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessTime))
                .AddDeathProcessCurrentTime()
                .AddInstanShootingDirections(new InstantShootingDirectionArgs(
                    new InstantShotDirectionArgs(0, 1)))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(config.AttackProcessTime))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddStartAttackRequest()
                .AddStartAttackEvent()
                .AddEndAttackEvent()
                .AddAttackDelayTime(new ReactiveVariable<float>(config.AttackDelayTime))
                .AddAttackDelayEndEvent()
                .AddInstantAttackDamage(new ReactiveVariable<float>(baseStats[StatTypes.Damage]))
                .AddAttackCanceledEvent()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(config.AttackCooldown))
                .AddAttackCooldownCurrentTime()
                .AddInAttackCooldown();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
                .Add(new FuncCondition(() => entity.IsMoving.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false));

            ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);

            entity
                .AddSystem(new StatEffectApplierSystem())
                .AddSystem(new DamageSynchronizerSystem())
                .AddSystem(new MaxHealthSynchronizerSystem())
                .AddSystem(new MoveSpeedSynchronizerSystem())
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new AttackCancelSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessTimerSystem())
                .AddSystem(new AttackDelayEndTriggerSystem())
                .AddSystem(new DirectionsInstantShootSystem(this))
                .AddSystem(new EndAttackSystem())
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            return entity;
        }

        public Entity CreateGhost(Vector3 position, GhostConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(config.MoveSpeed))
                .AddIsMoving()
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddCurrentHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessTime))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddContactsDetectingMask(Layers.CharactersMask)
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(config.BodyContactDamage))
                .AddSpawnCurrentTime()
                .AddSpawnInitialTime(new ReactiveVariable<float>(config.SpawnProcessTime))
                .AddInSpawnProcess()
                ;

            ICompositeCondition canMove = new CompositeCondition()
                    .Add(new FuncCondition(() => entity.IsDead.Value == false))
                    .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false))
                ;

            ICompositeCondition canRotate = new CompositeCondition()
                    .Add(new FuncCondition(() => entity.IsDead.Value == false))
                    .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false))
                ;

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                    .Add(new FuncCondition(() => entity.IsDead.Value == false))
                    .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false))
                ;

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage);

            entity
                .AddSystem(new SpawnProcessTimerSystem())
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            return entity;
        }

        public Entity CreateMage(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Mage");

            entity
                .AddMaxHealth(new ReactiveVariable<float>(3000))
                .AddCurrentHealth(new ReactiveVariable<float>(30))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(2))
                .AddDeathProcessCurrentTime()
                .AddTakeDamageEvent()
                .AddTakeDamageRequest()
                //energy:
                .AddMaxEnergy(new ReactiveVariable<float>(15))
                .AddCurrentEnergy(new ReactiveVariable<float>(0))
                .AddEnergyRecoverAmount(new ReactiveVariable<float>(3))
                .AddEnergyRecoverInterval(new ReactiveVariable<float>(1))
                .AddEnergySpendEvent()
                .AddEnergySpendRequest()
                //teleport:
                .AddTeleportCostEnergy(new ReactiveVariable<float>(5))
                .AddTeleportTargetPosition()
                .AddTeleportRadius(new ReactiveVariable<float>(3))
                .AddTeleportRequested()
                .AddTeleportInProcess()
                .AddTeleportCompleted()

                //.AddInstantAttackDamage(new ReactiveVariable<float>(1))
                //.AddAreaAttackRadius(new ReactiveVariable<float>(30))
                ;

            ICompositeCondition canStartTeleport = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.TeleportCostEnergy.Value))
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDIe = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanStartTeleport(canStartTeleport)
                .AddMustDie(mustDIe)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                ;

            entity
                .AddSystem(new EnergyRecoverySystem())
                .AddSystem(new EnergySpendSystem())

                //teleport:
                .AddSystem(new TeleportInitSystem())
                .AddSystem(new TeleportEnergySpendSystem())
                .AddSystem(new InstantRigidbodyTeleportSystem())
                .AddSystem(new TeleportEndSystem())

                //.AddSystem(new InstantAreaAttackSystem(this))
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_container.Resolve<EntitiesLifeContext>()));
            ;

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateProjectile(Vector3 position, Vector3 direction, float damage, Entity owner)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Projectile");

            entity
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddMoveSpeed(new ReactiveVariable<float>(25))
                .AddIsMoving()
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationSpeed(new ReactiveVariable<float>(9999))
                .AddIsDead()
                .AddContactsDetectingMask(Layers.EnvironmentMask |
                                          Layers.CharactersMask) // чтоб в целом отслеживать коллизии со слоем
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(damage))
                .AddDeathMask(Layers.EnvironmentMask)
                .AddIsTouchDeathMask()
                .AddIsTouchAnotherTeam()
                .AddTeam(new ReactiveVariable<Teams>(owner.Team.Value))
                .AddBounceEvent()
                .AddLayerToBounceReaction(Layers.EnvironmentMask)
                .AddBounceCount(new ReactiveVariable<int>(3))
                ;

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.BounceCount.Value + 1 == 0), 0)
                .Add(new FuncCondition(() => entity.IsTouchDeathMask.Value), 5)
                .Add(new FuncCondition(() => entity.IsTouchAnotherTeam.Value), 10, LogicOperations.Or);

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                
                .AddSystem(new BounceDetectorSystem())
                .AddSystem(new ReflectMovementDirectionOnBounceSystem())
                .AddSystem(new ReflectRotationDirectionOnBounceSystem())
                .AddSystem(new BounceCountDecreaseSystem())
                
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectorSystem())
                .AddSystem(new AnotherTeamTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateContactTrigger(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/ContactTrigger");

            entity
                .AddContactsDetectingMask(Layers.CharactersMask)
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64));

            entity
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreatePullable(string prefabPath, Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, prefabPath);

            entity
                .AddIsPullable()
                .AddIsPullingProcess(new ReactiveVariable<bool>(true))
                .AddCurrentTarget(new ReactiveVariable<Entity>(null))
                .AddInSpawnProcess(new ReactiveVariable<bool>(true))
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(12))
                .AddIsMoving()
                .AddIsCollected();

            ICompositeCondition moveCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsPullingProcess.Value))
                .Add(new FuncCondition(() => entity.InSpawnProcess.Value == false));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsCollected.Value));

            entity
                .AddCanMove(moveCondition)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new GenerateMoveDirectionToTargetSystem())
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new CollectedOnNearToTargetSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
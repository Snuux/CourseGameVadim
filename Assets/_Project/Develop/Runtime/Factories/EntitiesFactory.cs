using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.Attack.Attacks;
using _Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using _Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Gameplay.Features.Sensors;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.Generated;
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
        private readonly ConfigsProviderService _configsProviderService;

        private const int BufferDefaultSize = 64;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
            _collidersRegistryService = _container.Resolve<CollidersRegistryService>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();
        }

        public Entity CreateTower(Vector3 position, TowerConfig towerConfig, float maxHealth)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, towerConfig.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(towerConfig.ID))
                .AddMaxHealth(new ReactiveVariable<float>(maxHealth))
                .AddCurrentHealth(new ReactiveVariable<float>(maxHealth))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(towerConfig.DeathProcessTime))
                .AddDeathProcessCurrentTime()
                .AddContactsDetectingMask(Layers.CharactersMask)
                .AddContactCollidersBuffer(new Buffer<Collider>(BufferDefaultSize))
                .AddContactEntitiesBuffer(new Buffer<Entity>(BufferDefaultSize))
                .AddBodyContactDamage(new ReactiveVariable<float>(towerConfig.BodyContactDamage))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent();

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage);

            entity
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

        public Entity CreateGhost(Vector3 position, GhostConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
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
                .AddAttackDamage(new ReactiveVariable<float>(config.ExplosionDamage))
                .AddAttackRadius(new ReactiveVariable<float>(config.ExplosionRadius))
                .AddTriggerRadius(new ReactiveVariable<float>(config.AttackTriggerRadius))
                .AddAttackRequested()
                .AddAttackStarted()
                .AddHasReachedActionTime()
                .AddAttackCompleted()
                .AddCurrentTarget()
                
                .AddTakeDamageRequest()
                .AddTakeDamageEvent();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0))
                .Add(new FuncCondition(() => entity.AttackCompleted.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackInstantSystem())
                .AddSystem(new AreaActionAttackSystem(this))
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new EndAttackSystem());

            return entity;
        }

        public Entity CreateArcher(Vector3 position, ArcherConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
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
                .AddAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
                .AddTriggerRadius(new ReactiveVariable<float>(config.AttackTriggerRadius * 2))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                
                //attack
                .AddAttackRequested()
                .AddAttackStarted()
                .AddAttackCooldownCurrentTime()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(config.AttackCooldown))
                .AddInAttackCooldown()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(config.AttackProcessTime))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddAttackInitialActionTime(new ReactiveVariable<float>(config.AttackDelayTime))
                .AddHasReachedActionTime()
                .AddAttackCompleted()
                .AddCurrentTarget()
                ;

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessSystem())
                .AddSystem(new ProjectileActionAttackSystem(this))
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new EndAttackSystem());

            return entity;
        }
        
        public Entity CreateTurret(Vector3 position, TurretConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
                .AddRotationDirection()
                .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))
                .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddCurrentHealth(new ReactiveVariable<float>(config.MaxHealth))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessTime))
                .AddDeathProcessCurrentTime()
                .AddAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
                .AddTriggerRadius(new ReactiveVariable<float>(config.AttackTriggerRadius * 2))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                .AddCurrentTarget()
                
                //attack
                .AddAttackRequested()
                .AddAttackStarted()
                .AddAttackCooldownCurrentTime()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(config.AttackCooldown))
                .AddInAttackCooldown()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(config.AttackProcessTime))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddAttackInitialActionTime(new ReactiveVariable<float>(config.AttackDelayTime))
                .AddHasReachedActionTime()
                .AddAttackCompleted()
                ;

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false));

            entity
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack);

            entity
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessSystem())
                .AddSystem(new ProjectileActionAttackSystem(this))
                .AddSystem(new AttackCooldownTimerSystem())
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new EndAttackSystem());

            return entity;
        }
        
        public Entity CreatePuddle(Vector3 position, PuddleConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessTime))
                .AddDeathProcessCurrentTime()
                .AddCurrentTarget()
                .AddAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
                .AddTriggerRadius(new ReactiveVariable<float>(config.AttackTriggerRadius))
                .AddAttackRadius(new ReactiveVariable<float>(config.AttackTriggerRadius))
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()
                
                //attack
                .AddAttackRequested()
                .AddAttackStarted()
                .AddAttackCooldownCurrentTime()
                .AddAttackCooldownInitialTime(new ReactiveVariable<float>(0))
                .AddInAttackCooldown()
                .AddAttackProcessInitialTime(new ReactiveVariable<float>(config.AttackEverySecond))
                .AddAttackProcessCurrentTime()
                .AddInAttackProcess()
                .AddAttackInitialActionTime(new ReactiveVariable<float>(0))
                .AddHasReachedActionTime()
                .AddAttackCompleted();

            ICompositeCondition mustDie = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => false));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.InAttackCooldown.Value == false))
                .Add(new FuncCondition(() => entity.InAttackProcess.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack);

            entity
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackProcessSystem())
                .AddSystem(new AreaActionAttackSystem(this))
                .AddSystem(new AttackCooldownTimerSystem())
                //.AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                //.AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new EndAttackSystem());

            return entity;
        }

        public Entity CreateInstantDamageZone(Vector3 position, Entity owner)
        {
            InstantDamageZoneConfig config = _configsProviderService.GetConfig<InstantDamageZoneConfig>();

            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity.BodyCollider.radius = owner.AttackRadius.Value / 2;

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
                .AddOwner(new ReactiveVariable<Entity>(owner))
                .AddContactsDetectingMask(Layers.CharactersMask)
                .AddContactCollidersBuffer(new Buffer<Collider>(BufferDefaultSize))
                .AddContactEntitiesBuffer(new Buffer<Entity>(BufferDefaultSize))
                .AddBodyContactDamage(new ReactiveVariable<float>(owner.AttackDamage.Value))
                .AddAttackRadius(new ReactiveVariable<float>(owner.AttackRadius.Value))
                .AddIsDead()
                .AddIsTouchAnotherTeam()
                .AddTeam(new ReactiveVariable<Teams>(owner.Team.Value));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => true));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new AnotherTeamTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateProjectile(Vector3 position, Vector3 direction, Entity owner)
        {
            Entity entity = CreateEmpty();

            //todo вынести в конфиг
            _monoEntitiesFactory.Create(entity, position, "Entities/Projectile");
            float moveSpeed = 20;
            float maxTravelDistance = 20;
            
            entity
                .AddID(new ReactiveVariable<string>("Projectile"))
                
                .AddIsProjectile()
                .AddOwner(new ReactiveVariable<Entity>(owner))
                
                .AddIsMoving()
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddMoveSpeed(new ReactiveVariable<float>(moveSpeed))
                .AddContactsDetectingMask(Layers.CharactersMask)
                .AddContactCollidersBuffer(new Buffer<Collider>(BufferDefaultSize))
                .AddContactEntitiesBuffer(new Buffer<Entity>(BufferDefaultSize))
                
                .AddBodyContactDamage(new ReactiveVariable<float>(owner.AttackDamage.Value))
                
                .AddIsDead()
                .AddDeathMask(Layers.CharactersMask)
                .AddIsTouchDeathMask()
                .AddIsTouchAnotherTeam()
                .AddTeam(new ReactiveVariable<Teams>(owner.Team.Value))
                
                .AddMaxTravelDistance(new ReactiveVariable<float>(maxTravelDistance))
                .AddCurrentTravelDistance();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.IsTouchAnotherTeam.Value))
                .Add(new FuncCondition(() => entity.CurrentTravelDistance.Value >= entity.MaxTravelDistance.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                //.AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new MaxTravelDistanceCalculateSystem())
                .AddSystem(new DeathMaskTouchDetectorSystem())
                .AddSystem(new AnotherTeamTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateMine(Vector3 position, MineConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
                .AddAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
                .AddAttackRadius(new ReactiveVariable<float>(config.AttackRadius))
                .AddTriggerRadius(new ReactiveVariable<float>(config.TriggerRadius))
                .AddAttackRequested()
                .AddAttackStarted()
                .AddHasReachedActionTime()
                .AddAttackCompleted()
                .AddCurrentTarget()
                .AddIsDead()
                .AddInDeathProcess()
                .AddDeathProcessInitialTime(new ReactiveVariable<float>(config.DeathProcessTime))
                .AddDeathProcessCurrentTime();

            ICompositeCondition canStartAttack = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.AttackCompleted.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanStartAttack(canStartAttack);

            entity
                .AddSystem(new StartAttackSystem())
                .AddSystem(new AttackInstantSystem())
                .AddSystem(new AreaActionAttackSystem(this))
                .AddSystem(new DeathSystem())
                .AddSystem(new DeathProcessTimerSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new EndAttackSystem());

            return entity;
        }

        public Entity CreateCursorAttacker()
        {
            Entity entity = CreateEmpty();
            CursorAttackerConfig config = _configsProviderService.GetConfig<CursorAttackerConfig>();

            _monoEntitiesFactory.Create(entity, Vector3.zero, config.PrefabPath);

            entity
                .AddID(new ReactiveVariable<string>(config.ID))
                .AddAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
                .AddAttackRadius(new ReactiveVariable<float>(config.AttackRadius));

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}

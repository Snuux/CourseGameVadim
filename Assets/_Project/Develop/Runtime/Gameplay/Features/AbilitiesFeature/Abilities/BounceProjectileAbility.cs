using System;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.Gameplay.Features.Attack;
using _Project.Develop.Runtime.Gameplay.Features.BounceFeature;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class BounceProjectileAbility : Ability, IDisposable
    {
        private readonly BounceProjectileAbilityConfig _config;
        private readonly Entity _owner;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BounceProjectileAbility(
            BounceProjectileAbilityConfig config,
            Entity owner,
            EntitiesLifeContext entitiesLifeContext,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _config = config;
            _owner = owner;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public override void Activate()
        {
            _entitiesLifeContext.Added += OnCreaturesAdded;
        }

        private void OnCreaturesAdded(Entity entity)
        {
            if (entity.HasComponent<IsProjectile>()
                && entity.TryGetOwner(out ReactiveVariable<Entity> owner)
                && owner.Value == _owner)
            {
                entity
                    .AddBounceCount(new ReactiveVariable<int>(_config.GetBounceCountBy(CurrentLevel.Value)))
                    .AddBounceEvent()
                    .AddLayerToBounceReaction(_config.LayerBounceRection);

                entity.MustDie.Add(new FuncCondition(() => entity.BounceCount.Value + 1 == 0), 5);

                entity
                    .AddSystem(new BounceDetectorSystem())
                    .AddSystem(new ReflectRotationDirectionOnBounceSystem())
                    .AddSystem(new ReflectMovementDirectionOnBounceSystem())
                    .AddSystem(new BounceCountDecreaseSystem());
            }
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnCreaturesAdded;
        }
    }
}

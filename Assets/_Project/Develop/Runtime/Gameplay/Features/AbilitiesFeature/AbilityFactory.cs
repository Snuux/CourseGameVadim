using System;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityFactory
    {
        private readonly DIContainer _container;
        private readonly ShopAbilitiesConfig _shopAbilitiesConfig;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
            _shopAbilitiesConfig = container.Resolve<ConfigsProviderService>().GetConfig<ShopAbilitiesConfig>();
        }

        public Entity CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            InitializeAbilities(entity);

            if (entity.Abilities.Elements.Any(ability => ability.ID == config.ID))
                return entity;

            switch (config)
            {
                case HealthIncreaseAbilityConfig healthIncreaseAbilityConfig:
                    entity.Abilities.Add(new MaxHealthIncreaseAbility(entity, healthIncreaseAbilityConfig));
                    return entity;
                
                case DamageIncreaseAbilityConfig damageIncreaseAbilityConfig:
                    entity.Abilities.Add(new DamageIncreaseAbility(entity, damageIncreaseAbilityConfig));
                    return entity;
                
                case ApplyDamageOnFirstEnemiesAbilityConfig damageOnFirstEnemiesAbilityConfig:
                    entity.Abilities.Add(new ApplyDamageOnFirstEntitiesAbility(entity, damageOnFirstEnemiesAbilityConfig, _entitiesLifeContext));
                    return entity;
                
                case HealAbilityConfig healAbilityConfig:
                    entity.Abilities.Add(new HealAbility(entity, healAbilityConfig));
                    return entity;

                default:
                    throw new ArgumentException($"Unknown ability config {config}");
            }
        }

        private void InitializeAbilities(Entity entity)
        {
            if (entity.TryGetAbilities(out _) == true)
                return;

            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();

            entity
                .AddAbilities()
                .AddSystem(new AbilityOnAddActivatorSystem(_shopAbilitiesConfig))
                .AddSystem(new AbilityOnStateChangedActivatorSystem(stageProviderService, _shopAbilitiesConfig));
        }
    }
}

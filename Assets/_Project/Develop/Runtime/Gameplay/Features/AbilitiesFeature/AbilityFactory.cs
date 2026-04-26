using System;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;
using _Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityFactory
    {
        private DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public void CreateAbilityFor(Entity entity, AbilityConfig config)
        {
            switch (config)
            {
                case HealthIncreaseAbilityConfig healthIncreaseAbilityConfig:
                    entity.AddAbilities();
                    entity.Abilities.Add(new HealthIncreaseAbility(entity, healthIncreaseAbilityConfig));
                    return;

                /*case AdditionalDirectionsShotAbilityConfig additionalDirectionsShotAbilityConfig:
                    return new AdditionalDirectionsShotAbility(additionalDirectionsShotAbilityConfig, entity, currentLevel);

                case BounceProjectileAbilityConfig bounceProjectileAbilityConfig:
                    return new BounceProjectileAbility(
                        bounceProjectileAbilityConfig,
                        entity,
                        _container.Resolve<EntitiesLifeContext>(),
                        currentLevel);*/

                default:
                    throw new ArgumentException($"Unknown ability config {config}");
            }
        }
    }
}
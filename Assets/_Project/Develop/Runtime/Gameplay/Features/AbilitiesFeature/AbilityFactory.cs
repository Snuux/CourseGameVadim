using System;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.Abilities;
using _Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature
{
    public class AbilityFactory
    {
        private DIContainer _container;

        public AbilityFactory(DIContainer container)
        {
            _container = container;
        }

        public Ability CreateAbilityFor(Entity entity, AbilityConfig config, int currentLevel)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChangeConfig:
                    return new StatChangeAbility(entity, statChangeConfig, currentLevel);

                case AdditionalDirectionsShotAbilityConfig additionalDirectionsShotAbilityConfig:
                    return new AdditionalDirectionsShotAbility(additionalDirectionsShotAbilityConfig, entity, currentLevel);

                default:
                    throw new ArgumentException($"Unknown ability config {config}");
            }
        }
    }
}
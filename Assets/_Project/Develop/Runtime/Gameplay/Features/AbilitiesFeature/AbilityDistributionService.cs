using System;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Abilities;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityDistributionService : IInitializable, IDisposable
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly AbilitiesShopService _abilitiesShopService;
        private readonly AbilityFactory _abilityFactory;

        public AbilityDistributionService(
            EntitiesLifeContext entitiesLifeContext,
            AbilitiesShopService abilitiesShopService,
            AbilityFactory abilityFactory)
        {
            _entitiesLifeContext = entitiesLifeContext;
            _abilitiesShopService = abilitiesShopService;
            _abilityFactory = abilityFactory;
        }

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
            _abilitiesShopService.Bought += OnAbilityBought;
            Distribute();
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
            _abilitiesShopService.Bought -= OnAbilityBought;
        }

        public void Distribute()
        {
            foreach (Entity entity in _entitiesLifeContext.Entities)
                ApplyBoughtAbilitiesTo(entity);
        }

        private void OnEntityAdded(Entity entity)
        {
            ApplyBoughtAbilitiesTo(entity);
        }

        private void OnAbilityBought(string _)
        {
            Distribute();
        }

        private void ApplyBoughtAbilitiesTo(Entity entity)
        {
            foreach (ShopAbilityConfig abilityConfig in _abilitiesShopService.AvailableAbilitiesConfigs)
            {
                if (_abilitiesShopService.HasAbility(abilityConfig.ID) == false)
                    continue;

                if (CanApplyTo(entity, abilityConfig.ApplyToType) == false)
                    continue;

                _abilityFactory.CreateAbilityFor(entity, abilityConfig.AbilityConfig);
            }
        }

        private static bool CanApplyTo(Entity entity, EntitiesFilters filter)
        {
            switch (filter)
            {
                case EntitiesFilters.All:
                    return true;

                case EntitiesFilters.Ally:
                    return entity.HasComponent<Team>() && entity.Team.Value == Teams.Ally;

                case EntitiesFilters.Enemies:
                    return entity.HasComponent<Team>() && entity.Team.Value == Teams.Enemies;

                case EntitiesFilters.Tower:
                    return entity.HasComponent<IsTower>();
                
                case EntitiesFilters.Cursor:
                    return entity.HasComponent<IsCursor>();

                default:
                    return false;
            }
        }
    }
}

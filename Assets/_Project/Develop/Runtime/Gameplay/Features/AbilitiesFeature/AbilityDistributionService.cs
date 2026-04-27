using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Meta.Features.Abilities;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature
{
    public class AbilityDistributionService
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly AbilitiesService _abilitiesService;
        private readonly AbilitiesConfigsContainer _abilitiesConfigsContainer;
        private readonly AbilityFactory _abilityFactory;

        public AbilityDistributionService(
            EntitiesLifeContext entitiesLifeContext,
            AbilitiesService abilitiesService,
            AbilitiesConfigsContainer abilitiesConfigsContainer,
            AbilityFactory abilityFactory)
        {
            _entitiesLifeContext = entitiesLifeContext;
            _abilitiesService = abilitiesService;
            _abilitiesConfigsContainer = abilitiesConfigsContainer;
            _abilityFactory = abilityFactory;
        }

        public void Distribute()
        {
            foreach (Ability ability in _abilitiesService.AvailableAbilities)
            {
                AbilityConfig abilityConfig = _abilitiesConfigsContainer.GetConfigBy(ability);

                switch (abilityConfig.ApplyToType)
                {
                    case EntitiesFilters.All:
                        foreach (Entity entity in _entitiesLifeContext.Entities)
                            _abilityFactory.CreateAbilityFor(entity, abilityConfig);
                        break;
                    
                    case EntitiesFilters.Ally:
                        foreach (Entity entity in _entitiesLifeContext.Entities.Where(e => e.Team.Value == Teams.Ally))
                            _abilityFactory.CreateAbilityFor(entity, abilityConfig);
                        break;
                    
                    case EntitiesFilters.Enemy:
                        foreach (Entity entity in
                                 _entitiesLifeContext.Entities.Where(e => e.Team.Value == Teams.Enemies))
                            _abilityFactory.CreateAbilityFor(entity, abilityConfig);
                        break;
                    
                    case EntitiesFilters.Tower:
                        foreach (Entity entity in _entitiesLifeContext.Entities.Where(e => e.IsTowerC != null))
                            _abilityFactory.CreateAbilityFor(entity, abilityConfig);
                        break;
                }
            }
        }
    }
}
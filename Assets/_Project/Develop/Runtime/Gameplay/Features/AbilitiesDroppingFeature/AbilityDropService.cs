using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDropService
    {
        private readonly AbilitiesConfigsContainer _abilitiesConfigsContainer;
        private readonly AbilityDroppingRuleService _droppingRuleService;

        public AbilityDropService(AbilitiesConfigsContainer abilitiesConfigsContainer,
            AbilityDroppingRuleService droppingRuleService)
        {
            _abilitiesConfigsContainer = abilitiesConfigsContainer;
            _droppingRuleService = droppingRuleService;
        }

        public List<AbilityConfig> Drop(Entity entity, int count = 3)
        {
            List<AbilityConfig> availableConfigs
                = new List<AbilityConfig>(_abilitiesConfigsContainer
                    .AbilityConfigs
                    .Where(abilityOption => _droppingRuleService.IsAvailable(abilityOption, entity)));

            List<AbilityConfig> selectedAbilities = new();

            for (int i = 0; i < count; i++)
            {
                AbilityConfig abilityConfig = availableConfigs[UnityEngine.Random.Range(0, availableConfigs.Count)];
                selectedAbilities.Add(abilityConfig);
                availableConfigs.RemoveAt(0);
            }

            return selectedAbilities;
        }
    }
}
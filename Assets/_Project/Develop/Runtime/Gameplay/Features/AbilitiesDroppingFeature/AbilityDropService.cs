using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDropService
    {
        private readonly AbilitiesConfigsContainer _abilitiesConfigsContainer;
        private readonly AbilityDroppingRuleService _abilityDropingRules;

        public AbilityDropService(
            AbilitiesConfigsContainer abilitiesConfigsContainer,
            AbilityDroppingRuleService abilityDropingRules)
        {
            _abilitiesConfigsContainer = abilitiesConfigsContainer;
            _abilityDropingRules = abilityDropingRules;
        }

        public List<AbilityDropOption> Drop(int count, Entity entity)
        {
            List<AbilityDropOption> availablesAbilities = new List<AbilityDropOption>();

            foreach (AbilityConfig abilityConfig in _abilitiesConfigsContainer.AbilityConfigs)
            {
                for (int level = 1; level < abilityConfig.MaxLevel + 1; level++)
                {
                    if (_abilityDropingRules.IsAvailable(abilityConfig, entity, level))
                        availablesAbilities.Add(new AbilityDropOption(abilityConfig, level));
                }
            }

            List<AbilityDropOption> selectedAbilities = new();

            for (int i = 0; i < count; i++)
            {
                AbilityDropOption selectedAbility =
                    availablesAbilities[UnityEngine.Random.Range(0, availablesAbilities.Count)];
                selectedAbilities.Add(selectedAbility);
                availablesAbilities.Remove(selectedAbility);
            }

            return selectedAbilities;
        }
    }
}
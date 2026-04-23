using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDroppingRuleService
    {
        public bool IsAvailable(AbilityConfig config, Entity entity, int abilityLevel)
        {
            if (config.IsUpgradable())
            {
                if (entity.Abilities.Elements.Any(ability =>
                        ability.ID == config.ID
                        && ability.CurrentLevel.Value + abilityLevel > ability.MaxLevel))
                {
                    return false;
                }
            }

            switch (config)
            {
                case StatChangeAbilityConfig statChangeAbilityConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                           && modifiedStats.ContainsKey(statChangeAbilityConfig.StatType);
            }

            return true;
        }
    }
}
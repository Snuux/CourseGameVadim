using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature
{
    public class AbilityDroppingRuleService
    {
        public bool IsAvailable(AbilityConfig config, Entity entity)
        {
            switch (config)
            {
                case StatChangeAbilityConfig statChangeConfig:
                    return entity.TryGetModifiedStats(out var modifiedStats)
                           && modifiedStats.ContainsKey(statChangeConfig.StatType);
            }

            return true;
        }
    }
}
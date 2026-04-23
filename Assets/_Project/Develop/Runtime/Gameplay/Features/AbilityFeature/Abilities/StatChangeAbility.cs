using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.StatFeature;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature.Abilities
{
    public class StatChangeAbility : Ability
    {
        private Entity _entity;
        private StatChangeAbilityConfig _config;

        public StatChangeAbility(
            Entity entity,
            StatChangeAbilityConfig config,
            int currentLevel) : base(config.ID, currentLevel, config.MaxLevel)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.StatsEffects.Add(new StatsEffect(_config.StatType, _config.GetApplyEffect()));
        }
    }
}
using System;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class HealAbility : Ability
    {
        private readonly HealAbilityConfig _config;
        private readonly Entity _entity;

        public HealAbility(
            Entity entity,
            HealAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            float newHealth = _entity.CurrentHealth.Value + _entity.CurrentHealth.Value * _config.HealPercent / 100f;
            _entity.CurrentHealth.Value += Math.Min(newHealth, _entity.MaxHealth.Value);
        }
    }
}
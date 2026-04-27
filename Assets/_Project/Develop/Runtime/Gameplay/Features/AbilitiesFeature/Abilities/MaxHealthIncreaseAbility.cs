using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class MaxHealthIncreaseAbility : Ability
    {
        private readonly HealthIncreaseAbilityConfig _config;
        private readonly Entity _entity;

        public MaxHealthIncreaseAbility(
            Entity entity,
            HealthIncreaseAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            _entity.MaxHealth.Value += _config.Amount;
            _entity.CurrentHealth.Value += _config.Amount;
        }
    }
}
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class HealthIncreaseAbility : Ability
    {
        private readonly Entity _entity;

        public HealthIncreaseAbility(
            Entity entity,
            HealthIncreaseAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
        }

        public override void Activate()
        {
            _entity.MaxHealth.Value += 20;
            _entity.CurrentHealth.Value += 20;
        }
    }
}
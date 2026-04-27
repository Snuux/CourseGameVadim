using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class DamageIncreaseAbility : Ability
    {
        private readonly DamageIncreaseAbilityConfig _config;
        private readonly Entity _entity;

        public DamageIncreaseAbility(
            Entity entity,
            DamageIncreaseAbilityConfig config) : base(config.ID)
        {
            _entity = entity;
            _config = config;
        }

        public override void Activate()
        {
            float newDamage = _entity.AttackDamage.Value + _entity.AttackDamage.Value * _config.DamagePercent / 100f;
            _entity.AttackDamage.Value = newDamage;
        }
    }
}
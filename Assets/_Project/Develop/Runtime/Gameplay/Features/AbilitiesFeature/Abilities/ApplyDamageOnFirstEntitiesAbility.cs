using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class ApplyDamageOnFirstEntitiesAbility : Ability
    {
        private readonly ApplyDamageOnFirstEnemiesAbilityConfig _config;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly Entity _entity;

        public ApplyDamageOnFirstEntitiesAbility(
            Entity entity,
            ApplyDamageOnFirstEnemiesAbilityConfig config, 
            EntitiesLifeContext entitiesLifeContext) : base(config.ID)
        {
            _entity = entity;
            _config = config;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public override void Activate()
        {
            int entityIndex = 0;

            foreach (Entity entity in _entitiesLifeContext.Entities)
            {
                if (CanCount(entity) == false)
                    continue;

                if (entity != _entity)
                {
                    entityIndex++;
                    continue;
                }

                if (entityIndex < _config.EnemiesCount)
                {
                    float damage = _entity.CurrentHealth.Value * _config.DamagePercent / 100f;
                    _entity.TakeDamageRequest.Invoke(damage);
                }

                return;
            }
        }

        private bool CanCount(Entity entity)
        {
            if (HasAbility(entity) == false)
                return false;

            if (entity.TryGetIsDead(out var isDead) && isDead.Value)
                return false;

            return true;
        }

        private bool HasAbility(Entity entity)
        {
            if (entity.TryGetAbilities(out var abilities) == false)
                return false;

            return abilities.Elements.Any(ability => ability.ID == ID);
        }
    }
}

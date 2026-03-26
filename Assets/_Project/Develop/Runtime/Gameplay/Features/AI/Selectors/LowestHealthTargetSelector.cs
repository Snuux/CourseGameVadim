using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Utilities.Conditions;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.Selectors
{
    public class LowestHealthTargetSelector : ITargetSelector
    {
        private Entity _source;

        public LowestHealthTargetSelector(Entity entity)
        {
            _source = entity;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if(target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
                {
                    result = result && canApplyDamage.Evaluate();
                }

                result = result && (target != _source);

                return result;
            });

            if (selectedTargets.Any() == false)
                return null;

            Entity lowestHealthTarget = selectedTargets.First();
            float lowestHealth = lowestHealthTarget.CurrentHealth.Value;

            foreach (Entity target in selectedTargets)
            {
                float health = target.CurrentHealth.Value;
                
                if(health < lowestHealth)
                {
                    lowestHealth = health;
                    lowestHealthTarget = target;
                }
            }

            return lowestHealthTarget;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using _Project.Develop.Runtime.Utilities.Conditions;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class NearestDamageableTargetSelector : ITargetSelector
    {
        private Entity _source;
        private Transform _sourceTransform;

        public NearestDamageableTargetSelector(Entity source)
        {
            _source = source;
            _sourceTransform = source.Transform;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
                {
                    result = result && canApplyDamage.Evaluate();
                }

                result = result && (target != _source);
                
                return result;
            });

            if (selectedTargets.Any() == false)
                return null;

            Entity closestTarget = selectedTargets.First();
            float minDistance = GetDistanceTo(closestTarget);

            foreach (Entity target in selectedTargets)
            {
                float distance = GetDistanceTo(target);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target;
                }
            }
            
            return closestTarget;
        }

        private float GetDistanceTo(Entity target) => (_sourceTransform.position - target.Transform.position).magnitude;
    }
}
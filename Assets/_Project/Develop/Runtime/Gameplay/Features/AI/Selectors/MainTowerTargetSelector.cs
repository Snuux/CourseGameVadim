using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.Selectors
{
    public class MainTowerTargetSelector : ITargetSelector
    {
        private readonly Entity _source;

        public MainTowerTargetSelector(Entity entity)
        {
            _source = entity;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            return targets.FirstOrDefault(CanSelect);
        }

        private bool CanSelect(Entity target)
        {
            if (target == _source)
                return false;

            if (target.HasComponent<IsTower>() == false)
                return false;

            //if (_range == null)
            //    return true;
            
            //if (Vector3.Distance(_source.Transform.position, target.Transform.position) > _range.Value)
            //    return false;
            
            //if (target.HasComponent<TakeDamageRequest>() == false)
            //    return false;

            //if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
            //    return canApplyDamage.Evaluate();

            return true;
        }
    }
}
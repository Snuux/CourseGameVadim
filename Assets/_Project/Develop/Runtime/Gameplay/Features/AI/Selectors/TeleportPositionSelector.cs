using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.Selectors
{
    public class TeleportPositionSelector
    {
        public static Vector3 RandomPositionInRadiusSelector(Entity e)
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle * e.TeleportRadius.Value;
            Vector3 pointOnPlane = new Vector3(randomCirclePoint.x + e.Transform.position.x, e.Transform.position.y, randomCirclePoint.y + e.Transform.position.z);

            return pointOnPlane;
        }
        
        public static Vector3 NearestTargetPositionInRadiusSelector(Entity e)
        {
            Vector3 source = e.Transform.position;
            float radius = e.TeleportRadius.Value;
            Entity targetEntity = e.CurrentTarget.Value;

            if (targetEntity == null)
                return e.TeleportTargetPosition.Value;

            Vector3 target = targetEntity.Transform.position;

            Vector3 direction = target - source;
            direction.y = 0f;

            if (direction == Vector3.zero)
                return source;

            float distance = direction.magnitude;

            if (distance <= radius)
                return new Vector3(target.x, source.y, target.z);

            Vector3 nearestPoint = source + direction.normalized * radius;
            nearestPoint.y = source.y;

            return nearestPoint;
        }
    }
}
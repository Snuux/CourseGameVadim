using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportInRadiusState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _teleportTargetPosition;
        
        private readonly float _radius;
        private readonly Transform _teleportTransform;
        
        private readonly ReactiveEvent _teleportRequest;
        
        public RandomTeleportInRadiusState(Entity entity, float radius)
        {
            _teleportTransform = entity.Transform;
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _teleportRequest = entity.TeleportRequest;
            _radius = radius;
        }

        public override void Enter()
        {
            base.Enter();

            _teleportTargetPosition.Value = PositionInRadius(_teleportTransform.position, _radius);
            _teleportRequest.Invoke();
        }

        public void Update(float deltaTime)
        {
        }

        private Vector3 PositionInRadius(Vector3 center, float radius)
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle * radius;
            Vector3 pointOnPlane =
                new Vector3(randomCirclePoint.x + center.x, center.y, randomCirclePoint.y + center.z);
    
            return pointOnPlane;
        }
    }
}
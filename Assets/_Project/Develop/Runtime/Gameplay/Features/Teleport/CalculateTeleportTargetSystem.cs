using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class CalculateTeleportTargetSystem :IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<Vector3> _teleportTargetPosition;
        private Transform _teleportSource;
        private ReactiveVariable<float> _radius;
        
        private IDisposable _calculateTeleportTargetRequestDisposable;

        public void OnInit(Entity entity)
        {
            _teleportSource = entity.Transform;
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _radius = entity.TeleportRadius;

            _calculateTeleportTargetRequestDisposable = entity.CalculateTeleportTargetRequest.Subscribe(OnTeleportRequestCalculateTargetTransform);
        }

        public void OnDispose()
        {
            _calculateTeleportTargetRequestDisposable.Dispose();
        }

        private void OnTeleportRequestCalculateTargetTransform()
        {
            Vector3 teleportTargetPosition = PositionInRadius(_teleportSource.position, _radius.Value);
            
            Debug.Log($"Позиция для телепорта сгенерированна: {teleportTargetPosition}");
            
            _teleportTargetPosition.Value = teleportTargetPosition;
        }
        
        public static Vector3 PositionInRadius(Vector3 center, float radius)
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle * radius;
            Vector3 pointOnPlane =
                new Vector3(randomCirclePoint.x + center.x, center.y, randomCirclePoint.y + center.z);
    
            return pointOnPlane;
        }
    }
}
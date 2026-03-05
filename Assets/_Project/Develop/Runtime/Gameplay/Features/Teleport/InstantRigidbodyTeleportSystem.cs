using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class InstantRigidbodyTeleportSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent<Vector3> _teleportRequest;

        private Rigidbody _rigidbody;
        
        private ReactiveVariable<Vector3> _teleportTargetPosition;

        private IDisposable _doTeleportInTargetPositionRequestDisposable;

        public void OnInit(Entity entity)
        {
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _rigidbody = entity.Rigidbody;

            _doTeleportInTargetPositionRequestDisposable = entity.DoTeleportInTargetPositionRequest.Subscribe(OnTeleportInTargetPositionRequest);
        }

        public void OnDispose()
        {
            _doTeleportInTargetPositionRequestDisposable.Dispose();
        }

        private void OnTeleportInTargetPositionRequest()
        {
            Debug.Log($"Телепорт в позицию: {_teleportTargetPosition.Value}");

            TeleportRigidbody(_teleportTargetPosition.Value);
        }

        private void TeleportRigidbody(Vector3 position)
        {
            _rigidbody.position = position;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
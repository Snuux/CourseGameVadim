using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class InstantRigidbodyTeleportSystem : IInitializableSystem, IUpdatableSystem
    {
        private Rigidbody _rigidbody;
        private ReactiveVariable<Vector3> _teleportTargetPosition;
        private ReactiveVariable<bool> _teleportInProcess;

        public void OnInit(Entity entity)
        {
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _teleportInProcess = entity.TeleportInProcess;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_teleportInProcess.Value == false)
                return;

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
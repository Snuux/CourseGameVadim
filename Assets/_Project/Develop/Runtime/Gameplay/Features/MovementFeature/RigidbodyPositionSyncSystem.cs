using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyPositionSyncSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _position;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _position = entity.Position;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            _position.Value = _rigidbody.transform.position;
        }
    }
}
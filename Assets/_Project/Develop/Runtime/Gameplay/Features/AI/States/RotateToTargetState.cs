using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RotateToTargetState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly Transform _transform;
        private readonly Entity _entity;

        public RotateToTargetState(Entity entity)
        {
            _rotationDirection = entity.RotationDirection;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
            _entity = entity;
        }

        public void Update(float deltaTime)
        {
            if (EntitiesHelper.TryGetAliveTargetTransform(_entity, out Transform targetTransform) == false)
                return;

            _rotationDirection.Value = (targetTransform.position - _transform.position).normalized;
        }
    }
}
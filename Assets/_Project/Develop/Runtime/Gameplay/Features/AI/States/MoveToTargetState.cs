using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class MoveToTargetState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly Transform _transform;

        public MoveToTargetState(Entity entity)
        {
            _rotationDirection = entity.RotationDirection;
            _movementDirection = entity.MoveDirection;
            _currentTarget = entity.CurrentTarget;
            _transform = entity.Transform;
        }

        public void Update(float deltaTime)
        {
            if (_currentTarget.Value != null)
            {
                Vector3 targetDirection = (_currentTarget.Value.Transform.position - _transform.position).normalized;
                
                _rotationDirection.Value = targetDirection;
                _movementDirection.Value = targetDirection;
            }
        }
        
        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }

    }
}
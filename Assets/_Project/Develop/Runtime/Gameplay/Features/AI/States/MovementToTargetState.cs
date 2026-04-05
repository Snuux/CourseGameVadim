using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class MovementToTargetState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly ReactiveVariable<Vector3> _movementDirection;
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly Transform _transform;
        private readonly Entity _entity;

        public MovementToTargetState(Entity entity)
        {
            _rotationDirection = entity.RotationDirection;
            _movementDirection = entity.MoveDirection;
            _transform = entity.Transform;
            _entity = entity;
        }

        public void Update(float deltaTime)
        {
            if (EntitiesHelper.TryGetAliveTargetTransform(_entity, out Transform targetTransform) == false)
                return;
            
            Vector3 targetDirection = (targetTransform.position - _transform.position).normalized;
                
            _rotationDirection.Value = targetDirection;
            _movementDirection.Value = targetDirection;
        }
        
        public override void Exit()
        {
            base.Exit();

            _movementDirection.Value = Vector3.zero;
        }

    }
}
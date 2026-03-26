using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RotateToMouseWorldPositionState : State, IUpdatableState
    {
        private readonly IInputService _inputService;
        private readonly ReactiveVariable<Vector3> _rotationDirection;
        private readonly Transform _transform;

        public RotateToMouseWorldPositionState(Entity entity, IInputService inputService)
        {
            _inputService = inputService;
            _rotationDirection = entity.RotationDirection;
            _transform = entity.Transform;
        }

        public void Update(float deltaTime)
        {
            _rotationDirection.Value = (_inputService.MouseWorldPosition - _transform.position).normalized;
        }
    }
}
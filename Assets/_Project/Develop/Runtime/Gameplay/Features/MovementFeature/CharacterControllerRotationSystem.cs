using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class CharacterControllerRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Quaternion> _rotation;
        private ReactiveVariable<float> _rotationSpeed;
        private CharacterController _characterController;

        public void OnInit(Entity entity)
        {
            _rotation = entity.Rotation;
            _rotationSpeed = entity.RotationSpeed;
            _characterController = entity.CharacterController;
        }

        public void OnUpdate(float deltaTime)
        {
            Transform characterControllerTransform = _characterController.transform;
            
            characterControllerTransform.rotation = Quaternion.Slerp(
                characterControllerTransform.rotation,
                _rotation.Value,
                deltaTime * _rotationSpeed.Value
            );
        }
    }
}
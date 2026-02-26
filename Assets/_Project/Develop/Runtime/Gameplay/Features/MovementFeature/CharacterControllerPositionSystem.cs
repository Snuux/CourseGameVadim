using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class CharacterControllerPositionSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _position;
        private CharacterController _characterController;

        public void OnInit(Entity entity)
        {
            _position = entity.Position;
            _characterController = entity.CharacterController;
        }

        public void OnUpdate(float deltaTime)
        {
            _position.Value = _characterController.transform.position;
        }
    }
}
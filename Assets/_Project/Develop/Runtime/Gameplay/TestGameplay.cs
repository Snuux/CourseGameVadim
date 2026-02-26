using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;

        private bool _isRunning;

        private Entity _entityRigidbody;
        private Entity _entityCharacterController;

        public void Initialize(DIContainer container)
        {
            _container = container;

            _entitiesFactory = container.Resolve<EntitiesFactory>();
        }

        //запускается из буутстрапа для тестового геймплея 
        public void Run()
        {
            _entityRigidbody = _entitiesFactory.CreateTestEntityRigidbody(Vector3.zero);
            _entityCharacterController = _entitiesFactory.CreateTestEntityCharacterController(new Vector3(0, 0, 4));

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            MoveEntity(_entityRigidbody);
            MoveEntity(_entityCharacterController);
            RotateRigidbodyEntity(_entityRigidbody);
            RotateRigidbodyEntity(_entityCharacterController);
        }

        private void MoveEntity(Entity entity)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            entity.MoveDirection.Value = input;
        }

        private void RotateRigidbodyEntity(Entity entity)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero); // плоскость в нуле
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                Vector3 dir = hitPoint - entity.Position.Value;
                dir.y = 0f;

                if (dir.sqrMagnitude > 0.0001f) 
                    entity.Rotation.Value = Quaternion.LookRotation(dir, Vector3.up);
            }
        }
    }
}
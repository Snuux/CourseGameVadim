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
        }
    }
}
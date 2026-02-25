using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;

        private bool _isRunning;

        private Entity _entity;

        public void Initialize(DIContainer container)
        {
            _container = container;

            _entitiesFactory = container.Resolve<EntitiesFactory>();
        }

        //запускается из буутстрапа для тестового геймплея 
        public void Run()
        {
            _entity = _entitiesFactory.CreateTestEntity(Vector3.zero);

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _entity.MoveDirection.Value = input;
        }
    }
}
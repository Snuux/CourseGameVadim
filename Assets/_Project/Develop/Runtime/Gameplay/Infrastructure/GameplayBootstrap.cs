using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Features.Gameplay;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        
        private GameplayRunningService _gameplayRunningService;
        private bool _running;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException(
                    $"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            _gameplayRunningService = _container.Resolve<GameplayRunningService>();
            
            yield break;
        }

        public override void Run()
        {
            _gameplayRunningService.Run();
            
            _running = true;
        }

        private void Update()
        {
            if (_running == false)
                return;
            
            _gameplayRunningService.Update(Time.deltaTime);
        }
    }
}

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
        
        private GameplayRunningService _gameplayCycle;
        private bool _running;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Вы попали на уровень. " +
                      $"Необходимо ввести последовательность из: {_inputArgs.Symbols}" +
                      $"Кол-во символов: {_inputArgs.Length}");

            Debug.Log("Инициализация геймплейной сцены");
            
            _gameplayCycle = _container.Resolve<GameplayRunningService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");
            
            _gameplayCycle.Run();
            _running = true;
        }

        private void Update()
        {
            if (_running)
                _gameplayCycle.Update(Time.deltaTime);
        }
    }
}

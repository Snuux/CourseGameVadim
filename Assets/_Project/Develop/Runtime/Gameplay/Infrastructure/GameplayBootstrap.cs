using System;
using System.Collections;
using _Project.Develop.Runtime.Gameplay.Feature.GameCycle;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        
        private GameCycleHandler _gameCycle;
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
            
            _gameCycle = _container.Resolve<GameCycleHandler>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");
            
            _gameCycle.Run();
            _running = true;
        }

        private void Update()
        {
            if (_running)
                _gameCycle.Update(Time.deltaTime);
        }
    }
}
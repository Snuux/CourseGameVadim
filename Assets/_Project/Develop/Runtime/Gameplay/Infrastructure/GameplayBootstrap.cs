using System;
using System.Collections;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Infrastructure.States;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private WalletService _walletService;

        private GameplayStateContext _gameplayStateContext;
        //[SerializeField] private TestGameplay _testGameplay;

        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;
        private ConfigsProviderService _configsProviderService;

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
            Debug.Log($"Вы попали на уровень {_inputArgs.LevelNumber}");

            Debug.Log("Инициализация геймплейной сцены");

            _walletService = _container.Resolve<WalletService>();

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _gameplayStateContext = _container.Resolve<GameplayStateContext>();
            _configsProviderService = _container.Resolve<ConfigsProviderService>();

            _container.Resolve<AllyFactory>().CreateTower(
                Vector3.zero, 
                _configsProviderService.GetConfig<LevelsListConfig>().GetBy(_inputArgs.LevelNumber)
                );
            
            //_testGameplay.Initialize(_container);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены");

            //_testGameplay.Run();
            _gameplayStateContext.Run();
        }

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _gameplayStateContext?.Update(Time.deltaTime);

            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            //    ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            //    coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            //}
        }
    }
}

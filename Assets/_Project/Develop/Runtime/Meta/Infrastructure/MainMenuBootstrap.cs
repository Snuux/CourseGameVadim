using System.Collections;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Levels;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private WalletService _walletService;
        private GameStatisticsService _gameStatisticsService;

        private PlayerDataProvider _playerDataProvider;
        private ICoroutinesPerformer _coroutinesPerformer;

        private SwitcherSceneByLevelService _switcherSceneByLevelService;

        private bool _running;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            Debug.Log("Процесс регистрации сервисов главного меню");

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            _walletService = _container.Resolve<WalletService>();
            _gameStatisticsService = _container.Resolve<GameStatisticsService>();

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _switcherSceneByLevelService = _container.Resolve<SwitcherSceneByLevelService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");

            _running = true;
            
            Debug.Log("--------------------------------");
            Debug.Log("Статистика игры:");
            foreach (GameStatisticsTypes gameStatisticsTypes in _gameStatisticsService.AllGameStatistics)
                Debug.Log($"{gameStatisticsTypes.ToString()}: " +
                          $"{_gameStatisticsService.GetGameStatistics(gameStatisticsTypes).Value}");
            
            foreach (CurrencyTypes currencyTypes in _walletService.AvailableCurrencies)
                Debug.Log($"{currencyTypes.ToString()}: " +
                          $"{_walletService.GetCurrency(currencyTypes).Value}");
        }

        private void Update()
        {
            if (_running == false)
                return;
            
            _switcherSceneByLevelService.Update(Time.deltaTime);
            
            /*if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(2)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Золота осталось: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if(_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log("Золота осталось: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Сохранение было вызвано");
            }*/
        }
    }
}
using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features.Menu
{
    class MainMenuRunningService
    {
        private readonly WalletService _walletService;
        private readonly GameStatisticsService _gameStatisticsService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly ResetPriceConfig _resetPriceConfig;

        public MainMenuRunningService(
            WalletService walletService,
            GameStatisticsService gameStatisticsService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer,
            ResetPriceConfig resetPriceConfig)
        {
            _walletService = walletService;
            _gameStatisticsService = gameStatisticsService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _resetPriceConfig = resetPriceConfig;
        }

        public void Run()
        {
            OutputInstructions();
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.D))
                TryToBuyResetStatistics();

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Сохранение было вызвано");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log($"-- Статистика игры: " +
                          $"{_gameStatisticsService.AsString()}. " +
                          $"{_walletService.AsString()}");
            }
        }

        private void TryToBuyResetStatistics()
        {
            if (_walletService.Enough(_resetPriceConfig.CurrencyType, _resetPriceConfig.Value))
            {
                _walletService.Spend(_resetPriceConfig.CurrencyType, _resetPriceConfig.Value);
                _gameStatisticsService.ResetAll();
                Debug.Log($"Сброс был куплен. Осталось: {_walletService.AsString()}");
            }
            else
            {
                Debug.Log($"Не хватает золота для сброса! ({_walletService.AsString()})");
            }
        }

        private void OutputInstructions()
        {
            Debug.Log($"'A' - показать сохраненные данные, " +
                      $"'D' - купить сброс данных за {_resetPriceConfig.CurrencyType}: {_resetPriceConfig.Value}, " +
                      $"'S' - сохранить данные " +
                      $"'1' - режим игры символы " +
                      $"'2' - режим игры цифры ");
        }
    }
}
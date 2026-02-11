using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplayGetRewardService
    {
        private readonly GameStatisticsService _gameStatisticsService;
        private readonly WalletService _walletService;
        private readonly LevelsRewardConfig _levelsRewardConfig;

        public GameplayGetRewardService(
            GameStatisticsService gameStatisticsService,
            WalletService walletService,
            LevelsRewardConfig levelsRewardConfig)
        {
            _gameStatisticsService = gameStatisticsService;
            _walletService = walletService;
            _levelsRewardConfig = levelsRewardConfig;
        }

        public void WinProcess()
        {
            _gameStatisticsService.Add(GameStatisticsTypes.Win);
            _walletService.Add(_levelsRewardConfig.CurrencyType, _levelsRewardConfig.Value);

            Debug.Log("Вы победили! Нажмите 'Space' для продолжения...");
        }

        public void DefeatProcess()
        {
            _gameStatisticsService.Add(GameStatisticsTypes.Defeat);
            _walletService.Sub(_levelsRewardConfig.CurrencyType, _levelsRewardConfig.Value);

            Debug.Log("Вы проиграли! Нажмите 'Space' для продолжения...");
        }
    }
}
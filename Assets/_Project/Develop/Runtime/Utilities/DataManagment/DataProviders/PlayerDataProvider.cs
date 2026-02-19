using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.GameStatistics;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace _Project.Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private readonly ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadSerivce saveLoadSerivce, 
            ConfigsProviderService configsProviderService) : base(saveLoadSerivce)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                GameStatisticsData = InitGameStatisticsData(),
                CompletedLevels = new()
            };
        }

        private Dictionary<GameStatisticsTypes, int> InitGameStatisticsData()
        {
            Dictionary<GameStatisticsTypes, int> gameStatisticsData = new();

            StartGameStatisticsConfig gameStatisticsesConfig 
                = _configsProviderService.GetConfig<StartGameStatisticsConfig>();

            foreach (GameStatisticsTypes gameStatisticsTypes in Enum.GetValues(typeof(GameStatisticsTypes)))
                gameStatisticsData[gameStatisticsTypes] = gameStatisticsesConfig.GetValueFor(gameStatisticsTypes);

            return gameStatisticsData;
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData[currencyType] = walletConfig.GetValueFor(currencyType);

            return walletData;
        }
    }
}

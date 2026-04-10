using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.Statistics;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

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
                StatisticsData = InitStatisticsData(),
                CompletedLevels = new()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartWalletConfig walletConfig = _configsProviderService.GetConfig<StartWalletConfig>();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData[currencyType] = walletConfig.GetValueFor(currencyType);

            return walletData;
        }

        private Dictionary<StatisticType, int> InitStatisticsData()
        {
            Dictionary<StatisticType, int> statisticsData = new();

            StartStatisticsConfig statisticsConfig = _configsProviderService.GetConfig<StartStatisticsConfig>();
            
            foreach (StatisticType recordType in Enum.GetValues(typeof(StatisticType)))
                statisticsData[recordType] = statisticsConfig.GetValueFor(recordType);

            return statisticsData;
        }
    }
}
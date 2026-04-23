using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.Features.StatFeature;
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
                CompletedLevels = new(),
                StatsUpgradeLevel = InitStatsUpgradeValue()
            };
        }

        private Dictionary<StatTypes, int> InitStatsUpgradeValue()
        {
            Dictionary<StatTypes, int> statUpgradeData = new();

            foreach (StatTypes statType in Enum.GetValues(typeof(StatTypes)))
                statUpgradeData.Add(statType, 1);

            return statUpgradeData;
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
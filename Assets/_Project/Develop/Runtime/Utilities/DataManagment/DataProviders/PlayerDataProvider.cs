using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
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
                CurrenciesData = InitWalletData(),
                GameEndData = InitGameEndData()
            };
        }

        private Dictionary<GameEndTypes, int> InitGameEndData()
        {
            Dictionary<GameEndTypes, int> initGameEndData = new();

            StartGameEndValuesConfig startGameEndValuesConfig 
                = _configsProviderService.GetConfig<StartGameEndValuesConfig>();

            foreach (GameEndTypes gameEndTypes in Enum.GetValues(typeof(GameEndTypes)))
                initGameEndData[gameEndTypes] = startGameEndValuesConfig.GetValueFor(gameEndTypes);

            return initGameEndData;
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            StartCurrenciesConfig currenciesConfig = _configsProviderService.GetConfig<StartCurrenciesConfig>();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData[currencyType] = currenciesConfig.GetValueFor(currencyType);

            return walletData;
        }
    }
}

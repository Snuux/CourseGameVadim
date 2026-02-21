using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.DataManagment.DataRepository;
using _Project.Develop.Runtime.Utilities.DataManagment.KeysStorage;
using _Project.Develop.Runtime.Utilities.DataManagment.Serializers;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(CreateResourcesAssetsLoader);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);

            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);

            container.RegisterAsSingle(CreateWalletService).NonLazy();
            
            container.RegisterAsSingle(CreateLevelOutcomeService).NonLazy();

            container.RegisterAsSingle(CreatePlayerDataProvider);

            container.RegisterAsSingle<ISaveLoadSerivce>(CreateSaveLoadService);

            container.RegisterAsSingle(CreateProjectPresenterFactory);
            
            container.RegisterAsSingle(CreateViewsFactory);
        }

        private static ViewsFactory CreateViewsFactory(DIContainer c)
        {
            return new ViewsFactory(c.Resolve<ResourcesAssetsLoader>());
        }

        private static ProjectPresentersFactory CreateProjectPresenterFactory(DIContainer c)
        {
            return new ProjectPresentersFactory(c);
        }

        private static PlayerDataProvider CreatePlayerDataProvider(DIContainer c)
        {
            return new PlayerDataProvider(
                c.Resolve<ISaveLoadSerivce>(),
                c.Resolve<ConfigsProviderService>());
        }

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage dataKeysStorage = new MapDataKeysStorage();

            string saveFolderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;

            IDataRepository dataRepository = new LocalFileDataRepository(saveFolderPath, "json");

            return new SaveLoadService(dataSerializer, dataKeysStorage, dataRepository);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }
        
        private static LevelOutcomeService CreateLevelOutcomeService(DIContainer c)
        {
            Dictionary<GameEndTypes, ReactiveVariable<int>> gameEnds = new();

            foreach (GameEndTypes currencyType in Enum.GetValues(typeof(GameEndTypes)))
                gameEnds[currencyType] = new ReactiveVariable<int>();

            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new LevelOutcomeService(
                gameEnds, 
                configsProviderService, 
                c.Resolve<PlayerDataProvider>()
                );
        }
        
        /*private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> gameRewards = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(GameEndTypes)))
                gameRewards[currencyType] = new ReactiveVariable<int>();

            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();

            return new WalletService(
                gameRewards,
                configsProviderService, 
                c.Resolve<PlayerDataProvider>()
            );
        }*/

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
        {
            return new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);
        }

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandardLoadingScreen standardLoadingScreenPrefab = resourcesAssetsLoader
                .Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standardLoadingScreenPrefab);
        }
    }
}

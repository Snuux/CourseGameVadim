using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Menu;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню");

            container.RegisterAsSingle(CreateChangeSceneByLevelTypeService);
            container.RegisterAsSingle(CreateMainMenuRunningService);

            container.RegisterAsSingle(CreateWalletPresenter).NonLazy();
        }

        private static MainMenuSwitcherSceneService CreateChangeSceneByLevelTypeService(DIContainer c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new MainMenuSwitcherSceneService(configsProviderService, sceneSwitcherService, coroutinesPerformer);
        }

        private static MainMenuRunningService CreateMainMenuRunningService(DIContainer c)
        {
            return new MainMenuRunningService(
                c.Resolve<WalletService>(),
                c.Resolve<GameStatisticsService>(),
                c.Resolve<PlayerDataProvider>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<ConfigsProviderService>().GetConfig<ResetPriceConfig>()
            );
        }

        private static WalletPresenter CreateWalletPresenter(DIContainer c)
        {
            IconTextListView walletView = Object.FindObjectOfType<IconTextListView>();
            WalletPresenter walletPresenter = c.Resolve<ProjectPresenterFactory>().CreateWalletPresenter(walletView);

            return walletPresenter;
        }
    }
}
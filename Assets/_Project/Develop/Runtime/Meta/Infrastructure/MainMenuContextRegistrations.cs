using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Menu;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
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
            container.RegisterAsSingle(CreateChangeSceneByLevelTypeService);
            
            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();
            
            container.RegisterAsSingle(CreateMainMenuPresenterFactory);
            
            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
            
            container.RegisterAsSingle(CreateMainMenuPopupService);
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        private static MainMenuSwitcherSceneService CreateChangeSceneByLevelTypeService(DIContainer c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new MainMenuSwitcherSceneService(configsProviderService, sceneSwitcherService, coroutinesPerformer);
        }
        
        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRoot = resourcesAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRoot);
        }

        public static MainMenuPresenterFactory CreateMainMenuPresenterFactory(DIContainer c)
        {
            return new MainMenuPresenterFactory(c);
        }

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();

            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = c
                .Resolve<MainMenuPresenterFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
        }
    }
}
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Factories.UI;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Abilities;
using _Project.Develop.Runtime.Meta.Features.Levels;
using _Project.Develop.Runtime.UI.MainMenu.ShopAbilitiesPopup;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresentersFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<ILevelConfigProviderService>(),
                _container.Resolve<MainMenuPopupService>()
            );
        }

        public ShopAbilityItemPresenter CreateShopAbilityItemPresenter(ShopAbilityItemView view, ShopAbilityConfig config)
        {
            return new ShopAbilityItemPresenter(
                view,
                config,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                _container.Resolve<AbilitiesShopService>());
        }

        public ShopAbilitiesPopupPresenter CreateShopAbilitiesPopupPresenter(ShopAbilitiesPopupView view)
        {
            return new ShopAbilitiesPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                this,
                _container.Resolve<ViewsFactory>(),
                _container.Resolve<AbilitiesShopService>());
        }
    }
}

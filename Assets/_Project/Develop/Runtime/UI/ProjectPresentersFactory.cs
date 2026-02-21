
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core.TestPopup;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.UI.LevelsMenuPopup;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public SingleCurrencyPresenter CreateCurrencyPresenter(
            IconTextView iconTextView,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType
        )
        {
            return new SingleCurrencyPresenter(
                currency,
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                iconTextView);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(), 
                view);
        }

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        {
            return new TestPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view);
        }
        
        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, LevelTypes levelType)
        {
            return new LevelTilePresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<SceneSwitcherService>(),
                view,
                levelType,
                _container.Resolve<ConfigsProviderService>()
                );
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter(LevelsMenuPopupView view)
        {
            return new LevelsMenuPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<ConfigsProviderService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
        
        public GameplayOutcomePopupPresenter CreateGameplayOutcomePopupPresenter(GameplayOutcomePopupView view, string text)
        {
            return new GameplayOutcomePopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                text,
                _container.Resolve<SceneSwitcherService>());
        }
    }
}
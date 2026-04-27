using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.Levels;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Statistics;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;

        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly ILevelConfigProviderService _levelConfigProviderService;

        public readonly MainMenuPopupService _mainMenuPopupService;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory projectPresentersFactory, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer, 
            ILevelConfigProviderService levelConfigProviderService, 
            MainMenuPopupService mainMenuPopupService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _levelConfigProviderService = levelConfigProviderService;
            _mainMenuPopupService = mainMenuPopupService;
        }

        public void Initialize()
        {
            _screen.StartMenuButtonClicked += OnStartMenuButtonClicked;
            _screen.AbilitiesPopupButtonClicked += OnAbilitiesPopupButtonClicked;

            CreateWallet();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.StartMenuButtonClicked -= OnStartMenuButtonClicked;
            _screen.AbilitiesPopupButtonClicked -= OnAbilitiesPopupButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);
            StatisticsPresenter statisticsPresenter = _projectPresentersFactory.CreateStatisticsPresenter(_screen.StatisticsView);

            _childPresenters.Add(walletPresenter);
            _childPresenters.Add(statisticsPresenter);
        }

        private void OnStartMenuButtonClicked()
        {
            LevelConfig levelConfig = _levelConfigProviderService.Get();
            
            _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(
                    Scenes.Gameplay, 
                    new GameplayInputArgs(
                        levelConfig.Reward.Type,
                        levelConfig.Reward.Value,
                        levelConfig.TowerMaxHealth,
                        levelConfig.StageConfigs)));
        }

        private void OnAbilitiesPopupButtonClicked()
        {
            _mainMenuPopupService.OpenShopAbilitiesPopup();
        }
    }
}

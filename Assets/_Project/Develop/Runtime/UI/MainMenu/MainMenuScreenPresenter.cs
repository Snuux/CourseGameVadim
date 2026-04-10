using System.Collections.Generic;
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
        private readonly ILevelProviderService _levelProviderService;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen,
            ProjectPresentersFactory projectPresentersFactory, 
            SceneSwitcherService sceneSwitcherService, 
            ICoroutinesPerformer coroutinesPerformer, 
            ILevelProviderService levelProviderService)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _levelProviderService = levelProviderService;
        }

        public void Initialize()
        {
            _screen.StartMenuButtonClicked += OnStartMenuButtonClicked;

            CreateWallet();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.StartMenuButtonClicked -= OnStartMenuButtonClicked;

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
            _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(
                    Scenes.Gameplay, 
                    new GameplayInputArgs(_levelProviderService.Get()))
            );
        }
    }
}

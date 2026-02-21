using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainMenuPopupService _popupService;

        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        
        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView screen, 
            ProjectPresentersFactory projectPresentersFactory, 
            MainMenuPopupService popupService, 
            PlayerDataProvider playerDataProvider, 
            ICoroutinesPerformer coroutinesPerformer)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _popupService = popupService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Initialize()
        {
            _screen.OpenLevelsMenuButtonClicked += OnOpenLevelsMenuButtonClicked;
            _screen.SaveButtonClicked += OnSaveButtonClicked;
            _screen.LoadButtonClicked += OnLoadButtonClicked;
            _screen.ClearButtonClicked += OnClearSaveButtonClicked;
            
            
            CreateWallet();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.OpenLevelsMenuButtonClicked -= OnOpenLevelsMenuButtonClicked;
            _screen.SaveButtonClicked -= OnSaveButtonClicked;
            _screen.LoadButtonClicked -= OnLoadButtonClicked;
            _screen.ClearButtonClicked -= OnClearSaveButtonClicked;
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();
        }

        private void OnOpenLevelsMenuButtonClicked() => 
            _popupService.OpenLevelsMenuPopupPresenter();
        
        private void OnSaveButtonClicked() => 
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
        
        private void OnLoadButtonClicked() => 
            _coroutinesPerformer.StartPerform(_playerDataProvider.Load());
        
        private void OnClearSaveButtonClicked() => 
            _coroutinesPerformer.StartPerform(_playerDataProvider.Remove());

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);

            _childPresenters.Add(walletPresenter);
        }
    }
}
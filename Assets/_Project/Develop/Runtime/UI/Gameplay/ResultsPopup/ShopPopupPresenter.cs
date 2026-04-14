using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class ShopPopupPresenter : PopupPresenterBase
    {
        private readonly ShopPopupView _screen;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly StageProviderService _stageProviderService;
        
        private readonly List<IPresenter> _childPresenters = new();

        public ShopPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            ShopPopupView screen,
            ProjectPresentersFactory projectPresentersFactory,
            StageProviderService stageProviderService) : base(coroutinesPerformer)
        {
            _screen = screen;
            _projectPresentersFactory = projectPresentersFactory;
            _stageProviderService = stageProviderService;
        }

        protected override PopupViewBase PopupView => _screen;

        public override void Initialize()
        {
            base.Initialize();
            
            _screen.ContinueButtonClicked += OnContinueButtonClicked;
            
            CreateWallet();
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _screen.ContinueButtonClicked -= OnContinueButtonClicked;
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }
        
        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _screen.ContinueButtonClicked -= OnContinueButtonClicked;
        }
        
        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_screen.WalletView);
            
            _childPresenters.Add(walletPresenter);
        }

        private void OnContinueButtonClicked()
        {
            _stageProviderService.SetShopStateCompleted();
            
            OnCloseRequest();
        }
    }
}
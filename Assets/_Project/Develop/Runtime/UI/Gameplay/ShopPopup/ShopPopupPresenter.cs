using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Factories.UI;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.ResultsPopup;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Gameplay.ShopPopup
{
    public class ShopPopupPresenter : PopupPresenterBase
    {
        private readonly ShopPopupView _view;
        private readonly StageProviderService _stageProviderService;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly ShopService _shopService;
        private readonly GameplayPopupService _gameplayPopupService;
        
        private readonly List<IPresenter> _childPresenters = new();
        private readonly List<ShopItemPresenter> _shopItemPresenters = new();

        public ShopPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            ShopPopupView view,
            StageProviderService stageProviderService, 
            GameplayPresentersFactory gameplayPresentersFactory, 
            ShopService shopService, 
            ViewsFactory viewsFactory, 
            GameplayPopupService gameplayPopupService) : base(coroutinesPerformer)
        {
            _view = view;
            _stageProviderService = stageProviderService;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _shopService = shopService;
            _viewsFactory = viewsFactory;
            _gameplayPopupService = gameplayPopupService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            foreach (ShopItemConfig shopItemConfig in _shopService.AvailableShopItemsConfigs)
            {
                ShopItemView shopItemView = _viewsFactory.Create<ShopItemView>(ViewIDs.ShopItemView);
                _view.ShopItemsListView.Add(shopItemView);
                ShopItemPresenter shopItemPresenter = _gameplayPresentersFactory.CreateShopItemPresenter(shopItemView, shopItemConfig.ItemType);
                shopItemPresenter.Clicked += OnShopItemClicked;
                shopItemPresenter.Initialize();
                _shopItemPresenters.Add(shopItemPresenter);
            }
            
            _view.ContinueButtonClicked += OnContinueButtonClicked;
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        private void OnShopItemClicked(ShopItemPresenter shopItemPresenter)
        {
            OnCloseRequest();
            _gameplayPopupService.OpenPlacePopup(() => _gameplayPopupService.OpenShopPopup());
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.ContinueButtonClicked -= OnContinueButtonClicked;

            foreach (ShopItemPresenter shopItemPresenter in _shopItemPresenters)
            {
                _view.ShopItemsListView.Remove(shopItemPresenter.View);
                _viewsFactory.Release(shopItemPresenter.View);
                shopItemPresenter.Clicked -= OnShopItemClicked;
                shopItemPresenter.Dispose();
            }
            
            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }
        
        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.ContinueButtonClicked -= OnContinueButtonClicked;
        }

        private void OnContinueButtonClicked()
        {
            _stageProviderService.SetShopStateCompleted();
            
            OnCloseRequest();
        }
    }
}
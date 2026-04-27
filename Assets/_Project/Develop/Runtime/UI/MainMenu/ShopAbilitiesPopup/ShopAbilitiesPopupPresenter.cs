using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Factories.UI;
using _Project.Develop.Runtime.Meta.Features.Abilities;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.MainMenu;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.MainMenu.ShopAbilitiesPopup
{
    public class ShopAbilitiesPopupPresenter : PopupPresenterBase
    {
        private readonly ShopAbilitiesPopupView _view;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly AbilitiesShopService _abilitiesShopService;

        private readonly List<ShopAbilityItemPresenter> _shopAbilityItemPresenters = new();

        public ShopAbilitiesPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            ShopAbilitiesPopupView view,
            MainMenuPresentersFactory mainMenuPresentersFactory,
            ViewsFactory viewsFactory,
            AbilitiesShopService abilitiesShopService) : base(coroutinesPerformer)
        {
            _view = view;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
            _viewsFactory = viewsFactory;
            _abilitiesShopService = abilitiesShopService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            foreach (ShopAbilityConfig shopAbilityConfig in _abilitiesShopService.AvailableAbilitiesConfigs)
            {
                ShopAbilityItemView shopAbilityItemView = _viewsFactory.Create<ShopAbilityItemView>(ViewIDs.ShopAbilityItemView);
                _view.ShopAbilitiesListView.Add(shopAbilityItemView);

                ShopAbilityItemPresenter shopAbilityItemPresenter =
                    _mainMenuPresentersFactory.CreateShopAbilityItemPresenter(shopAbilityItemView, shopAbilityConfig);

                shopAbilityItemPresenter.Initialize();
                _shopAbilityItemPresenters.Add(shopAbilityItemPresenter);
            }

            _view.ReturnClicked += OnReturnClicked;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.ReturnClicked -= OnReturnClicked;

            foreach (ShopAbilityItemPresenter shopAbilityItemPresenter in _shopAbilityItemPresenters)
            {
                _view.ShopAbilitiesListView.Remove(shopAbilityItemPresenter.View);
                _viewsFactory.Release(shopAbilityItemPresenter.View);
                shopAbilityItemPresenter.Dispose();
            }

            _shopAbilityItemPresenters.Clear();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.ReturnClicked -= OnReturnClicked;
        }

        private void OnReturnClicked() => OnCloseRequest();
    }
}

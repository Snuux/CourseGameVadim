using System;
using _Project.Develop.Runtime.Factories.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.MainMenu.ShopAbilitiesPopup;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;
        private readonly MainMenuPresentersFactory _mainMenuPresentersFactory;

        public MainMenuPopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory,
            MainMenuUIRoot uiRoot,
            MainMenuPresentersFactory mainMenuPresentersFactory)
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
            _mainMenuPresentersFactory = mainMenuPresentersFactory;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
        
        public ShopAbilitiesPopupPresenter OpenShopAbilitiesPopup(Action closedCallback = null)
        {
            ShopAbilitiesPopupView view = ViewsFactory.Create<ShopAbilitiesPopupView>(ViewIDs.ShopAbilitiesPopupView, PopupLayer);

            ShopAbilitiesPopupPresenter popup = _mainMenuPresentersFactory.CreateShopAbilitiesPopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }
    }
}

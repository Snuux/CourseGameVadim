using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.UI.AbilitySelectPopup;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.ResultsPopup;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly GameplayUIRoot _uiRoot;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;

        public GameplayPopupService(ViewsFactory viewsFactory, ProjectPresentersFactory presentersFactory,
            GameplayUIRoot uiRoot, GameplayPresentersFactory gameplayPresentersFactory) : base(viewsFactory, 
            presentersFactory)
        {
            _uiRoot = uiRoot;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;

        public WinPopupPresenter OpenWinPopup(Action closedCallback = null)
        {
            WinPopupView view = ViewsFactory.Create<WinPopupView>(ViewIDs.WinPopup, PopupLayer);

            WinPopupPresenter popup = _gameplayPresentersFactory.CreateWinPopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }
        
        public DefeatPopupPresenter OpenDefeatPopup(Action closedCallback = null)
        {
            DefeatPopupView view = ViewsFactory.Create<DefeatPopupView>(ViewIDs.DefeatPopup, PopupLayer);

            DefeatPopupPresenter popup = _gameplayPresentersFactory.CreateDefeatPopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);
            
            return popup;
        }

        public AbilitySelectPopupPresenter OpenAbilityPopupPresenter(Entity entity, Action closedCallback = null)
        {
            AbilitySelectPopupView view 
                = ViewsFactory.Create<AbilitySelectPopupView>(ViewIDs.AbilitySelectPopup, PopupLayer);
            AbilitySelectPopupPresenter popup 
                = _gameplayPresentersFactory.CreateAbilitySelectPopupPresenter(view, entity);
            
            OnPopupCreated(popup, view, closedCallback);

            return popup;
        }
    }
}
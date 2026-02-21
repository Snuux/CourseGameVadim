using _Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupService
    {
        private readonly GameplayUIRoot _uiRoot;

        public GameplayPopupService(
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory, GameplayUIRoot uiRoot) 
            : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform _popupLayer => _uiRoot.PopupsLayer;
    }
}
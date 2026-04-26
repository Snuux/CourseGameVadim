using System;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.ShopPopup;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class ShopPopupView : PopupViewBase
    {
        public event Action ContinueButtonClicked;
        
        [field: SerializeField] public ShopItemsListView ShopItemsListView { get; private set; }

        [SerializeField] private Button _continueButton;
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnButtonClicked);
        }
        
        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _continueButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => ContinueButtonClicked?.Invoke();
    }
}
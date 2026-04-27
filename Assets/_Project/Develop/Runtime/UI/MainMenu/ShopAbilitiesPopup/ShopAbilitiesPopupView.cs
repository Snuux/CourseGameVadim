using System;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.ShopPopup;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.MainMenu.ShopAbilitiesPopup
{
    public class ShopAbilitiesPopupView : PopupViewBase
    {
        public event Action ReturnClicked;
        
        [field: SerializeField] public ShopAbilitiesListView ShopAbilitiesListView { get; private set; }

        [SerializeField] private Button _returnButton;
        
        private void OnEnable()
        {
            _returnButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _returnButton.onClick.RemoveListener(OnButtonClicked);
        }
        
        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _returnButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => ReturnClicked?.Invoke();
    }
}
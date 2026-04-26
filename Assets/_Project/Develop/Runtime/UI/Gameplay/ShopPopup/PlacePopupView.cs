using System;
using _Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Gameplay.ShopPopup
{
    public class PlacePopupView : PopupViewBase
    {
        public event Action DeclineButtonClicked;

        [SerializeField] private Button _declineButton;

        private void OnEnable()
        {
            _declineButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _declineButton.onClick.RemoveListener(OnButtonClicked);
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _declineButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() => DeclineButtonClicked?.Invoke();
    }
}
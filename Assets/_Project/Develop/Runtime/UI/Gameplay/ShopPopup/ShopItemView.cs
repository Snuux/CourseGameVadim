using System;
using _Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Gameplay.ShopPopup
{
    public class ShopItemView : MonoBehaviour, IView, IShowableView
    {
        public event Action Clicked;

        [SerializeField] private Image _image;
        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _price;

        [SerializeField] private Button _button;

        public void Initialize(Sprite icon, Sprite currencyIcon, int price)
        {
            _image.sprite = icon;
            _currencyIcon.sprite = currencyIcon;
            _price.text = $"${price}";
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            Clicked?.Invoke();
        }

        public Tween Show()
        {
            transform.DOKill();

            return transform
                .DOScale(1, 0.1f)
                .From(0)
                .SetUpdate(true)
                .Play();
        }

        public Tween Hide()
        {
            transform.DOKill();

            return DOTween.Sequence();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}
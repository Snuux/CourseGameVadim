using System;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.ResultsPopup;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay.ShopPopup
{
    public class ShopItemPresenter : IPresenter
    {
        public event Action<ShopItemPresenter> Clicked;
        
        private readonly ShopItemView _view;
        private readonly ShopItemTypes _shopItemType;
        private readonly ShopService _shopService;

        private readonly ShopItemConfig _shopItemConfig;
        private readonly ShopItemViewsConfig _shopItemViewsConfig;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        public ShopItemPresenter(
            ShopItemView view,
            ShopItemTypes shopItemType,
            CurrencyIconsConfig currencyIconsConfig,
            ShopItemViewsConfig shopItemViewsConfig,
            ShopService shopService)
        {
            _shopService = shopService;
            _view = view;
            _shopItemType = shopItemType;
            _currencyIconsConfig = currencyIconsConfig;
            _shopItemViewsConfig = shopItemViewsConfig;

            _shopItemConfig = _shopService.AvailableShopItemsConfigs
                .First(s => s.ItemType == shopItemType);
        }
        
        public ShopItemView View => _view;
        
        public void Initialize()
        {
            _view.Initialize(
                _shopItemViewsConfig.GetShopItemViewData(_shopItemType).Sprite,
                _currencyIconsConfig.GetSpriteFor(_shopItemConfig.CurrencyType),
                _shopItemConfig.Price);

            _view.Clicked += OnShopItemClick;
        }

        public void Dispose()
        {
            _view.Clicked -= OnShopItemClick;
        }

        private void OnShopItemClick()
        {
            _shopService.Purchase(_shopItemConfig.ItemType);
            
            Clicked?.Invoke(this);
        }
    }
}
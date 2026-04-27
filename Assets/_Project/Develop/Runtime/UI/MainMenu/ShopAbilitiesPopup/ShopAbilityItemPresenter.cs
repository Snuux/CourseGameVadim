using System;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Meta.Features.Abilities;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.MainMenu.ShopAbilitiesPopup
{
    public class ShopAbilityItemPresenter : IPresenter
    {
        public event Action<ShopAbilityItemPresenter> Clicked;

        private readonly ShopAbilityItemView _view;
        private readonly ShopAbilityConfig _shopAbilityConfig;
        private readonly AbilitiesShopService _abilitiesShopService;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        public ShopAbilityItemPresenter(
            ShopAbilityItemView view,
            ShopAbilityConfig shopAbilityConfig,
            CurrencyIconsConfig currencyIconsConfig,
            AbilitiesShopService abilitiesShopService)
        {
            _view = view;
            _shopAbilityConfig = shopAbilityConfig;
            _currencyIconsConfig = currencyIconsConfig;
            _abilitiesShopService = abilitiesShopService;
        }

        public ShopAbilityItemView View => _view;

        public void Initialize()
        {
            _view.Initialize(
                _shopAbilityConfig.AbilityConfig.Icon,
                _currencyIconsConfig.GetSpriteFor(_shopAbilityConfig.CurrencyType),
                _shopAbilityConfig.Price,
                _shopAbilityConfig.AbilityConfig.Name,
                _shopAbilityConfig.AbilityConfig.Description);

            if (_abilitiesShopService.HasAbility(_shopAbilityConfig.ID))
                _view.SetBought();

            _view.Clicked += OnShopItemClick;
            _abilitiesShopService.Bought += OnAbilityBought;
        }

        public void Dispose()
        {
            _view.Clicked -= OnShopItemClick;
            _abilitiesShopService.Bought -= OnAbilityBought;
        }

        private void OnShopItemClick()
        {
            _abilitiesShopService.TryToPurchase(_shopAbilityConfig.ID);

            Clicked?.Invoke(this);
        }

        private void OnAbilityBought(string abilityId)
        {
            if (abilityId != _shopAbilityConfig.ID)
                return;

            _view.SetBought();
        }
    }
}

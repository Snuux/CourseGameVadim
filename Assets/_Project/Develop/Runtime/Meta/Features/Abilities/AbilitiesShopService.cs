using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features.Abilities
{
    public class AbilitiesShopService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly WalletService _walletService;
        private readonly Dictionary<string, bool> _abilities = new();

        private ShopAbilitiesConfig _shopAbilitiesConfig;

        public event Action<string> Bought;

        public AbilitiesShopService(
            PlayerDataProvider playerDataProvider,
            WalletService walletService)
        {
            _walletService = walletService;

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public IReadOnlyList<ShopAbilityConfig> AvailableAbilitiesConfigs => _shopAbilitiesConfig.Configs;

        public void Setup(ShopAbilitiesConfig shopAbilitiesConfig)
        {
            _shopAbilitiesConfig = shopAbilitiesConfig;

            _abilities.Clear();

            foreach (ShopAbilityConfig abilityConfig in _shopAbilitiesConfig.Configs)
                _abilities.Add(abilityConfig.ID, false);
        }

        public bool HasAbility(string abilityId) => _abilities[abilityId];

        public bool CanPurchase(string abilityId)
        {
            if (HasAbility(abilityId))
                return false;

            ShopAbilityConfig shopAbilityConfig = _shopAbilitiesConfig.GetConfigBy(abilityId);

            return _walletService.Enough(shopAbilityConfig.CurrencyType, shopAbilityConfig.Price);
        }

        public bool TryToPurchase(string abilityId)
        {
            if (HasAbility(abilityId))
                return false;

            ShopAbilityConfig shopAbilityConfig = _shopAbilitiesConfig.GetConfigBy(abilityId);

            if (_walletService.Enough(shopAbilityConfig.CurrencyType, shopAbilityConfig.Price) == false)
            {
                Debug.Log($"Not enough {shopAbilityConfig.CurrencyType} to purchase ability {abilityId}. Need {shopAbilityConfig.Price}.");
                return false;
            }

            _walletService.Spend(shopAbilityConfig.CurrencyType, shopAbilityConfig.Price);
            _abilities[abilityId] = true;

            Bought?.Invoke(abilityId);
            return true;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (string abilityId in _abilities.Keys.ToList())
                _abilities[abilityId] = false;

            if (data.AbilitiesData == null)
                return;

            foreach (KeyValuePair<string, bool> abilityData in data.AbilitiesData)
            {
                if (_abilities.ContainsKey(abilityData.Key))
                    _abilities[abilityData.Key] = abilityData.Value;
            }
        }

        public void WriteTo(PlayerData data)
        {
            data.AbilitiesData = _abilities.ToDictionary(
                pair => pair.Key,
                pair => pair.Value);
        }
    }
}

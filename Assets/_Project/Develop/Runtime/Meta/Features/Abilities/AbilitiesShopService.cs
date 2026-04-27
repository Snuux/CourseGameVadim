using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Abilities
{
    public class AbilitiesShopService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly WalletService _walletService;
        private readonly ShopAbilitiesConfig _shopAbilitiesConfig;
        private readonly Dictionary<string, ReactiveVariable<bool>> _abilities = new();

        public event Action<string> Bought;

        public AbilitiesShopService(
            PlayerDataProvider playerDataProvider,
            WalletService walletService,
            ShopAbilitiesConfig shopAbilitiesConfig)
        {
            _walletService = walletService;
            _shopAbilitiesConfig = shopAbilitiesConfig;

            foreach (ShopAbilityConfig abilityConfig in _shopAbilitiesConfig.Configs)
                _abilities.Add(abilityConfig.ID, new ReactiveVariable<bool>());

            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public IReadOnlyList<ShopAbilityConfig> AvailableAbilitiesConfigs => _shopAbilitiesConfig.Configs;

        public bool HasAbility(string abilityId) => _abilities[abilityId].Value;

        public bool CanPurchase(string abilityId)
        {
            if (HasAbility(abilityId))
                return false;

            ShopAbilityConfig shopAbilityConfig = _shopAbilitiesConfig.GetConfigBy(abilityId);

            return _walletService.Enough(shopAbilityConfig.CurrencyType, shopAbilityConfig.Price);
        }

        public bool TryToPurchase(string abilityId)
        {
            if (CanPurchase(abilityId) == false)
                return false;

            ShopAbilityConfig shopAbilityConfig = _shopAbilitiesConfig.GetConfigBy(abilityId);

            _walletService.Spend(shopAbilityConfig.CurrencyType, shopAbilityConfig.Price);
            _abilities[abilityId].Value = true;

            Bought?.Invoke(abilityId);
            return true;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (ReactiveVariable<bool> ability in _abilities.Values)
                ability.Value = false;

            if (data.AbilitiesData == null)
                return;

            foreach (KeyValuePair<string, bool> abilityData in data.AbilitiesData)
            {
                if (_abilities.TryGetValue(abilityData.Key, out ReactiveVariable<bool> ability))
                    ability.Value = abilityData.Value;
            }
        }

        public void WriteTo(PlayerData data)
        {
            data.AbilitiesData = _abilities.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Value);
        }
    }
}

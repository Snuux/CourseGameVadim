using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Shop/New ShopAbilitiesConfig", fileName = "ShopAbilitiesConfig")]
    public class ShopAbilitiesConfig : ScriptableObject
    {
        [SerializeField] private List<ShopAbilityConfig> _configs;

        public IReadOnlyList<ShopAbilityConfig> Configs => _configs;

        public ShopAbilityConfig GetConfigBy(string id) =>
            _configs.First(config => config.ID == id);
    }

    [Serializable]
    public class ShopAbilityConfig
    {
        public string ID => AbilityConfig.ID;
        [field: SerializeField] public AbilityConfig AbilityConfig { get; private set; }
        [field: SerializeField] public CurrencyType CurrencyType { get; private set; } = CurrencyType.Gold;
        [field: SerializeField] public int Price { get; private set; } = 50;

        [field: SerializeField] public EntitiesFilters ApplyToType { get; private set; }
        [field: SerializeField] public AbilityActivationTypes ActivateOnType { get; private set; }
    }
}

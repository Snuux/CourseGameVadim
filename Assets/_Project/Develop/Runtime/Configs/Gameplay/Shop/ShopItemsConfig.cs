using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Shop
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Shop/New ShopItemsConfig", fileName = "ShopItemsConfig")]
    public class ShopItemsConfig : ScriptableObject
    {
        [SerializeField] private List<ShopItemConfig> _configs;

        public IReadOnlyList<ShopItemConfig> Configs => _configs;
        
        public ShopItemConfig GetPriceFor(ShopItemTypes itemType) =>
            _configs.First(config => config.ItemType == itemType);
    }
    
    [Serializable]
    public class ShopItemConfig
    {
        [field: SerializeField] public ShopItemTypes ItemType { get; private set; } = ShopItemTypes.Mine;
        [field: SerializeField] public CurrencyType CurrencyType { get; private set; } = CurrencyType.Gold;
        [field: SerializeField] public int Price { get; private set; } = 50;
    }
}
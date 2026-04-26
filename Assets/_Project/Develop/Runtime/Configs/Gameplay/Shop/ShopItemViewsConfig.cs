using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Shop
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Shop/ShopItemsViewConfig", fileName = "ShopItemsViewConfig")]
    public class ShopItemViewsConfig : ScriptableObject
    {
        [SerializeField] private List<ShopItemViewConfig> _shopItemViewData;

        public ShopItemViewConfig GetShopItemViewData(ShopItemTypes statType) =>
            _shopItemViewData.First(s => s.Type == statType);
    }
    
    [Serializable]
    public class ShopItemViewConfig
    {
        [field: SerializeField] public ShopItemTypes Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
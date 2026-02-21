using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/CurrencyIconConfig", fileName = "CurrencyIconConfig")]
    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyToSpriteConfig> _configs;

        public Sprite GetSpriteFor(CurrencyTypes currencyType)
            => _configs.First(config => config.Type == currencyType).Sprite;

        [Serializable]
        private class CurrencyToSpriteConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
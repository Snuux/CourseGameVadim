using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/GameEndIconsConfig", fileName = "GameEndIconsConfig")]
    public class GameEndIconsConfig : ScriptableObject
    {
        [SerializeField] private List<GameEndToSpriteConfig> _configs;

        public Sprite GetSpriteFor(GameEndTypes currencyType)
            => _configs.First(config => config.Type == currencyType).Sprite;

        [Serializable]
        private class GameEndToSpriteConfig
        {
            [field: SerializeField] public GameEndTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Configs.Meta.Statistics;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.ConfigsManagment
{
    public class ResourcesConfigsLoader : IConfigsLoader
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly Dictionary<Type, string> _configsResourcesPaths = new()
        {
            {typeof(StartWalletConfig), "Configs/Meta/Wallet/StartWalletConfig" },
            {typeof(CurrencyIconsConfig), "Configs/Meta/Wallet/CurrencyIconsConfig" },
            
            {typeof(StartStatisticsConfig), "Configs/Meta/Statistics/StartStatisticsConfig" },
            {typeof(RecordIconsConfig), "Configs/Meta/Statistics/RecordIconsConfig" },
            
            {typeof(LevelsListConfig), "Configs/Gameplay/Levels/LevelsListConfig" },
            
            {typeof(TowerConfig), "Configs/Gameplay/Entities/Characters/TowerConfig" },
            {typeof(ArcherConfig), "Configs/Gameplay/Entities/Characters/ArcherConfig" },
            {typeof(GhostConfig), "Configs/Gameplay/Entities/Characters/GhostConfig" },
            
            {typeof(InstantDamageZoneConfig), "Configs/Gameplay/Entities/Characters/InstantDamageZoneConfig" },
            {typeof(CursorAttackerConfig), "Configs/Gameplay/Entities/CursorAttackerConfig" },
            {typeof(MineConfig), "Configs/Gameplay/Entities/Characters/MineConfig" },
            {typeof(TurretConfig), "Configs/Gameplay/Entities/Characters/TurretConfig" },
            {typeof(PuddleConfig), "Configs/Gameplay/Entities/Characters/PuddleConfig" },
            
            {typeof(SpawnerEnemiesConfig), "Configs/Gameplay/Entities/SpawnerEnemiesConfig" },
            {typeof(ShopItemsConfig), "Configs/Gameplay/Shop/ShopItemsConfig" },
            {typeof(ShopItemViewsConfig), "Configs/Gameplay/Shop/ShopItemViewsConfig" },
            
            {typeof(ShopAbilitiesConfig), "Configs/Meta/Abilities/ShopAbilitiesConfig" },
        };

        public ResourcesConfigsLoader(ResourcesAssetsLoader resources)
        {
            _resources = resources;
        }

        public IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded)
        {
            Dictionary<Type, object> loadedConfigs = new();

            foreach (KeyValuePair<Type, string> configResourcesPath in _configsResourcesPaths)
            {
                ScriptableObject config = _resources.Load<ScriptableObject>(configResourcesPath.Value);
                loadedConfigs.Add(configResourcesPath.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Loot;
using _Project.Develop.Runtime.Configs.Meta.Stats;
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
            {typeof(LevelsListConfig), "Configs/Gameplay/Levels/LevelsListConfig" },
            {typeof(HeroConfig), "Configs/Gameplay/Entities/Characters/HeroConfig" },
            {typeof(AbilitiesConfigsContainer), "Configs/Gameplay/Abilities/AbilitiesConfigsContainer" },
            {typeof(ExperienceForUpgradeConfig), "Configs/Gameplay/ExperienceForUpgradeConfig" },
            {typeof(LootListConfig), "Configs/Gameplay/Loot/LootListConfig" },
            {typeof(StatsViewConfig), "Configs/Meta/Stats/StatsViewConfig" },
            {typeof(PlayerStatsUpgradeConfig), "Configs/Meta/Stats/PlayerStatsUpgradeConfig" },
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
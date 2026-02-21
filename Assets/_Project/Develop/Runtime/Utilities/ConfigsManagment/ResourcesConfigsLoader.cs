using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
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
            { typeof(LevelsConfig), "Configs/Levels/LevelsConfig" },
            { typeof(StartCurrenciesConfig), "Configs/Currencies/StartCurrenciesConfig" },
            { typeof(ResetPriceConfig), "Configs/Currencies/ResetPriceConfig" },
            { typeof(CurrencyIconsConfig), "Configs/Currencies/CurrencyIconsConfig" }
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
                ScriptableObject config = _resources
                    .Load<ScriptableObject>(configResourcesPath.Value);

                loadedConfigs.Add(configResourcesPath.Key, config);
                yield return null;
            }

            onConfigsLoaded?.Invoke(loadedConfigs);
        }
    }
}
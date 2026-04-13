using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features.Levels
{
    public class RandomLevelConfigConfigProviderService : ILevelConfigProviderService
    {
        private readonly ConfigsProviderService _configsProviderService;
        private List<LevelConfig> _levelConfigs;

        public RandomLevelConfigConfigProviderService(ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;

            LevelsListConfig levelsListConfig = _configsProviderService.GetConfig<LevelsListConfig>();
            _levelConfigs = new List<LevelConfig>(levelsListConfig.Levels);
        }

        public LevelConfig Get()
        {
            return _levelConfigs[Random.Range(0, _levelConfigs.Count)];
        }
    }
}
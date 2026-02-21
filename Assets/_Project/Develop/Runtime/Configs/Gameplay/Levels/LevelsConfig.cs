using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/LevelsConfig", fileName = "LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfigByType> _configs;

        public LevelConfig GetLevelConfigBy(LevelTypes levelType) 
            => _configs.First(config => config.LevelType == levelType).LevelConfig;

        public IReadOnlyList<LevelConfigByType> Levels => _configs;

        [Serializable]
        public class LevelConfigByType
        {
            [field: SerializeField] public LevelTypes LevelType { get; private set; }
            [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/LevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig1> _levels;

        public IReadOnlyList<LevelConfig1> Levels => _levels;

        public LevelConfig1 GetBy(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            return _levels[levelIndex];
        }
    }
}
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<StageConfig> _stageConfigs;

        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
        
        [field: SerializeField] public float TowerMaxHealth { get; private set; } = 3;
        
        [field: SerializeField] public StartWalletConfig.Currency Reward { get; private set; }
    }
}
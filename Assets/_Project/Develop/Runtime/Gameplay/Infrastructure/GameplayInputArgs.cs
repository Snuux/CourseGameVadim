using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(CurrencyType rewardCurrencyType, int rewardPrice, float maxTowerHealth, IReadOnlyList<StageConfig> stageConfigs)
        {
            RewardCurrencyType = rewardCurrencyType;
            RewardPrice = rewardPrice;
            TowerMaxHealth = maxTowerHealth;
            StageConfigs = stageConfigs;
        }
        
        public float TowerMaxHealth { get; }
        public CurrencyType RewardCurrencyType { get; }
        public int RewardPrice { get; }
        public IReadOnlyList<StageConfig> StageConfigs { get; }
    }
}

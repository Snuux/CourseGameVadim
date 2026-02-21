using _Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels   
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int Length { get; private set; }
        
        [field: SerializeField] public CurrencyConfig WinReward { get; private set; }
        [field: SerializeField] public CurrencyConfig DefeatPenalty { get; private set; }
    }
}
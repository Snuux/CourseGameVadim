using _Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/ResetPriceConfig", fileName = "ResetPriceConfig")]
    public class ResetPriceConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencyConfig Price { get; private set; }
    }
}
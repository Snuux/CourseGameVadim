using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Levels
{
    [CreateAssetMenu(menuName = "Configs/ResetPriceConfig", fileName = "ResetPriceConfig")]
    public class ResetPriceConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Abilities/New HealAbilityConfig", fileName = "HealAbilityConfig")]
    public class HealAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int HealPercent { get; private set; } = 3;
    }
}
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Abilities/New DamageIncreaseAbilityConfig", fileName = "DamageIncreaseAbilityConfig")]
    public class DamageIncreaseAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public float DamagePercent { get; private set; } = 3;
    }
}
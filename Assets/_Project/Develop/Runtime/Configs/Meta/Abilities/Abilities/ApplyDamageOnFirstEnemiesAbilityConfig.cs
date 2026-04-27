using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Abilities/New ApplyDamageOnFirstEnemiesAbilityConfig", fileName = "ApplyDamageOnFirstEnemiesAbilityConfig")]
    public class ApplyDamageOnFirstEnemiesAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public float DamagePercent { get; private set; } = 3;
        [field: SerializeField] public int EnemiesCount { get; private set; } = 3;
    }
}
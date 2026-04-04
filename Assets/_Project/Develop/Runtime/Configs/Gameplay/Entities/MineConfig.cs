using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewMineConfig", fileName = "MineConfig")]
    public class MineConfig : EntityConfig
    {
        [field: SerializeField] public float TriggerRadius { get; private set; } = 3f;
        [field: SerializeField] public float AttackRadius { get; private set; } = 4f;
        [field: SerializeField] public float AttackDamage { get; private set; } = 1f;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = .1f;
    }
}
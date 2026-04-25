using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewArcherConfig", fileName = "ArcherConfig")]
    public class ArcherConfig : EntityConfig
    {
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 9;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 900;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 3;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = .1f;
        
        [field: SerializeField, Min(0)] public float AttackDamage { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float AttackTriggerRadius { get; private set; } = .8f;

        [field: SerializeField, Min(0)] public float AttackProcessTime { get; private set; } = 1.5f;
        [field: SerializeField, Min(0)] public float AttackDelayTime { get; private set; } = 0.75f;
        [field: SerializeField, Min(0)] public float AttackCooldown { get; private set; } = 1f;
        
        [field: SerializeField, Min(0)] public float SpawnProcessTime { get; private set; } = 2;
    }
}
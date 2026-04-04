using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewGhostConfig", fileName = "GhostConfig")]
    public class GhostConfig : EntityConfig
    {
        [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 9;
        [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 900;
        [field: SerializeField, Min(0)] public float MaxHealth { get; private set; } = 3;
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = .1f;
        [field: SerializeField, Min(0)] public float ExplosionDamage { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float ExplosionRadius { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float DistanceForAreaAttack { get; private set; } = .8f;
    }
}
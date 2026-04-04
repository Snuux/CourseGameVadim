using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewAreaProjectileConfig", fileName = "AreaProjectileConfig")]
    public class AreaProjectileConfig : EntityConfig
    {
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = .1f;
    }
}
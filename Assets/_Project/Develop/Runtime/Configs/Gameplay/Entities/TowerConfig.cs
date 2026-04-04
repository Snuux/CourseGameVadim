using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewTowerConfig", fileName = "TowerConfig")]
    public class TowerConfig : EntityConfig
    {
        [field: SerializeField] public float BodyContactDamage { get; private set; } = 999;
        [field: SerializeField] public float DeathProcessTime { get; private set; } = .1f;
    }
}
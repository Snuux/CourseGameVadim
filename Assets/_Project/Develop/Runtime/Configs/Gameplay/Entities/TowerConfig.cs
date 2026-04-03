using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewTowerConfig", fileName = "TowerConfig")]
    public class TowerConfig : EntityConfig
    {
        [field: SerializeField] public string PrefabPath { get; private set; } = "Entities/Tower";
        [field: SerializeField] public float BodyContactDamage { get; private set; } = 999;
        [field: SerializeField] public float DeathProcessTime { get; private set; } = .1f;
        [field: SerializeField] public float AttackRadius { get; private set; } = 4f;
        [field: SerializeField] public float AttackDamage { get; private set; } = 1f;
    }
}
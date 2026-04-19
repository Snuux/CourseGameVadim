using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Loot
{
    [CreateAssetMenu(fileName = "HealthLootConfig", menuName = "Configs/Gameplay/Loot/New HealthLootConfig")]
    public class HealthLootConfig : LootConfig
    {
        [field: SerializeField] public float Health { get; private set; }
    }
}
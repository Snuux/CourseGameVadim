using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Loot
{
    [CreateAssetMenu(fileName = "ExperienceLootConfig", menuName = "Configs/Gameplay/Loot/New ExperienceLootConfig")]
    public class ExperienceLootConfig : LootConfig
    {
        [field: SerializeField] public int Experience { get; private set; }
    }
}
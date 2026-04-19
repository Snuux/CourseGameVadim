using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Loot
{
    [CreateAssetMenu(fileName = "GoldLootConfig", menuName = "Configs/Gameplay/Loot/New GoldLootConfig")]
    public class CoinsLootConfig : LootConfig
    {
        [field: SerializeField] public int Coins { get; private set; }
    }
}
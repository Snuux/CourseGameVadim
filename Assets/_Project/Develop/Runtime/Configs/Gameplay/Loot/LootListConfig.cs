using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Loot
{
    [CreateAssetMenu(fileName = "LootListConfig", menuName = "Configs/Gameplay/Loot/New LootListConfig")]
    public class LootListConfig : ScriptableObject
    {
        [field: SerializeField] private List<LootConfig> _lootConfigs;

        public IReadOnlyList<LootConfig> LootConfigs => _lootConfigs;

        public LootConfig GetLootConfig(string id) => _lootConfigs.First(lootConfig => lootConfig.ID == id);
    }
}
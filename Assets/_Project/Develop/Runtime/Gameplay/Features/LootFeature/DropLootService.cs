using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Loot;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class DropLootService
    {
        private LootListConfig _lootListConfig;
        private LootFactory _lootFactory;

        public DropLootService(LootListConfig lootListConfig, LootFactory lootFactory)
        {
            _lootListConfig = lootListConfig;
            _lootFactory = lootFactory;
        }

        public void DropLootFor(Entity entity)
        {
            Transform entityTransform = entity.Transform;

            List<ExperienceLootConfig> expConfig = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(ExperienceLootConfig))
                .Cast<ExperienceLootConfig>()
                .ToList();

            if (expConfig.Count > 0)
                DropExp(entityTransform.position, expConfig[Random.Range(0, expConfig.Count)]);

            DropCoins(entityTransform.position);
            DropHealth(entityTransform.position);
        }

        private void DropExp(Vector3 position, ExperienceLootConfig experienceLootConfig)
        {
            int expInOnePotion = 300;

            if (experienceLootConfig.Experience < expInOnePotion)
            {
                _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position,
                    experienceLootConfig.Experience);
            }
            else
            {
                int restOfExp = experienceLootConfig.Experience % expInOnePotion;

                int potionNumbers = (experienceLootConfig.Experience - restOfExp) / expInOnePotion;

                for (int i = 0; i < potionNumbers; i++)
                    _lootFactory.CreateExperienceLoot(experienceLootConfig.PrefabPath, position, expInOnePotion);
            }
        }

        private void DropCoins(Vector3 position)
        {
            List<CoinsLootConfig> coinsConfig = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(CoinsLootConfig))
                .Cast<CoinsLootConfig>()
                .ToList();

            if (coinsConfig.Count > 0 && Random.Range(0, 100) > 50)
            {
                CoinsLootConfig coinConfig = coinsConfig[Random.Range(0, coinsConfig.Count)];
                _lootFactory.CreateCoinsLoot(coinConfig.PrefabPath, position, coinsConfig.Count);
            }
        }

        private void DropHealth(Vector3 position)
        {
            List<HealthLootConfig> healthLootConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(HealthLootConfig))
                .Cast<HealthLootConfig>()
                .ToList();

            if (healthLootConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                HealthLootConfig healthConfig = healthLootConfigs[Random.Range(0, healthLootConfigs.Count)];
                _lootFactory.CreateHealthLoot(healthConfig.PrefabPath, position, healthLootConfigs.Count);
            }
        }
    }
}
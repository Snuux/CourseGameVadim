using System;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Stages
{
    [Serializable]
    public class EnemyItemConfig
    {
        public EnemyItemConfig()
        {
        }

        public EnemyItemConfig(EntityConfig enemyConfig)
        {
            EnemyConfig = enemyConfig;
        }

        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}

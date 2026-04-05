using System;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Stages
{
    [Serializable]
    public class EnemyItemConfig
    {
        [field: SerializeField] public EntityConfig EnemyConfig { get; private set; }
    }
}

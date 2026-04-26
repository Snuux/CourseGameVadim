using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Abilities/New HealthIncreaseAbilityConfig", fileName = "HealthIncreaseAbilityConfig")]
    public class HealthIncreaseAbilityConfig : AbilityConfig
    {
        [field: SerializeField] public int MaxHealth { get; private set; } = 3;
    }
}
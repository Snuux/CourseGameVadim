using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewPuddleConfig", fileName = "PuddleConfig")]
    public class PuddleConfig : EntityConfig
    {
        [field: SerializeField, Min(0)] public float AttackDamage { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float AttackTriggerRadius { get; private set; } = .8f;

        [field: SerializeField, Min(0)] public float AttackEverySecond { get; private set; } = .3f;
        
        [field: SerializeField, Min(0)] public float DeathProcessTime { get; private set; } = .1f;
        
        [field: SerializeField, Min(0)] public float SpawnProcessTime { get; private set; } = 2;
    }
}
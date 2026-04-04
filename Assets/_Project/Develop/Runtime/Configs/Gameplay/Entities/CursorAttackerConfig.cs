using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewMCursorAttackerConfig", fileName = "CursorAttackerConfig")]
    public class CursorAttackerConfig : EntityConfig
    {
        [field: SerializeField] public float AttackRadius { get; private set; } = 3f;
        [field: SerializeField] public float AttackDamage { get; private set; } = 1f;
    }
}
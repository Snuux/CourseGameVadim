using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.Entities
{
    public abstract class EntityConfig : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
    }
}
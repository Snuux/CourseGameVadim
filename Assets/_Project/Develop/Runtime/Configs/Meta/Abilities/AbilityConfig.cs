using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string ID  { get; private set; }
        
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: SerializeField] public EntitiesFilters ApplyToType { get; private set; }
        [field: SerializeField] public AbilityActivationTypes ActivateOnType { get; private set; }
    }
}
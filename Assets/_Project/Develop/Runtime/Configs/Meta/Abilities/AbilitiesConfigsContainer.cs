using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Meta/Abilities/NewAbilitiesConfigsContainer", fileName = "AbilitiesConfigsContainer")]
    public class AbilitiesConfigsContainer : ScriptableObject
    {   
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        
        public IReadOnlyList<AbilityConfig> AbilityConfigs => _abilityConfigs;
        
        public AbilityConfig GetConfigBy(string id) => _abilityConfigs.First(config => config.ID == id);
        public AbilityConfig GetConfigBy(Ability ability) => _abilityConfigs.First(config => config.ID == ability.ID);
    }
}
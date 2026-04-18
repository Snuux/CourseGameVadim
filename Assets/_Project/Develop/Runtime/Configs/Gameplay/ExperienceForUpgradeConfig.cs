using System.Collections.Generic;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/New ExperienceForUpgradeConfig",  fileName = "ExperienceForUpgradeConfig")]
    public class ExperienceForUpgradeConfig : ScriptableObject
    {
        [SerializeField] private List<float> _experienceForLevel;
        
        public int MaxLevel => _experienceForLevel.Count;
        
        public float GetExperienceFor(int level) => _experienceForLevel[level - 1];
    }
}
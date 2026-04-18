using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.StatFeature
{
    public class BaseStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }
    
    public class ModifiedStats : IEntityComponent
    {
        public Dictionary<StatTypes, float> Value;
    }

    public class StatsEffects : IEntityComponent
    {
        public StatsEffectsList Value;
    }
}
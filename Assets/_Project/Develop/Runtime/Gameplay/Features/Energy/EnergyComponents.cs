using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class MaxEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class CurrentEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRecoverAmount : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class EnergyRecoverInterval : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergySpendRequest : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }
    
    public class EnergySpendEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}
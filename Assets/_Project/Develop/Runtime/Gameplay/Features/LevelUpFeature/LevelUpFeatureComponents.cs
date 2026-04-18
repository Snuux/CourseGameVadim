using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature
{
    public class Experience : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class Level : IEntityComponent
    {
        public ReactiveVariable<int> Value;
    }
}
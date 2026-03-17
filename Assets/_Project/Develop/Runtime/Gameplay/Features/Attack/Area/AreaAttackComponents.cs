using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.Area
{
    public class AreaAttackRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}
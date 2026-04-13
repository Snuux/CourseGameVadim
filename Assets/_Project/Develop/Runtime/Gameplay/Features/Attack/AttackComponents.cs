using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.AreaAttack
{
    public class CanStartAttack : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class AttackDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackRequested : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class AttackStarted : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class AttackCompleted : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class TriggerRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}
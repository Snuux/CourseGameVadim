using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class IsProjectile : IEntityComponent
    {
    }
    
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

    public class TriggerRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    // Attack main loop

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
    
    public class ShootPoint : IEntityComponent
    {
        public Transform Value;
    }

    // Attack cooldown
    
    public class AttackCooldownInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackCooldownCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InAttackCooldown : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    // Attack process
    
    public class AttackProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class AttackProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InAttackProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    // Attack action
    
    public class AttackInitialActionTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class HasReachedActionTime : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
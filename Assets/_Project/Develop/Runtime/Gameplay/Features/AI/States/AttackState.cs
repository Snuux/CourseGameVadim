using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class AttackState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Entity> _currentTarget;
        private readonly ReactiveVariable<bool> _attackRequested;
        
        public AttackState(Entity entity)
        {
            _currentTarget = entity.CurrentTarget;
            _attackRequested = entity.AttackRequested;
        }
        
        public void Update(float deltaTime)
        {
            if (_currentTarget.Value == null)
                return;

            _attackRequested.Value = true;
        }
    }
}
using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AreaAttack
{
    public class StartAttackSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _attackRequested;
        private ReactiveVariable<bool> _attackStarted;
        private ICompositeCondition _canStartAttack;

        public void OnInit(Entity entity)
        {
            _attackRequested = entity.AttackRequested;
            _attackStarted = entity.AttackStarted;
            _canStartAttack = entity.CanStartAttack;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackRequested.Value == false)
                return;

            _attackRequested.Value = false;

            if (_canStartAttack.Evaluate())
            {
                _attackStarted.Value = true;
                Debug.Log("Атака начата");
                return;
            }
        }
    }
}
using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inAttackCooldown;
        private ReactiveVariable<bool> _attackCompleted;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.AttackCooldownCurrentTime;
            _initialTime = entity.AttackCooldownInitialTime;
            _inAttackCooldown = entity.InAttackCooldown;
            _attackCompleted = entity.AttackCompleted;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackCompleted.Value == false || _inAttackCooldown.Value == false)
                return;

            if (_attackCompleted.Value == true)
            {
                _currentTime.Value = _initialTime.Value;
                _inAttackCooldown.Value = true;
            }

            _currentTime.Value -= deltaTime;

            if (_currentTime.Value <= 0) 
                _inAttackCooldown.Value = false;
        }
    }
}

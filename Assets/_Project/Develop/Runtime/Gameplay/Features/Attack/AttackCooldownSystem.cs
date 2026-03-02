using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackCooldownSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;

        private ReactiveVariable<bool> _inAttackCooldown;

        private ReactiveEvent _endAttackEvent;

        private IDisposable _endAttackEventDisposable;

        public void OnInit(Entity entity)
        {
            _initialTime = entity.AttackCooldownInitialTime;
            _currentTime = entity.AttackCooldownCurrentTime;
            _inAttackCooldown = entity.InAttackCooldown;

            _endAttackEvent = entity.EndAttackEvent;

            _endAttackEventDisposable = _endAttackEvent.Subscribe(OnEndAttack);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackCooldown.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
            {
                _inAttackCooldown.Value = false;
                Debug.Log("Кулдаун завершился");
            }
        }

        public void OnDispose()
        {
            _endAttackEventDisposable.Dispose();
        }

        private void OnEndAttack()
        {
            Debug.Log("Кулдаун начался");
            _currentTime.Value = _initialTime.Value;
            _inAttackCooldown.Value = true;
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;
    }
}
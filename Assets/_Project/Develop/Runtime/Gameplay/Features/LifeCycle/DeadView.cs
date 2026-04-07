using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    [RequireComponent(typeof(Animator))]
    public class DeadView : EntityView
    {
        private readonly int IsDeadKey = Animator.StringToHash("IsDead");

        [SerializeField] private Animator _animator;

        private IReadOnlyVariable<bool> _isDead;

        private IDisposable _isDeadChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;

            _isDeadChangedDisposable = _isDead.Subscribe(OnIsMovingChanged);    
            UpdateIsMoving(_isDead.Value);
        }
        
        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _isDeadChangedDisposable?.Dispose();
        }

        private void UpdateIsMoving(bool isDeadValue) => _animator.SetBool(IsDeadKey, isDeadValue);

        private void OnIsMovingChanged(bool oldIsDead, bool isDead) => UpdateIsMoving(isDead);
    }
}
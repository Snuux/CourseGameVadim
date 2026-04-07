using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class InstantAttackView : EntityView
    {
        private readonly int InAttackProcess = Animator.StringToHash("IsAttack");

        [SerializeField] private Animator _animator;
        
        private ReactiveVariable<bool> _inAttackProcess;
        private IDisposable _inAttackProcessChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inAttackProcess = entity.InAttackProcess;

            _inAttackProcessChangedDisposable = _inAttackProcess.Subscribe(OnAttackProcessChanged);
            UpdateInAttack(_inAttackProcess.Value);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _inAttackProcessChangedDisposable.Dispose();
        }

        private void OnAttackProcessChanged(bool oldIsAttack, bool inAttack)
        {
            UpdateInAttack(inAttack);
        }

        private void UpdateInAttack(bool inAttack)
        {
            _animator.SetBool(InAttackProcess, inAttack);
        }
    }
}
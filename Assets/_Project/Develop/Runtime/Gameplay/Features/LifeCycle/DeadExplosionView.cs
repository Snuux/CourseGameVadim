using System;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeadExplosionView : EntityView
    {
        private IReadOnlyVariable<bool> _isDead;
        private Transform _entityTransform;

        private IDisposable _isDeadChangedDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;
            _entityTransform =  entity.Transform;

            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _isDeadChangedDisposable?.Dispose();
        }

        private void OnIsDeadChanged(bool oldIsDead, bool isDead)
        {
            if (isDead)
                UpdateIsDead(isDead);
        }

        private void UpdateIsDead(bool isDeadValue)
        {
            Sequence s = CommonAnimationsCreator.CreateBeforeExplosionAnimation(_entityTransform, 2, 2);
            s.Play();
        }
    }
}
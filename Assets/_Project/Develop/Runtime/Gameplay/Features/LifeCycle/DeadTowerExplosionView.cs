using System;
using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeadTowerExplosionView : EntityView
    {
        private const float ExplosionMaxScale = 1.3f;

        [SerializeField] ParticleSystem _applyDamageEffectPrefab;

        private IReadOnlyVariable<bool> _isDead;
        private IReadOnlyVariable<float> _deathProcessInitialTime;
        private Transform _entityTransform;

        private Sequence _deathAnimation;

        private IDisposable _isDeadChangedDisposable;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _isDead = entity.IsDead;
            _deathProcessInitialTime = entity.DeathProcessInitialTime;
            _entityTransform = entity.Transform;

            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _deathAnimation?.Kill();
            _deathAnimation = null;
            _isDeadChangedDisposable?.Dispose();
            
            InstantiateFinalExplosion();
        }

        private void OnIsDeadChanged(bool oldIsDead, bool isDead)
        {
            if (isDead)
                PlayDeathAnimation();
        }

        private void PlayDeathAnimation()
        {
            if (_entityTransform == null)
                return;

            _deathAnimation = CommonAnimationsCreator.CreatePreExplosionAnimation(
                _entityTransform,
                ExplosionMaxScale,
                _deathProcessInitialTime.Value);

            _deathAnimation.Play();
        }

        private void InstantiateFinalExplosion()
        {
            Instantiate(_applyDamageEffectPrefab, _entityTransform.position, Quaternion.identity, null);
        }
    }
}
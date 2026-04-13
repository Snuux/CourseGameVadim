using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class TriggerRadiusView : EntityView
    {
        [SerializeField] ParticleSystem _radiusEffectPrefab;
        [SerializeField] Transform _effectSpawnPoint;

        private ParticleSystem _radiusEffect;

        private IReadOnlyVariable<bool> _isDead;
        private IDisposable _isDeadChangedDisposable;

        private void OnValidate()
        {
            _effectSpawnPoint ??= transform;
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            SetStartSizeToRadiusFor(entity);
            _radiusEffect = Instantiate(_radiusEffectPrefab, _effectSpawnPoint.position, Quaternion.identity,
                transform);

            _isDead = entity.IsDead;
            _isDeadChangedDisposable = _isDead.Subscribe(OnIsDeadChanged);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _isDeadChangedDisposable?.Dispose();
        }

        private void SetStartSizeToRadiusFor(Entity entity)
        {
            var explosion = _radiusEffectPrefab.main;
            explosion.startSize = entity.TriggerRadius.Value;
        }

        private void OnIsDeadChanged(bool arg1, bool isDead)
        {
            Destroy(_radiusEffect);
        }
    }
}
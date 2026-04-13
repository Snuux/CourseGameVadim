using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
    public class ApplyDamageView :EntityView
    {
        [SerializeField] ParticleSystem _applyDamageEffectPrefab;
        [SerializeField] Transform _effectSpawnPoint;

        private ReactiveEvent<float> _damageEvent;
        
        private IDisposable _damageEventDisposable;
        
        protected override void OnEntityStartedWork(Entity entity)
        {
            _damageEvent = entity.TakeDamageEvent;
            _damageEventDisposable = _damageEvent.Subscribe(OnDamaged);
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);
            
            _damageEventDisposable.Dispose();
        }

        private void OnDamaged(float obj)
        {
            Instantiate(_applyDamageEffectPrefab, _effectSpawnPoint.position, Quaternion.identity, null);
        }
    }
}
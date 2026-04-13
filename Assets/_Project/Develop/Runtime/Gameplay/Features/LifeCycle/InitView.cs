using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class InitView : EntityView
    {
        [SerializeField] ParticleSystem _initEffectPrefab;
        [SerializeField] Transform _effectSpawnPoint;

        protected override void OnEntityStartedWork(Entity entity)
        {
            SetStartSizeToRadiusFor(entity);

            Instantiate(_initEffectPrefab, _effectSpawnPoint.position, Quaternion.identity, null);
        }

        private void SetStartSizeToRadiusFor(Entity entity)
        {
            var explosion = _initEffectPrefab.main;
            explosion.startSize = entity.AttackRadius.Value;
        }
    }
}
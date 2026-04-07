using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.SpawnFeature
{
    [RequireComponent(typeof(Animator))]
    public class SpawnProcessView : EntityView
    {
        private readonly int SpawningProcessKey = Animator.StringToHash("InSpawnProcess");

        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _spawnEffectPrefab;

        private ReactiveVariable<bool> _inSpawnProcess;
        private Transform _entityTransform;

        private IDisposable _inSpawnProcessChangedDisposable;

        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        protected override void OnEntityStartedWork(Entity entity)
        {
            _inSpawnProcess = entity.InSpawnProcess;
            _entityTransform = entity.Transform;

            _inSpawnProcessChangedDisposable = _inSpawnProcess.Subscribe(OnSpawnProcessChanged);
            UpdateSpawnProcessKey(_inSpawnProcess.Value);
        }

        private void OnSpawnProcessChanged(bool oldInSpawnProcess, bool inSpawnProcess)
        {
            UpdateSpawnProcessKey(inSpawnProcess);
        }

        private void UpdateSpawnProcessKey(bool inSpawnProcess)
        {
            _animator.SetBool(SpawningProcessKey, inSpawnProcess);

            if (inSpawnProcess)
                Instantiate(_spawnEffectPrefab, _entityTransform.position, Quaternion.identity, null);
        }
    }
}
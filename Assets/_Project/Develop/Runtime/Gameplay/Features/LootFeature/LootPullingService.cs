using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class LootPullingService : IInitializable, IDisposable
    {
        private ReactiveVariable<bool> _allCollected = new();
        private List<Entity> _loot = new();
        private EntitiesLifeContext _entitiesLifeContext;
        private bool _isActivated;

        public LootPullingService(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }
        
        public IReadOnlyVariable<bool> AllCollected => _allCollected;

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdd;
            _entitiesLifeContext.Released += OnEntityRelease;
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdd;
            _entitiesLifeContext.Released -= OnEntityRelease;
        }

        public void PullTo(Entity entity)
        {
            if (_isActivated)
                throw new InvalidOperationException($"{nameof(PullTo)} can only be called once");
            
            _isActivated = true;

            if (_loot.Count == 0)
            {
                _allCollected.Value = true;
                return;
            }

            foreach (var loot in _loot)
            {
                loot.CurrentTarget.Value = entity;
                loot.IsPullingProcess.Value = true;
            }
        }

        public void Reset()
        {
            _isActivated = false;
            _allCollected.Value = false;
        }

        private void OnEntityAdd(Entity entity)
        {
            if (entity.HasComponent<IsPullable>() == false)
                return;
            
            _loot.Add(entity);
            
            Transform lootTransform = entity.Transform;
            
            Vector2 randomOffset = UnityEngine.Random.insideUnitCircle;
            Vector3 offset = new Vector3(randomOffset.x, 0, randomOffset.y);
            Vector3 endJumpPosition = lootTransform.position + offset;

            lootTransform
                .DOJump(endJumpPosition, 2, 1, .7f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => entity.InSpawnProcess.Value = false)
                .Play();

        }

        private void OnEntityRelease(Entity obj)
        {
            bool lootRemoved =  _loot.Remove(obj);
            
            if (lootRemoved && _loot.Count == 0)
                _allCollected.Value = true;
        }
    }
}
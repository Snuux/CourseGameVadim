using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class TowerHolderService : IInitializable, IDisposable
    {
        private EntitiesLifeContext _entitiesLifeContext;
        private ReactiveEvent<Entity> _towerRegistered = new();

        private Entity _tower;

        public TowerHolderService(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public ReactiveEvent<Entity> TowerRegistered => _towerRegistered;

        public Entity Tower => _tower;

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            Debug.Log("asdadkasldkaso");
            if (entity.HasComponent<IsTower>())
            {
                Debug.Log("ISTOWER!!");
                _entitiesLifeContext.Added -= OnEntityAdded;
                _tower = entity;
                _towerRegistered?.Invoke(_tower);
            }
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
        }
    }
}
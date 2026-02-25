using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntityFactory : IInitializable, IDisposable
    {
        private readonly ResourcesAssetsLoader _resources;

        private readonly EntitiesLifeContext _entitiesLifeContext;

        private Dictionary<Entity, MonoEntity> _entityToMono = new();

        public MonoEntityFactory(ResourcesAssetsLoader resourcesAssetsLoader, EntitiesLifeContext entitiesLifeContext)
        {
            _resources = resourcesAssetsLoader;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public MonoEntity Create(Entity entity, Vector3 position, string path)
        {
            MonoEntity prefab = _resources.Load<MonoEntity>(path);
            
            MonoEntity viewInstance = Object.Instantiate(prefab, position, Quaternion.identity, null);
            viewInstance.Setup(entity);

            return viewInstance;
        }

        public void Initialize()
        {
            _entitiesLifeContext.Released += OnEntityReleased;
        }

        public void Dispose()
        {
            _entitiesLifeContext.Released -= OnEntityReleased;

            foreach (Entity entity in _entityToMono.Keys) 
                CleanupFor(entity);
            
            _entityToMono.Clear();
        }

        private void OnEntityReleased(Entity entity)
        {
            CleanupFor(entity);
            
            _entityToMono.Remove(entity);
        }

        private void CleanupFor(Entity entity)
        {
            MonoEntity monoEntity = _entityToMono[entity];
            monoEntity.Cleanup(entity);
            Object.Destroy(monoEntity.gameObject);
        }
    }
}
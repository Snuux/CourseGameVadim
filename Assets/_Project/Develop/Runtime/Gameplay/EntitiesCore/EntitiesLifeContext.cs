using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesLifeContext : IDisposable
    {
        public event Action<Entity> Added;
        public event Action<Entity> Released;
        
        private readonly List<Entity> _entities = new();
        private readonly List<Entity> _releaseRequests = new();

        public void Add(Entity entity)
        {
            _entities.Add(entity);
            
            entity.Initialize();
            
            Added?.Invoke(entity);
        }

        public void Update(float deltaTime)
        {
            //foreach нельзя потому что есди добавится во время ввыполнения сущность
            //то он сломается
            for (int i = 0; i < _entities.Count; i++)
                _entities[i].OnUpdate(deltaTime);

            //удаляем
            foreach (Entity entity in _releaseRequests)
            {
                _entities.Remove(entity);
                entity.OnDispose();
                Released?.Invoke(entity);
            }
            
            _releaseRequests.Clear();
        }

        public void Release(Entity entity)
        {
            _releaseRequests.Add(entity);
        }

        public void Dispose()
        {
            foreach (Entity entity in _entities) 
                entity.OnDispose();
            
            _entities.Clear();
            _releaseRequests.Clear();
        }
    }
}
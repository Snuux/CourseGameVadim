using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegistryService _collidersRegistryService;

        private Entity _linkedEntity;

        public void Initialize(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public Entity LinkedEntity => _linkedEntity;

        public void Link(Entity entity)
        {
            _linkedEntity = entity;

            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null)
                foreach (MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            foreach (Collider collider1 in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Register(collider1, entity);
        }

        public void Cleanup(Entity entity)
        {
            _linkedEntity = null;
            foreach (Collider collider1 in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Unregister(collider1);
        }
    }
}
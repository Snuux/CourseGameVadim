using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono
{
    public class MonoEntity : MonoBehaviour
    {
        private CollidersRegistryService _collidersRegistryService;

        private Entity _linkedEntity;

        // Хранит логическую Entity, с которой связано это Mono-представление.
        public Entity LinkedEntity => _linkedEntity;

        // Сохраняем сервис регистрации коллайдеров, который связывает Collider
        // с соответствующей игровой Entity.
        public void Initialize(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        // Привязываем runtime Entity к этому MonoBehaviour-объекту.
        public void Link(Entity entity)
        {
            _linkedEntity = entity;

            // Ищем все дочерние компоненты-регистраторы.
            MonoEntityRegistrator[] registrators = GetComponentsInChildren<MonoEntityRegistrator>();

            if (registrators != null)
                foreach (MonoEntityRegistrator registrator in registrators)
                    registrator.Register(entity);

            EntityView[] views = GetComponentsInChildren<EntityView>();
            
            //Вызываем Link у EntityViews
            if (views != null)
                foreach (EntityView view in views)
                    view.Link(entity);

            // Регистрируем каждый найденный Collider в общем реестре.
            // Это позволяет по любому столкновению быстро определить,
            // какой именно Entity принадлежит конкретный collider.
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Register(collider, entity);
        }

        // Очищаем связь Mono-объекта с Entity перед уничтожением или возвратом в пул.
        public void Cleanup(Entity entity)
        {
            EntityView[] views = GetComponentsInChildren<EntityView>();
            
            if (views != null)
                foreach (EntityView view in views)
                    view.Cleanup(entity);

            // Повторно обходим все дочерние Collider, потому что именно они
            // ранее были зарегистрированы в сервисе во время Link.
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                _collidersRegistryService.Unregister(collider);

            _linkedEntity = null;
        }
    }
}
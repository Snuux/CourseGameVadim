using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private readonly MonoEntityFactory _monoEntityFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntityFactory = container.Resolve<MonoEntityFactory>();
        }

        public Entity CreateTestEntityRigidbody(Vector3 position)
        {
            Entity entity = CreateEntity();

            _monoEntityFactory.Create(entity, position, "Entities/TestEntityRigidbody");
            
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotation(new ReactiveVariable<Quaternion>(Quaternion.identity))
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddPosition();

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem())
                .AddSystem(new RigidbodyPositionSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }
        
        public Entity CreateTestEntityCharacterController(Vector3 position)
        {
            Entity entity = CreateEntity();

            _monoEntityFactory.Create(entity, position, "Entities/TestEntityCharacterController");
            
            entity
                .AddMoveDirection()
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddRotation(new ReactiveVariable<Quaternion>(Quaternion.identity))
                .AddRotationSpeed(new ReactiveVariable<float>(900))
                .AddPosition();

            entity
                .AddSystem(new CharacterControllerMovementSystem())
                .AddSystem(new CharacterControllerRotationSystem())
                .AddSystem(new CharacterControllerPositionSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }


        private Entity CreateEntity() => new Entity();
    }
}
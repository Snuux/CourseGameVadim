using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.Attacks
{
    public class ProjectileActionAttackSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;

        private ReactiveVariable<bool> _hasReachedActionTime;
        private ReactiveVariable<bool> _attackCompleted;

        private Entity _entity;
        private Transform _shootPoint;

        public ProjectileActionAttackSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _hasReachedActionTime = entity.HasReachedActionTime;
            _shootPoint = entity.ShootPoint;
            _attackCompleted = entity.AttackCompleted;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_hasReachedActionTime.Value == false)
                return;

            _hasReachedActionTime.Value = false;

            _entitiesFactory.CreateProjectile(_shootPoint.position, _shootPoint.forward, _entity);

            _attackCompleted.Value = true;
        }
    }
}
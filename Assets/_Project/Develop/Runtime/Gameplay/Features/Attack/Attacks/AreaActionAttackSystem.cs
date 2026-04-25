using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.Attacks
{
    public class AreaActionAttackSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;
        
        private ReactiveVariable<bool> _hasReachedActionTime;
        private ReactiveVariable<bool> _attackCompleted;
        
        private Transform _sourceTransform;
        private Entity _entity;

        public AreaActionAttackSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _sourceTransform = entity.Transform;
            _hasReachedActionTime = entity.HasReachedActionTime;
            _attackCompleted = entity.AttackCompleted;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_hasReachedActionTime.Value == false)
                return;
            
            _hasReachedActionTime.Value = false;
            
            _entitiesFactory.CreateInstantDamageZone(_sourceTransform.position, _entity);
            
            _attackCompleted.Value = true;
        }
    }
}
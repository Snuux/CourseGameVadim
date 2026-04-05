using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AreaAttack
{
    public class AreaAttackSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;
        
        private Transform _sourceTransform;
        
        private ReactiveVariable<bool> _attackStarted;
        private ReactiveVariable<bool> _attackCompleted;
        
        private ReactiveVariable<float> _damage;
        private ReactiveVariable<float> _radius;
        
        private Entity _entity;

        public AreaAttackSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _sourceTransform = entity.Transform;
            _attackStarted = entity.AttackStarted;
            _attackCompleted = entity.AttackCompleted;
            _damage = entity.AttackDamage;
            _radius = entity.AttackRadius;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackStarted.Value == false)
                return;
            
            _attackStarted.Value = false;
            _entitiesFactory.CreateAreaProjectile(_sourceTransform.position, _radius.Value, _damage.Value, _entity);
            
            _attackCompleted.Value = true;
        }
    }
}
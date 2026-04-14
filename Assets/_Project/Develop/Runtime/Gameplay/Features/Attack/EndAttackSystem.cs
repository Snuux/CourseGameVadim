using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class EndAttackSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _attackCompleted;
        
        public void OnInit(Entity entity)
        {
            _attackCompleted = entity.AttackCompleted;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackCompleted.Value == false)
                return;

            _attackCompleted.Value = false;
            
            Debug.Log("Атака завершена");
        }
    }
}
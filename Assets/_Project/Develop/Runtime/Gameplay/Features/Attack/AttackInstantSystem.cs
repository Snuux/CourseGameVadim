using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackInstantSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _attackStarted;
        private ReactiveVariable<bool> _hasReachedActionTime;

        public void OnInit(Entity entity)
        {
            _attackStarted = entity.AttackStarted;
            _hasReachedActionTime = entity.HasReachedActionTime;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackStarted.Value == false)
                return;

            _attackStarted.Value = false;

            _hasReachedActionTime.Value = true;
        }
    }
}
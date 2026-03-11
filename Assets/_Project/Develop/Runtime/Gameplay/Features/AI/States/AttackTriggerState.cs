using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class AttackTriggerState : State, IUpdatableState
    {
        private ReactiveEvent _attackRequst;

        public AttackTriggerState(Entity entity)
        {
            _attackRequst = entity.StartAttackRequest;
        }

        public override void Enter()
        {
            base.Enter();
            
            _attackRequst.Invoke();
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}
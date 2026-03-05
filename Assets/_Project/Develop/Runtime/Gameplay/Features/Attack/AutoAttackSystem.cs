using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AutoAttackSystem : IUpdatableSystem
    {
        private readonly ICompositeCondition _condition;
        private readonly ReactiveEvent _event;

        public AutoAttackSystem(ICompositeCondition condition, ReactiveEvent eventArg)
        {
            _event = eventArg;
            _condition = condition;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_condition.Evaluate())
                _event.Invoke();
        }
    }
}
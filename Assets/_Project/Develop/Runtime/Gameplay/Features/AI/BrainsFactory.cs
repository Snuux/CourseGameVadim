using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Timer;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly TimerServiceFactory _timerServiceFactory;
        private readonly AIBrainsContext _brainsContext;
        private readonly IInputService _inputService;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;
            _timerServiceFactory = _container.Resolve<TimerServiceFactory>();
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _inputService = _container.Resolve<IInputService>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }
        
        public StateMachineBrain CreateGhostBrain(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            AIStateMachine movementAttackStateMachine = CreateMovementAttackStateMachine(entity);
            EmptyState emptyState = new EmptyState();

            ICompositeCondition movementCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentTarget != null))
                .Add(new FuncCondition(() => entity.CurrentTarget.Value.IsDead.Value == false));
            
            ICompositeCondition idleCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentTarget == null))
                .Add(new FuncCondition(() => entity.CurrentTarget.Value.IsDead.Value));

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(movementAttackStateMachine);
            stateMachine.AddState(emptyState);

            stateMachine.AddTransition(movementAttackStateMachine, emptyState, idleCondition);
            stateMachine.AddTransition(emptyState, movementAttackStateMachine, movementCondition);

            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        private AIStateMachine CreateMovementAttackStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            MovementToTargetState movementToTargetState = new MovementToTargetState(entity);
            AttackState attackState = new AttackState(entity);

            ICompositeCondition movementCondition = new CompositeCondition()
                .Add(new FuncCondition(() => TargetInRange(entity) == false));

            ICompositeCondition attackCondition = new CompositeCondition()
                .Add(new FuncCondition(() => TargetInRange(entity)));

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(attackState);
            stateMachine.AddState(movementToTargetState);

            stateMachine.AddTransition(movementToTargetState, attackState, attackCondition);
            stateMachine.AddTransition(attackState, movementToTargetState, movementCondition);

            return stateMachine;
        }

        private bool TargetInRange(Entity entity)
        {
            return CalcDistanceToTarget(entity) < entity.DistanceForAttack.Value;
        }

        private float CalcDistanceToTarget(Entity source)
        {
            return (source.CurrentTarget.Value.Transform.position - source.Transform.position).magnitude;
        }
    }
}
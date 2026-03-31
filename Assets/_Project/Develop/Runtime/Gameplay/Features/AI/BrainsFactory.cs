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
            AIStateMachine stateMachine = CreateMoveToTargetStateMachine(entity);
            StateMachineBrain brain = new StateMachineBrain(stateMachine);

            _brainsContext.SetFor(entity, brain);

            return brain;
        }
        
        private AIStateMachine CreateMoveToTargetStateMachine(Entity entity)
        {
            List<IDisposable> disposables = new List<IDisposable>();

            MoveToTargetState moveToTargetState = new MoveToTargetState(entity);

            EmptyState emptyState = new EmptyState();

            FuncCondition movementCondition = new FuncCondition(() => entity.CurrentTarget != null);
            FuncCondition idleCondition = new FuncCondition(() => entity.CurrentTarget == null);

            AIStateMachine stateMachine = new AIStateMachine(disposables);

            stateMachine.AddState(emptyState);
            stateMachine.AddState(moveToTargetState);

            stateMachine.AddTransition(moveToTargetState, emptyState, idleCondition);
            stateMachine.AddTransition(emptyState, moveToTargetState, movementCondition);

            return stateMachine;
        }
    }
}
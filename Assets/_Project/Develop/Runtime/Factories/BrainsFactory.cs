using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI
{
    public class BrainsFactory
    {
        private readonly DIContainer _container;
        private readonly AIBrainsContext _brainsContext;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public BrainsFactory(DIContainer container)
        {
            _container = container;
            _brainsContext = _container.Resolve<AIBrainsContext>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
        }
        
        public StateMachineBrain CreateGhostBrain(Entity entity, ITargetSelector targetSelector)
        {
            AIStateMachine movementAttackStateMachine = CreateMovementAttackStateMachine(entity);
            EmptyState emptyState = new EmptyState();

            ICompositeCondition movementCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentTarget != null))
                .Add(new FuncCondition(() => entity.CurrentTarget.Value.IsDead.Value == false));
            
            ICompositeCondition idleCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => entity.CurrentTarget == null))
                .Add(new FuncCondition(() => entity.CurrentTarget.Value.IsDead.Value == true));

            AIStateMachine behaviour = new AIStateMachine();

            behaviour.AddState(movementAttackStateMachine);
            behaviour.AddState(emptyState);

            behaviour.AddTransition(movementAttackStateMachine, emptyState, idleCondition);
            behaviour.AddTransition(emptyState, movementAttackStateMachine, movementCondition);

            FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
            AIParallelState parallelState = new AIParallelState(findTargetState, behaviour);
            
            AIStateMachine rootStateMachine = new AIStateMachine();
            rootStateMachine.AddState(parallelState);
            
            StateMachineBrain brain = new StateMachineBrain(rootStateMachine);
            _brainsContext.SetFor(entity, brain);

            return brain;
        }
        
        public StateMachineBrain CreateArcherBrain(Entity entity, ITargetSelector targetSelector)
        {
            return CreateGhostBrain(entity, targetSelector);
        }
        
        public StateMachineBrain CreateTurretBrain(Entity entity, ITargetSelector targetSelector)
        {
            EmptyState emptyState = new EmptyState();
            AttackState attackState = new AttackState(entity);

            ICompositeCondition attackCondition = new CompositeCondition()
                .Add(new FuncCondition(() => TargetInRange(entity)));
            
            ICompositeCondition ifNoTargetCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => TargetInRange(entity) == false));
            
            AIStateMachine behaviour = new AIStateMachine();
            
            behaviour.AddState(emptyState);
            behaviour.AddState(attackState);
            
            behaviour.AddTransition(emptyState, attackState, attackCondition);
            behaviour.AddTransition(attackState, emptyState, ifNoTargetCondition);
            
            FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
            RotateToTargetState rotateToTargetState = new RotateToTargetState(entity);
            AIParallelState parallelState = new AIParallelState(findTargetState, rotateToTargetState, behaviour);
            
            AIStateMachine rootStateMachine = new AIStateMachine();
            rootStateMachine.AddState(parallelState);
            
            StateMachineBrain brain = new StateMachineBrain(rootStateMachine);
            _brainsContext.SetFor(entity, brain);

            return brain;
        }

        public IBrain CreatePuddleBrain(Entity entity, NearestDamageableTargetSelector targetSelector)
        {
            return CreateMineBrain(entity, targetSelector);
        }

        public IBrain CreateMineBrain(Entity entity, ITargetSelector targetSelector)
        {
            EmptyState emptyState = new EmptyState();
            AttackState attackState = new AttackState(entity);
            
            ICompositeCondition attackCondition = new CompositeCondition()
                .Add(new FuncCondition(() => TargetInRange(entity)));

            ICompositeCondition ifNoTargetCondition = new CompositeCondition(LogicOperations.Or)
                .Add(new FuncCondition(() => TargetInRange(entity) == false));

            AIStateMachine behaviour = new AIStateMachine();
            
            behaviour.AddState(emptyState);
            behaviour.AddState(attackState);

            behaviour.AddTransition(emptyState, attackState, attackCondition);
            behaviour.AddTransition(attackState, emptyState, ifNoTargetCondition);
            
            FindTargetState findTargetState = new FindTargetState(targetSelector, _entitiesLifeContext, entity);
            AIParallelState parallelState = new AIParallelState(findTargetState, behaviour);
            
            AIStateMachine rootStateMachine = new AIStateMachine();
            rootStateMachine.AddState(parallelState);
            
            StateMachineBrain brain = new StateMachineBrain(rootStateMachine);
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
            if (EntitiesHelper.TryGetAliveTargetTransform(entity, out Transform targetTransform) == false)
                return false;
            
            return CalcDistanceToTarget(targetTransform.position, entity.Transform.position ) < 
                   entity.TriggerRadius.Value / 2.0f;
        }

        private float CalcDistanceToTarget(Vector3 source, Vector3 target)
        {
            return (target - source).magnitude;
        }
    }
}
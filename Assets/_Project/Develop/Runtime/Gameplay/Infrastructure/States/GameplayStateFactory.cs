using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class GameplayStateFactory
    {
        private readonly DIContainer _container;

        public GameplayStateFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs inputArgs)
        {
            GameplayStateMachine coreLoopState = CreateCoreLoopState();

            GameplayStateMachine gameplayCycle = new GameplayStateMachine();
            gameplayCycle.AddState(coreLoopState);

            return gameplayCycle;
        }

        public GameplayStateMachine CreateCoreLoopState()
        {
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            TowerHolderService towerHolderService = _container.Resolve<TowerHolderService>();
            IInputService inputService = _container.Resolve<IInputService>();
            EntitiesFactory entitiesFactory = _container.Resolve<EntitiesFactory>();

            StageProcessState stageProcessState = CreateStageProcessState();
            CursorAttackState cursorAttackState = new CursorAttackState(towerHolderService, inputService, entitiesFactory);

            GameplayParallelState battleState = new GameplayParallelState(stageProcessState, cursorAttackState);
            CursorPurchaseState purchaseState = new CursorPurchaseState(towerHolderService, entitiesFactory, inputService);
            
            GameplayStateMachine coreLoopState = new GameplayStateMachine();
            
            coreLoopState.AddState(battleState);
            coreLoopState.AddState(purchaseState);

            //ICompositeCondition waveEndCondition = new CompositeCondition()
            //    .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed))
            //    .Add(new FuncCondition(() => stageProviderService.HasNextStage()));

            ICompositeCondition battleToPurchaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed));
            
            ICompositeCondition purchaseToBattleCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProviderService.HasNextStage()))
                .Add(new FuncCondition(() => inputService.RightMouseButton))
                ;

            coreLoopState.AddTransition(battleState, purchaseState, battleToPurchaseCondition);
            coreLoopState.AddTransition(purchaseState, battleState, purchaseToBattleCondition);

            return coreLoopState;
        }

        public StageProcessState CreateStageProcessState()
        {
            return new StageProcessState(_container.Resolve<StageProviderService>());
        }
    }
}
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Gameplay.Infrastructure.States.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
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
            AllyFactory allyFactory = _container.Resolve<AllyFactory>();
            WalletService walletService = _container.Resolve<WalletService>();
            EntitiesLifeContext entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            ShopConfig shopConfig = _container.Resolve<ConfigsProviderService>().GetConfig<ShopConfig>();

            StageProcessState stageProcessState = CreateStageProcessState();
            CursorAttackState cursorAttackState = new CursorAttackState(inputService, entitiesFactory, allyFactory, entitiesLifeContext);

            GameplayParallelState battleState = new GameplayParallelState(stageProcessState, cursorAttackState);
            CursorShopState shopState = new CursorShopState(towerHolderService, allyFactory, inputService, walletService, shopConfig);
            
            GameplayStateMachine coreLoopState = new GameplayStateMachine();
            
            coreLoopState.AddState(battleState);
            coreLoopState.AddState(shopState);

            //ICompositeCondition waveEndCondition = new CompositeCondition()
            //    .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed))
            //    .Add(new FuncCondition(() => stageProviderService.HasNextStage()));

            ICompositeCondition battleToPurchaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed));
            
            ICompositeCondition purchaseToBattleCondition = new CompositeCondition()
                .Add(new FuncCondition(() => stageProviderService.HasNextStage()))
                .Add(new FuncCondition(() => inputService.RightMouseButtonDown))
                ;

            coreLoopState.AddTransition(battleState, shopState, battleToPurchaseCondition);
            coreLoopState.AddTransition(shopState, battleState, purchaseToBattleCondition);

            return coreLoopState;
        }

        public StageProcessState CreateStageProcessState()
        {
            return new StageProcessState(_container.Resolve<StageProviderService>());
        }
    }
}
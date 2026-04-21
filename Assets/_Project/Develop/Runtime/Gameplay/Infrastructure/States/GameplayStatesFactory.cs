using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.LootFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class GameplayStatesFactory
    {
        private readonly DIContainer _container;

        public GameplayStatesFactory(DIContainer container)
        {
            _container = container;
        }

        public CollectLootState CreateCollectLootState()
        {
            return new CollectLootState(
                _container.Resolve<LootPullingService>(),
                _container.Resolve<MainHeroHolderService>());
        }

        public PreparationState CreatePreparationState()
        {
            return new PreparationState(_container.Resolve<PreparationTriggerService>());
        }

        public StageProcessState CreateStageProcessState()
        {
            return new StageProcessState(_container.Resolve<StageProviderService>());
        }

        public WinState CreateWinState(GameplayInputArgs inputArgs)
        {
            return new WinState(
                _container.Resolve<IInputService>(),
                _container.Resolve<LevelsProgressionService>(),
                inputArgs,
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<GameplayPopupService>(),
                _container.Resolve<IPauseService>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<MainHeroHolderService>()
            );
        }

        public DefeatState CreateDefeatState()
        {
            return new DefeatState(
                _container.Resolve<IInputService>(),
                _container.Resolve<GameplayPopupService>(),
                _container.Resolve<IPauseService>()
            );
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs inputArgs)
        {
            PreparationTriggerService preparationTriggerService = _container.Resolve<PreparationTriggerService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            MainHeroHolderService mainHeroHolderService = _container.Resolve<MainHeroHolderService>();

            GameplayStateMachine coreLoopState = CreateCoreLoopState();
            DefeatState defeatState = CreateDefeatState();
            WinState winState = CreateWinState(inputArgs);

            ICompositeCondition coreLoopToWinCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage() == false));

            ICompositeCondition coreLoopToDefeatCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (mainHeroHolderService.MainHero != null)
                        return mainHeroHolderService.MainHero.IsDead.Value;

                    return false;
                }));

            GameplayStateMachine gameplayCycle = new GameplayStateMachine();
            gameplayCycle.AddState(coreLoopState);
            gameplayCycle.AddState(winState);
            gameplayCycle.AddState(defeatState);

            gameplayCycle.AddTransition(coreLoopState, winState, coreLoopToWinCondition);
            gameplayCycle.AddTransition(coreLoopState, defeatState, coreLoopToDefeatCondition);

            return gameplayCycle;
        }

        public GameplayStateMachine CreateCoreLoopState()
        {
            PreparationTriggerService preparationTriggerService = _container.Resolve<PreparationTriggerService>();
            StageProviderService stageProviderService = _container.Resolve<StageProviderService>();
            LootPullingService lootPullingService = _container.Resolve<LootPullingService>();

            CollectLootState collectLootState = CreateCollectLootState();
            PreparationState preparationState = CreatePreparationState();
            StageProcessState stageProcessState = CreateStageProcessState();

            ICompositeCondition preparationToStageProcessCondition = new CompositeCondition()
                .Add(new FuncCondition(() => preparationTriggerService.HasMainHeroContact.Value))
                .Add(new FuncCondition(() => stageProviderService.HasNextStage()));

            FuncCondition stageProcessToCollectStateCondition =
                new FuncCondition(() => stageProviderService.CurrentStageResult.Value == StageResults.Completed);

            FuncCondition collectStateToPreparationCondition =
                new FuncCondition(() => lootPullingService.AllCollected.Value);

            GameplayStateMachine coreLoopState = new GameplayStateMachine();

            coreLoopState.AddState(preparationState);
            coreLoopState.AddState(stageProcessState);
            coreLoopState.AddState(collectLootState);

            coreLoopState.AddTransition(preparationState, stageProcessState, preparationToStageProcessCondition);
            coreLoopState.AddTransition(stageProcessState, collectLootState, stageProcessToCollectStateCondition);
            coreLoopState.AddTransition(collectLootState, preparationState, collectStateToPreparationCondition);

            return coreLoopState;
        }
    }
}
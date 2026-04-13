using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Gameplay.Infrastructure.States.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class GameplayStateFactory
    {
        private readonly DIContainer _container;

        private readonly StageProviderService _stageProviderService;
        private readonly TowerHolderService _towerHolderService;
        private readonly IInputService _inputService;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly AllyFactory _allyFactory;
        private readonly WalletService _walletService;
        private readonly StatisticsService _statisticsService;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ShopConfig _shopConfig;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayPopupService _popupService;

        public GameplayStateFactory(DIContainer container)
        {
            _container = container;

            _stageProviderService = _container.Resolve<StageProviderService>();
            _towerHolderService = _container.Resolve<TowerHolderService>();
            _inputService = _container.Resolve<IInputService>();
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _allyFactory = _container.Resolve<AllyFactory>();
            _walletService = _container.Resolve<WalletService>();
            _statisticsService = _container.Resolve<StatisticsService>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _shopConfig = _container.Resolve<ConfigsProviderService>().GetConfig<ShopConfig>();
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            _popupService = _container.Resolve<GameplayPopupService>();
        }

        public GameplayStateMachine CreateGameplayStateMachine(GameplayInputArgs inputArgs)
        {
            GameplayStateMachine coreLoopState = CreateCoreLoopState();
            DefeatState defeatState = CreateDefeatState();
            WinState winState = CreateWinState(inputArgs);

            ICompositeCondition coreLoopToWinCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (_towerHolderService.Tower != null)
                        return _towerHolderService.Tower.IsDead.Value == false;

                    return true;
                }))
                .Add(new FuncCondition(() => _stageProviderService.CurrentStageResult.Value == StageResults.Completed))
                .Add(new FuncCondition(() => _stageProviderService.HasNextStage() == false))
                ;

            ICompositeCondition coreLoopToDefeatCondition = new CompositeCondition()
                .Add(new FuncCondition(() =>
                {
                    if (_towerHolderService.Tower != null)
                        return _towerHolderService.Tower.IsDead.Value;

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

        private GameplayStateMachine CreateCoreLoopState()
        {
            StageProcessState stageProcessState = CreateStageProcessState();
            CursorAttackState cursorAttackState = CreateCursorAttackState();

            GameplayParallelState battleState = new GameplayParallelState(stageProcessState, cursorAttackState);
            ShopState shopState = CreateCursorShopState();

            GameplayStateMachine coreLoopState = new GameplayStateMachine();

            coreLoopState.AddState(battleState);
            coreLoopState.AddState(shopState);

            ICompositeCondition battleToPurchaseCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProviderService.CurrentStageResult.Value == StageResults.Completed));

            ICompositeCondition purchaseToBattleCondition = new CompositeCondition()
                .Add(new FuncCondition(() => _stageProviderService.HasNextStage()))
                .Add(new FuncCondition(() => _stageProviderService.CurrentStageResult.Value == StageResults.ShopCompleted));

            coreLoopState.AddTransition(battleState, shopState, battleToPurchaseCondition);
            coreLoopState.AddTransition(shopState, battleState, purchaseToBattleCondition);

            return coreLoopState;
        }

        public CursorAttackState CreateCursorAttackState()
        {
            return new CursorAttackState(_inputService, _entitiesFactory, _allyFactory, _entitiesLifeContext);
        }

        private WinState CreateWinState(GameplayInputArgs inputArgs)
        {
            return new WinState(_inputService, inputArgs, _playerDataProvider, _sceneSwitcherService,
                _coroutinesPerformer, _walletService, _statisticsService, _popupService);
        }

        private DefeatState CreateDefeatState()
        {
            return new DefeatState(_inputService, _sceneSwitcherService, _coroutinesPerformer, _statisticsService, _popupService);
        }

        private StageProcessState CreateStageProcessState()
        {
            return new StageProcessState(_stageProviderService);
        }

        private ShopState CreateCursorShopState()
        {
            return new ShopState(_towerHolderService, _allyFactory, _inputService, _walletService, _shopConfig, _popupService);
        }
    }
}
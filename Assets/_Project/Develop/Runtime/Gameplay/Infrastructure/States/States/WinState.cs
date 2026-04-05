using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly WalletService _walletService;

        public WinState(
            IInputService inputService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer, 
            WalletService walletService) : base(inputService)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"ПОБЕДА. Начисление: {_gameplayInputArgs.Level.Reward.Type}: {_gameplayInputArgs.Level.Reward.Value}");
            Debug.Log($"Нажмите Q для перехода в меню");
            
            _walletService.Add(_gameplayInputArgs.Level.Reward.Type, _gameplayInputArgs.Level.Reward.Value);
            _walletService.Add(CurrencyTypes.Wins, 1);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _coroutinesPerformer.StartPerform(
                    _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly WalletService _walletService;
        private readonly StatisticsService _statisticsService;
        private readonly GameplayPopupService _popupService;
        private readonly ShopService _shopService;

        public WinState(
            IInputService inputService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer, 
            WalletService walletService, 
            StatisticsService statisticsService, 
            GameplayPopupService popupService, 
            ShopService shopService) : base(inputService)
        {
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _walletService = walletService;
            _statisticsService = statisticsService;
            _popupService = popupService;
            _shopService = shopService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"ПОБЕДА. Начисление: {_gameplayInputArgs.RewardCurrencyType}: {_gameplayInputArgs.RewardPrice}");
            Debug.Log($"Нажмите Q для перехода в меню");
            
            _walletService.Add(_gameplayInputArgs.RewardCurrencyType, _gameplayInputArgs.RewardPrice);
            _statisticsService.Add(StatisticType.Wins);
            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            
            _popupService.OpenWinPopup();
            
            _shopService.CleanupSpawnedItems();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
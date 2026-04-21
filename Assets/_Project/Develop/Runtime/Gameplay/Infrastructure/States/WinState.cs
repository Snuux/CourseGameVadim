using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Project.Develop.Runtime.Meta.Features.LevelsProgression;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class WinState : EndGameState, IUpdatableState
    {
        private readonly LevelsProgressionService _levelsProgressionService;
        private readonly GameplayInputArgs _gameplayInputArgs;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly WalletService _walletService;
        private readonly MainHeroHolderService _mainHeroHolderService;
        
        private readonly GameplayPopupService _popupService;

        public WinState(
            IInputService inputService,
            LevelsProgressionService levelsProgressionService,
            GameplayInputArgs gameplayInputArgs,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer, 
            GameplayPopupService popupService,
            IPauseService pauseService, 
            WalletService walletService, 
            MainHeroHolderService mainHeroHolderService) : base(inputService, pauseService)
        {
            _levelsProgressionService = levelsProgressionService;
            _gameplayInputArgs = gameplayInputArgs;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;
            _popupService = popupService;
            _walletService = walletService;
            _mainHeroHolderService = mainHeroHolderService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("ПОБЕДА!");
            
            _walletService.Add(CurrencyTypes.Gold, _mainHeroHolderService.MainHero.Coins.Value);
            
            _levelsProgressionService.AddLevelToCompleted(_gameplayInputArgs.LevelNumber);

            _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());

            _popupService.OpenWinPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplayRunningService
    {
        private readonly GameplayStateService _gameplayStateService;
        private readonly InputSequenceService _inputSequenceService;
        private readonly WalletService _walletService;
        private readonly LevelOutcomeService _levelOutcomeService;
        
        private readonly (CurrencyTypes, int) _winRewardGold;
        private readonly (CurrencyTypes, int) _defeatPenaltyGold;

        public GameplayRunningService(
            GameplayStateService gameplayStateService,
            InputSequenceService inputSequenceService,
            (CurrencyTypes, int) winRewardGold,
            (CurrencyTypes, int) defeatPenaltyGold, 
            WalletService walletService, 
            LevelOutcomeService levelOutcomeService)
        {
            _gameplayStateService = gameplayStateService;
            _inputSequenceService = inputSequenceService;
            _winRewardGold = winRewardGold;
            _defeatPenaltyGold = defeatPenaltyGold;
            _walletService = walletService;
            _levelOutcomeService = levelOutcomeService;
        }

        public void Run()
        {
        }

        public void Update(float deltaTime)
        {
            if (_gameplayStateService.State == GameplayState.Run)
            {
                string input = _inputSequenceService.GetCurrentInput();
                _gameplayStateService.StateCompute(input);
            }
            else
            {
                SetOutcomeState();
            }
        }

        private void SetOutcomeState()
        {
            switch (_gameplayStateService.State)
            {
                case GameplayState.Win:
                    _walletService.Add(_winRewardGold.Item1, _winRewardGold.Item2);
                    _levelOutcomeService.Add(GameEndTypes.Win);
                    _inputSequenceService.Clear();
                    
                    _gameplayStateService.SetStopState();
                    
                    break;
                case GameplayState.Defeat:
                    _walletService.Sub(_defeatPenaltyGold.Item1, _defeatPenaltyGold.Item2);
                    _levelOutcomeService.Add(GameEndTypes.Defeat);
                    _inputSequenceService.Clear();
                    
                    _gameplayStateService.SetStopState();
                    
                    break;
            }
        }
    }
}
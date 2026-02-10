using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.GameCycle
{
    public class GameRunningStateHandler
    {
        private RandomSymbolsSequenceGenerationService _randomSymbolsSequenceGenerationService;
        private GameFinishStateHandler _gameFinishStateHandler;
        private InputSequenceService _inputSequenceService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private SceneSwitcherService _sceneSwitcherService;

        private GameStatisticsService _gameStatisticsService;
        private WalletService _walletService;
        private LevelsRewardConfig _levelsRewardConfig;

        public GameRunningStateHandler(
            RandomSymbolsSequenceGenerationService randomSymbolsSequenceGenerationService,
            GameFinishStateHandler gameFinishStateHandler,
            InputSequenceService inputSequenceService,
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            GameStatisticsService gameStatisticsService,
            WalletService walletService,
            LevelsRewardConfig levelsRewardConfig)
        {
            _randomSymbolsSequenceGenerationService = randomSymbolsSequenceGenerationService;
            _gameFinishStateHandler = gameFinishStateHandler;
            _inputSequenceService = inputSequenceService;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _gameStatisticsService = gameStatisticsService;
            _walletService = walletService;
            _levelsRewardConfig = levelsRewardConfig;
        }

        public void Run()
        {
            _inputSequenceService.Clear();

            Debug.Log($"Сгенерированная последовательность: {_randomSymbolsSequenceGenerationService.Sequence}. Повторите:");
        }

        public void Update(float deltaTime)
        {
            if (_gameFinishStateHandler.State == GameFinishState.Running)
            {
                ProcessInput();
                SetStateByInputSymbols();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_gameFinishStateHandler.State == GameFinishState.Win)
                    {
                        _gameStatisticsService.Add(GameStatisticsTypes.Win);
                        _walletService.Add(_levelsRewardConfig.CurrencyType, _levelsRewardConfig.Value);
                    }
                    
                    if (_gameFinishStateHandler.State == GameFinishState.Defeat)
                    {
                        _gameStatisticsService.Add(GameStatisticsTypes.Defeat);
                        _walletService.Sub(_levelsRewardConfig.CurrencyType, _levelsRewardConfig.Value);
                    }

                    SwitchSceneTo(Scenes.MainMenu);
                }
            }
        }

        private void SetStateByInputSymbols() 
            => _gameFinishStateHandler.SetStateByEquality(_inputSequenceService.InputSymbols);

        private void ProcessInput() 
            => _inputSequenceService.ProcessInputKeys();

        private void SwitchSceneTo(string sceneName)
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName));
    }
}
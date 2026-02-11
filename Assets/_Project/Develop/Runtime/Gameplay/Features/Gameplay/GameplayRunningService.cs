using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplayRunningService
    {
        private readonly GameplaySetFinishStateService _gameplaySetFinishStateService;
        private readonly InputSequenceService _inputSequenceService;
        private readonly GameplayGetRewardService _gameplayGetRewardService;
        private readonly GameplaySwitcherSceneService _gameplaySwitcherSceneService;

        private readonly string _generatedSequence;

        public GameplayRunningService(
            string generatedSequence,
            GameplaySetFinishStateService gameplaySetFinishStateService,
            InputSequenceService inputSequenceService,
            GameplayGetRewardService gameplayGetRewardService,
            GameplaySwitcherSceneService gameplaySwitcherSceneService
        )
        {
            _generatedSequence = generatedSequence;
            _gameplaySetFinishStateService = gameplaySetFinishStateService;
            _inputSequenceService = inputSequenceService;
            _gameplayGetRewardService = gameplayGetRewardService;
            _gameplaySwitcherSceneService = gameplaySwitcherSceneService;
        }

        public void Run()
        {
            _inputSequenceService.Clear();

            Debug.Log($"Сгенерированная последовательность: <color=red> {_generatedSequence} </color>. Повторите:");
        }

        public void Update(float deltaTime)
        {
            if (_gameplaySetFinishStateService.State == GameFinishState.Running)
            {
                ProcessInput();
                UpdateStateFromInput();
                ProcessFinish();
            }
            else
            {
                _gameplaySwitcherSceneService.Update(Time.deltaTime);
            }
        }

        private void ProcessFinish()
        {
            if (_gameplaySetFinishStateService.State != GameFinishState.Running)
            {
                if (_gameplaySetFinishStateService.State == GameFinishState.Win)
                    _gameplayGetRewardService.WinProcess();
                else if (_gameplaySetFinishStateService.State == GameFinishState.Defeat)
                    _gameplayGetRewardService.DefeatProcess();
            }
        }

        private void UpdateStateFromInput()
            => _gameplaySetFinishStateService.SetStateByEquality(_inputSequenceService.InputSymbols);

        private void ProcessInput() => _inputSequenceService.ProcessInputKeys();
    }
}
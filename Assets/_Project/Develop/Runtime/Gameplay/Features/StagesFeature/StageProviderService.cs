using System;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        private ReactiveVariable<int> _currentStageNumber = new();
        private ReactiveVariable<StageResults> _currentStageResult = new();

        private GameplayInputArgs _inputArgs;
        private StagesFactory _stagesFactory;
        private IStage _currentStage;

        private IDisposable _stageEndedDisposable;

        public StageProviderService(GameplayInputArgs inputArgs, StagesFactory stagesFactory)
        {
            _inputArgs = inputArgs;
            Debug.Log(inputArgs.ToString());
            _stagesFactory = stagesFactory;
        }

        public ReactiveVariable<int> CurrentStageNumber => _currentStageNumber;

        public ReactiveVariable<StageResults> CurrentStageResult => _currentStageResult;

        public int StagesCount => _inputArgs.StageConfigs.Count;

        public bool HasNextStage() => CurrentStageNumber.Value < StagesCount;
        
        public void SetShopStateCompleted() => _currentStageResult.Value = StageResults.ShopCompleted;

        public void SwitchToNext()
        {
            if (HasNextStage() == false)
                throw new InvalidOperationException($"No next stage!");

            if (_currentStage != null)
                CleanupCurrent();

            _currentStageNumber.Value++;
            _currentStageResult.Value = StageResults.Uncompleted;

            _currentStage = _stagesFactory.Create(_inputArgs.StageConfigs[_currentStageNumber.Value - 1]);
        }

        public void StartCurrent()
        {
            _stageEndedDisposable = _currentStage.Completed.Subscribe(OnStageCompleted);
            _currentStage.Start();
        }

        private void OnStageCompleted()
        {
            _currentStageResult.Value = StageResults.Completed;
        }

        public void UpdateCurrent(float deltaTime) => _currentStage.Update(deltaTime);

        public void CleanupCurrent() => _currentStage.Cleanup();

        public void Dispose()
        {
            _currentStage?.Dispose();
            _stageEndedDisposable?.Dispose();
        }
    }
}
using System;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature.States;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class StageProviderService : IDisposable
    {
        public event Action StageCompleted;
        
        private readonly ReactiveVariable<int> _currentStageNumber = new();
        private readonly ReactiveVariable<StageResults> _currentStageResult = new();

        private readonly GameplayInputArgs _inputArgs;
        private readonly StagesFactory _stagesFactory;
        private IStage _currentStage;

        private IDisposable _stageEndedDisposable;

        public StageProviderService(GameplayInputArgs inputArgs, StagesFactory stagesFactory)
        {
            _inputArgs = inputArgs;
            _stagesFactory = stagesFactory;
        }

        public ReactiveVariable<int> CurrentStageNumber => _currentStageNumber;

        public ReactiveVariable<StageResults> CurrentStageResult => _currentStageResult;

        public int StagesCount => _inputArgs.StageConfigs.Count;

        public bool HasNextStage() => CurrentStageNumber.Value < StagesCount;
        
        public void SetShopStateCompleted() => _currentStageResult.Value = StageResults.ShopCompleted;

        public bool TryAddEnemy(EntityConfig enemyConfig)
        {
            if (_currentStage is not ClearAllEnemiesStage clearAllEnemiesStage)
                return false;

            return clearAllEnemiesStage.AddRuntimeEnemy(enemyConfig);
        }

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
            StageCompleted?.Invoke();
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

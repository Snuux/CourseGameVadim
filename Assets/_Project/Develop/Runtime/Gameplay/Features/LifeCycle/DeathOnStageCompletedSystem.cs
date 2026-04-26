using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class DeathOnStageCompletedSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly StageProviderService _stageProviderService;
        private IDisposable _currentStageResultChangedDisposable;

        private ReactiveVariable<bool> _isDead;

        public DeathOnStageCompletedSystem(StageProviderService stageProviderService)
        {
            _stageProviderService = stageProviderService;
        }

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;

            _currentStageResultChangedDisposable =
                _stageProviderService.CurrentStageResult.Subscribe(OnStageResultChanged);
        }

        private void OnStageResultChanged(StageResults arg1, StageResults stageResult)
        {
            if (stageResult == StageResults.Completed)
                _isDead.Value = true;
        }

        public void OnDispose()
        {
            _currentStageResultChangedDisposable.Dispose();
        }
    }
}
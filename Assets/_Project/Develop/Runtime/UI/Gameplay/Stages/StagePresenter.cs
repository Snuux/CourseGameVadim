using System;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace _Project.Develop.Runtime.UI.Gameplay.Stages
{
    public class StagePresenter : IPresenter
    {
        private readonly IconTextView _view;
        private readonly StageProviderService _stageProviderService;

        IDisposable _currentStageNumberChangedDisposable;

        public StagePresenter(IconTextView view, StageProviderService stageProviderService)
        {
            _view = view;
            _stageProviderService = stageProviderService;
        }

        public void Initialize()
        {
            _currentStageNumberChangedDisposable =
                _stageProviderService.CurrentStageNumber.Subscribe(OnNextIndexChanged);

            UpdateStageNumber();
        }

        public void Dispose()
        {
            _currentStageNumberChangedDisposable.Dispose();
        }

        private void OnNextIndexChanged(int arg1, int arg2)
        {
            UpdateStageNumber();
        }

        private void UpdateStageNumber()
        {
            _view.SetText($"{_stageProviderService.CurrentStageNumber.Value} / {_stageProviderService.StagesCount}");
        }
    }
}
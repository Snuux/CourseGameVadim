using System;
using _Project.Develop.Runtime.Configs.Meta.Statistics;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI.Statistics
{
    public class RecordPresenter : IPresenter
    {
        //Бизнес логика
        private readonly IReadOnlyVariable<int> _record;
        private readonly StatisticType _statisticType;
        private readonly RecordIconsConfig _recordIconsConfig;

        //Визуал
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public RecordPresenter(
            IReadOnlyVariable<int> record, 
            StatisticType statisticType, 
            RecordIconsConfig recordIconsConfig, 
            IconTextView view)
        {
            _record = record;
            _statisticType = statisticType;
            _recordIconsConfig = recordIconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_record.Value);
            _view.SetIcon(_recordIconsConfig.GetSpriteFor(_statisticType));

            _disposable = _record.Subscribe(OnRecordChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnRecordChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
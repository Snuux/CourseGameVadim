using System.Collections.Generic;
using _Project.Develop.Runtime.Factories.UI;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Statistics
{
    public class StatisticsPresenter : IPresenter
    {
        private readonly StatisticsService _statisticsService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;

        private readonly List<RecordPresenter> _recordPresenters = new();

        public StatisticsPresenter(
            StatisticsService statisticsService, 
            ProjectPresentersFactory presentersFactory, 
            ViewsFactory viewsFactory, 
            IconTextListView view)
        {
            _statisticsService = statisticsService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (StatisticType recordType in _statisticsService.AvailableRecords)
            {
                IconTextView currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);

                _view.Add(currencyView);

                RecordPresenter recordPresenter = _presentersFactory.CreateRecordPresenter(
                    currencyView,
                    _statisticsService.GetRecord(recordType),
                    recordType);

                recordPresenter.Initialize();
                _recordPresenters.Add(recordPresenter);
            }
        }

        public void Dispose()
        {
            foreach (RecordPresenter recordPresenter in _recordPresenters)
            {
                _view.Remove(recordPresenter.View);
                _viewsFactory.Release(recordPresenter.View);
                recordPresenter.Dispose();
            }

            _recordPresenters.Clear();
        }
    }
}
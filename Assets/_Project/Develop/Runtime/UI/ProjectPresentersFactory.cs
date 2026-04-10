using _Project.Develop.Runtime.Configs.Meta.Statistics;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Core.TestPopup;
using _Project.Develop.Runtime.UI.Statistics;
using _Project.Develop.Runtime.UI.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyType currencyType)
        {
            return new CurrencyPresenter(
                currency,
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                view);
        }
        
        public RecordPresenter CreateRecordPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            StatisticType statisticType)
        {
            return new RecordPresenter(
                currency,
                statisticType,
                _container.Resolve<ConfigsProviderService>().GetConfig<RecordIconsConfig>(),
                view);
        }

        public StatisticsPresenter CreateStatisticsPresenter(IconTextListView view)
        {
            return new StatisticsPresenter(
                _container.Resolve<StatisticsService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }
        
        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(
                _container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        public TestPopupPresenter CreateTestPopupPresenter(TestPopupView view)
        {
            return new TestPopupPresenter(
                view,
                _container.Resolve<ICoroutinesPerformer>());
        }
    }
}

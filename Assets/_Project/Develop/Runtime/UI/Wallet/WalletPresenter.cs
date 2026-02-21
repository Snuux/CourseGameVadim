using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;
        
        private readonly List<SingleCurrencyPresenter> _currencyPresenters = new();
        
        public WalletPresenter(
            WalletService walletService,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            IconTextListView view)
        {
            _walletService = walletService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (CurrencyTypes currencyType in _walletService.AvailableCurrencies)
            {
                IconTextView currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);

                _view.Add(currencyView); 
                
                SingleCurrencyPresenter singleCurrencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                    currencyView,
                    _walletService.GetCurrency(currencyType),
                    currencyType);
                
                singleCurrencyPresenter.Initialize();
                
                _currencyPresenters.Add(singleCurrencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (SingleCurrencyPresenter presenter in _currencyPresenters)
            {
                _view.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose(); 
            }
            
            _currencyPresenters.Clear();
        }
    }
}
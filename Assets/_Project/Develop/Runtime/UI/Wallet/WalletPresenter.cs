using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.Wallet
{
    public class WalletPresenter : IPresenter
    {
        private readonly WalletService _walletService;
        private readonly ProjectPresenterFactory _presenterFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;
        
        private readonly List<CurrencyPresenter> _currencyPresenters = new();
        
        public WalletPresenter(
            WalletService walletService,
            ProjectPresenterFactory presenterFactory,
            ViewsFactory viewsFactory,
            IconTextListView view)
        {
            _walletService = walletService;
            _presenterFactory = presenterFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (CurrencyTypes currencyType in _walletService.AvailableCurrencies)
            {
                IconTextView currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);

                _view.Add(currencyView); 
                
                CurrencyPresenter currencyPresenter = _presenterFactory.CreateCurrencyPresenter(
                    currencyView,
                    _walletService.GetCurrency(currencyType),
                    currencyType);
                
                currencyPresenter.Initialize();
                
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (CurrencyPresenter presenter in _currencyPresenters)
            {
                _view.Remove(presenter.View);
                _viewsFactory.Release(presenter.View);
                presenter.Dispose(); 
            }
            
            _currencyPresenters.Clear();
        }
    }
}
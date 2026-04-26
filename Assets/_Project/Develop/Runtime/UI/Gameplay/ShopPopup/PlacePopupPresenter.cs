using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.Gameplay.ShopPopup
{
    public class PlacePopupPresenter : PopupPresenterBase
    {
        private readonly PlacePopupView _view;
        private readonly ShopService _shopService;

        public PlacePopupPresenter(
            ICoroutinesPerformer coroutinesPerformer, 
            PlacePopupView view, 
            ShopService shopService) : base(coroutinesPerformer)
        {
            _view = view;
            _shopService = shopService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _shopService.Spawned += OnItemSpawned;
            _view.DeclineButtonClicked += OnDeclineButtonClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _shopService.Spawned -= OnItemSpawned;
            _view.DeclineButtonClicked -= OnDeclineButtonClicked;
        }
        
        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _shopService.Spawned -= OnItemSpawned;
            _view.DeclineButtonClicked -= OnDeclineButtonClicked;
        }
        
        private void OnDeclineButtonClicked()
        {
            _shopService.DeclinePurchase();
            
            OnCloseRequest();
        }

        private void OnItemSpawned()
        {
            OnCloseRequest();
        }
    }
}
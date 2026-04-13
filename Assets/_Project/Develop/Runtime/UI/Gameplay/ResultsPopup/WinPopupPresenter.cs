using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class WinPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU WIN!";
        
        private readonly WinPopupView _view;
        
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        
        public WinPopupPresenter(ICoroutinesPerformer coroutinesPerformer, WinPopupView view,
            SceneSwitcherService sceneSwitcherService) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(TitleName);
            _view.ConinueClicked += OnContinueClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.ConinueClicked -= OnContinueClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.ConinueClicked -= OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService
                .ProcessSwitchTo(Scenes.MainMenu));
            
            OnCloseRequest();
        }
    }
}
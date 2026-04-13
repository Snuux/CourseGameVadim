using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class DefeatPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "YOU LOOSE";
        
        private readonly DefeatPopupView _view;
        
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayInputArgs _gameplayInputArgs;
        
        public DefeatPopupPresenter(ICoroutinesPerformer coroutinesPerformer, DefeatPopupView view, 
            SceneSwitcherService sceneSwitcherService, GameplayInputArgs gameplayInputArgs) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _gameplayInputArgs = gameplayInputArgs;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetTitle(TitleName);
            _view.ContinueClicked += OnContinueClicked;
            _view.RestartClicked += OnRestartClicked;
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
            
            _view.ContinueClicked -= OnContinueClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _view.ContinueClicked -= OnContinueClicked;
            _view.RestartClicked -= OnRestartClicked;
        }

        private void OnContinueClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService
                .ProcessSwitchTo(Scenes.MainMenu));
            
            OnCloseRequest();
        }
        
        private void OnRestartClicked()
        {
            // todo restart func
            //_coroutinesPerformer.StartPerform(_sceneSwitcherService
            //    .ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(_gameplayInputArgs.LevelNumber)));

            
            
            
            OnCloseRequest();
        }
    }
}
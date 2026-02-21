using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayOutcomePopupPresenter : PopupPresenterBase
    {
        private readonly GameplayOutcomePopupView _view;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;

        private string _outcomeText;

        public GameplayOutcomePopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            GameplayOutcomePopupView view,
            string outcomeText, 
            SceneSwitcherService sceneSwitcherService) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _view = view;
            _outcomeText = outcomeText;
            _sceneSwitcherService = sceneSwitcherService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            
            _view.SetInfo(_outcomeText);

            _view.CloseRequest += SwitchSceneToMain;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.CloseRequest -= SwitchSceneToMain;
        }

        private void SwitchSceneToMain()
            => _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
    }
}
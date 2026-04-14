using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Meta.Features.Levels;
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
        private readonly ILevelConfigProviderService _levelConfigProviderService;

        public DefeatPopupPresenter(ICoroutinesPerformer coroutinesPerformer, DefeatPopupView view,
            SceneSwitcherService sceneSwitcherService,
            ILevelConfigProviderService levelConfigProviderService) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _levelConfigProviderService = levelConfigProviderService;
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
            LevelConfig levelConfig = _levelConfigProviderService.Get();

            _coroutinesPerformer.StartPerform(
                _sceneSwitcherService.ProcessSwitchTo(
                    Scenes.Gameplay,
                    new GameplayInputArgs(
                        levelConfig.Reward.Type,
                        levelConfig.Reward.Value,
                        levelConfig.TowerMaxHealth,
                        levelConfig.StageConfigs)));

            OnCloseRequest();
        }
    }
}
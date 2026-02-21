using _Project.Develop.Runtime.Gameplay.Features.Gameplay;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly InputSequenceService _inputSequenceService;
        private readonly string _sequence;
        
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly GameplayStateService _gameplayStateService;
        
        private readonly GameplayPopupService _popupService;
        
        public GameplayScreenPresenter(
            GameplayScreenView screen,
            string sequence, 
            InputSequenceService inputSequenceService, 
            ICoroutinesPerformer coroutinesPerformer, 
            SceneSwitcherService sceneSwitcherService, 
            GameplayPopupService popupService, 
            GameplayStateService gameplayStateService)
        {
            _screen = screen;
            _sequence = sequence;
            _inputSequenceService = inputSequenceService;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _popupService = popupService;
            _gameplayStateService = gameplayStateService;
        }

        public void Initialize()
        {
            _screen.SetSequence(_sequence);
            _screen.CloseGameplayScreenButtonClicked += OnCloseButtonPressed;
            _inputSequenceService.InputPress += OnInput;
            _gameplayStateService.StateChanged += OnWinDefeat;
        }

        public void Dispose()
        {
            _screen.CloseGameplayScreenButtonClicked -= OnCloseButtonPressed;
            _inputSequenceService.InputPress -= OnInput;
            _gameplayStateService.StateChanged -= OnWinDefeat;
        }

        private void OnInput(string input)
        {
            _screen.SetInput(input);
        }

        public void OnCloseButtonPressed()
        {
            SwitchSceneTo(Scenes.MainMenu);
        }
        
        public void OnWinDefeat(GameplayState gameplayState)
        {
            if (gameplayState == GameplayState.Defeat || gameplayState == GameplayState.Win)
                _popupService.OpenGameplayOutcomePopupPresenter(gameplayState.ToString() + "!");
        }
        
        private void SwitchSceneTo(string sceneName)
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName));
    }
}
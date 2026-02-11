using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplaySwitcherSceneService
    {
        private readonly GameplaySetFinishStateService _gameplaySetFinishStateService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;

        public GameplaySwitcherSceneService(
            GameplaySetFinishStateService gameplaySetFinishStateService,
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService)
        {
            _gameplaySetFinishStateService = gameplaySetFinishStateService;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
        }

        public void Update(float deltaTime)
        {
            if (_gameplaySetFinishStateService.State == GameFinishState.Running)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                SwitchSceneTo(Scenes.MainMenu);
        }

        private void SwitchSceneTo(string sceneName)
            => _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(sceneName));
    }
}
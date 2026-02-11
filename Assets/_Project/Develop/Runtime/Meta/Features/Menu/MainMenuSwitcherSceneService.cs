using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Features.Menu
{
    class MainMenuSwitcherSceneService
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public MainMenuSwitcherSceneService(ConfigsProviderService configsProviderService,
            SceneSwitcherService sceneSwitcherService, ICoroutinesPerformer coroutinesPerformer)
        {
            _configsProviderService = configsProviderService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ChangeSceneBy(LevelTypes.Letters);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ChangeSceneBy(LevelTypes.Digits);
        }

        public void ChangeSceneBy(LevelTypes type)
        {
            LevelsConfig levelsConfig = _configsProviderService.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.GetLevelConfigBy(type);

            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    levelConfig.Length,
                    levelConfig.Symbols
                )));
        }
    }
}
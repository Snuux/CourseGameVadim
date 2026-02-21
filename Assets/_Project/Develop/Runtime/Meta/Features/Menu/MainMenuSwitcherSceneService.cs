using _Project.Develop.Runtime.Configs.Gameplay.Levels;
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

        public void ChangeSceneBy(LevelTypes type)
        {
            LevelsConfig levelsConfig = _configsProviderService.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.GetLevelConfigBy(type);

            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    levelConfig.Length,
                    levelConfig.Symbols,
                    (levelConfig.WinReward.Type, levelConfig.WinReward.Value) ,
                    (levelConfig.DefeatPenalty.Type, levelConfig.DefeatPenalty.Value) 
                )));
        }
    }
}
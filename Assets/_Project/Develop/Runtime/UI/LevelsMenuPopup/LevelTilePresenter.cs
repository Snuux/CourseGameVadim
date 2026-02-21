using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscribePresenter
    {
        private readonly ConfigsProviderService _configsProviderService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly SceneSwitcherService _sceneSwitcherService;

        private readonly LevelTypes _levelType;
        private readonly LevelTileView _view;

        public LevelTilePresenter(
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            LevelTileView view, 
            LevelTypes levelTypes, 
            ConfigsProviderService configsProviderService)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _view = view;
            _levelType = levelTypes;
            _configsProviderService = configsProviderService;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            
        }

        public void Subscribe()
        {
            _view.Clicked += OnViewClicked;
        }

        public void Unsubscribe()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            LevelsConfig levelsConfig = _configsProviderService.GetConfig<LevelsConfig>();
            LevelConfig levelConfig = levelsConfig.GetLevelConfigBy(_levelType);
            
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(
                Scenes.Gameplay, new GameplayInputArgs(
                    levelConfig.Length,
                    levelConfig.Symbols,
                    (levelConfig.WinReward.Type, levelConfig.WinReward.Value) ,
                    (levelConfig.DefeatPenalty.Type, levelConfig.DefeatPenalty.Value) 
                )));
        }

        public void Dispose()
        {
            _view.Clicked -= OnViewClicked;
        }
    }
}
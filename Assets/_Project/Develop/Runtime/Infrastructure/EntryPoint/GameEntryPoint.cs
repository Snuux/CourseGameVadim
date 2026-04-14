using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Levels;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.LoadingScreen;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Старт проекта, сетап настроек");

            SetupAppSettings();

            Debug.Log("Процесс регистрации сервисов всего проекта");

            DIContainer projectContainer = new DIContainer();
            ProjectContextRegistrations.Process(projectContainer);
            
            projectContainer.Initialize();
            projectContainer.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();
            ConfigsProviderService configsProviderService = container.Resolve<ConfigsProviderService>();

            loadingScreen.Show();

            Debug.Log("Начинается инициализация сервисов");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();

            bool isPlayerDataSaveExists = false;

            yield return playerDataProvider.ExistsAsync(result => isPlayerDataSaveExists = result);

            if (isPlayerDataSaveExists)
                yield return playerDataProvider.LoadAsync();
            else
                playerDataProvider.Reset();

            Debug.Log("Завершается инициализация сервисов");

            loadingScreen.Hide();

            // For test from gameplay state level #levelToTest
            
            //const int levelToTest = 1;
            //LevelsListConfig levelsListConfig = configsProviderService.GetConfig<LevelsListConfig>();
            //List<LevelConfig> levelConfigs = new List<LevelConfig>(levelsListConfig.Levels);
            //
            //LevelConfig levelConfig = levelConfigs[levelToTest];
            //    
            //yield return sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay,
            //    new GameplayInputArgs(
            //        levelConfig.Reward.Type,
            //        levelConfig.Reward.Value,
            //        levelConfig.TowerMaxHealth,
            //        levelConfig.StageConfigs));
            
            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
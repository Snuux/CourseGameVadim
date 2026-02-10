using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Levels;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню");
            
            container.RegisterAsSingle(CreateChangeSceneByLevelTypeService);
        }
        
        private static SwitcherSceneByLevelService CreateChangeSceneByLevelTypeService(DIContainer c)
        {
            ConfigsProviderService configsProviderService = c.Resolve<ConfigsProviderService>();
            SceneSwitcherService sceneSwitcherService = c.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new SwitcherSceneByLevelService(configsProviderService, sceneSwitcherService, coroutinesPerformer);
        }
    }
}

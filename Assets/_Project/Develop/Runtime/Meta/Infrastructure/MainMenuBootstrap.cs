using System.Collections;
using _Project.Develop.Runtime.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Menu;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MainMenuSwitcherSceneService _mainMenuSwitcherSceneService;
        private MainMenuRunningService _mainMenuRunningService;

        private bool _running;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            Debug.Log("Процесс регистрации сервисов главного меню");

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            _mainMenuSwitcherSceneService = _container.Resolve<MainMenuSwitcherSceneService>();
            _mainMenuRunningService = _container.Resolve<MainMenuRunningService>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");
            
            _mainMenuRunningService.Run();
            _running = true;
        }


        private void Update()
        {
            if (_running == false)
                return;

            _mainMenuSwitcherSceneService.Update(Time.deltaTime);
            _mainMenuRunningService.Update(Time.deltaTime);
        }
    }
}
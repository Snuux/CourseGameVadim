using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.UI.StatsUpgradePopup
{
    public class CharacterPreviewPresenter : IPresenter
    {
        private readonly SceneLoaderService _sceneLoader;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public CharacterPreviewPresenter(SceneLoaderService sceneLoader, ICoroutinesPerformer coroutinePerformer)
        {
            _sceneLoader = sceneLoader;
            _coroutinesPerformer = coroutinePerformer;
        }

        public void Initialize()
        {
            _coroutinesPerformer.StartPerform(_sceneLoader.LoadAsync(Scenes.CharacterPreviewScene, LoadSceneMode.Additive));
        }

        public void Dispose()
        {
            _coroutinesPerformer.StartPerform(_sceneLoader.UnloadAsync(Scenes.CharacterPreviewScene));
        }
    }
}

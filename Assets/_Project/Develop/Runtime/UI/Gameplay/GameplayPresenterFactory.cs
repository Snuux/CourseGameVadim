using _Project.Develop.Runtime.Gameplay.Features.Gameplay;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresenterFactory
    {
        private readonly DIContainer _container;

        public GameplayPresenterFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view, string sequence)
        {
            return new GameplayScreenPresenter(
                view,
                sequence,
                _container.Resolve<InputSequenceService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<GameplayPopupService>(),
                _container.Resolve<GameplayStateService>());
        }
    }
}
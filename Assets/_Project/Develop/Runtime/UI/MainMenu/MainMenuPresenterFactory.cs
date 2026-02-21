using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuPresenterFactory
    {
        private readonly DIContainer _container;

        public MainMenuPresenterFactory(DIContainer container)
        {
            _container = container;
        }

        public MainMenuScreenPresenter CreateMainMenuScreen(MainMenuScreenView view)
        {
            return new MainMenuScreenPresenter(
                view,
                _container.Resolve<ProjectPresentersFactory>(),
                _container.Resolve<MainMenuPopupService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>());
        }
    }
}
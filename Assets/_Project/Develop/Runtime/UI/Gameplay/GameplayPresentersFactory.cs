using _Project.Develop.Runtime.Configs.Gameplay;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Gameplay.Infrastructure;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.AbilitySelectPopup;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Experience;
using _Project.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Project.Develop.Runtime.UI.Gameplay.ResultsPopup;
using _Project.Develop.Runtime.UI.Gameplay.Stages;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;
        private readonly GameplayInputArgs _gameplayInputArgs;

        public GameplayPresentersFactory(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;
            _gameplayInputArgs = gameplayInputArgs;
        }

        public MainHeroExperiencePresenter CreateMainHeroExperiencePresenter(BarWithText view)
        {
            return new MainHeroExperiencePresenter(
                view,
                _container.Resolve<MainHeroHolderService>(),
                _container.Resolve<ConfigsProviderService>().GetConfig<ExperienceForUpgradeConfig>());
        }

        public AbilitySelectPopupPresenter CreateAbilitySelectPopupPresenter(
            AbilitySelectPopupView view,
            Entity entity,
            int level)
        {
            return new AbilitySelectPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                entity,
                _container.Resolve<AbilityDropService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                level);
        }

        public SelectableAbilityPresenter CreateSelectableAbilityPresenter(
            AbilityConfig abilityConfig,
            SelectableAbilityView view,
            Entity entity,
            int level)
        {
            return new SelectableAbilityPresenter(
                abilityConfig,
                view,
                _container.Resolve<AbilityFactory>(),
                entity,
                level
                );
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new DefeatPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>(),
                _gameplayInputArgs);
        }

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new WinPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public StagePresenter CreateStagePresenter(IconTextView view)
        {
            return new StagePresenter(view, _container.Resolve<StageProviderService>());
        }

        public GameplayScreenPresenter CreateGameplayScreenPresenter(GameplayScreenView view)
        {
            return new GameplayScreenPresenter(
                view,
                _container.Resolve<GameplayPresentersFactory>(),
                _container.Resolve<MainHeroHolderService>(),
                _container.Resolve<ProjectPresentersFactory>());
        }

        public EntityHealthPresenter CreateEntityHealthPresenter(Entity entity, BarWithText view)
        {
            return new EntityHealthPresenter(entity, view);
        }

        public EntitiesHealthDisplayPresenter CreateEntitiesHealthDisplayPresenter(EntitiesHealthDisplay view)
        {
            return new EntitiesHealthDisplayPresenter(
                _container.Resolve<EntitiesLifeContext>(),
                view,
                _container.Resolve<ViewsFactory>(),
                this);
        }
    }
}
using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Experience;
using _Project.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Project.Develop.Runtime.UI.Gameplay.Stages;
using _Project.Develop.Runtime.UI.Wallet;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainHeroHolderService _mainHeroHolderService;

        private readonly List<IPresenter> _childPresenters = new();

        private EntitiesHealthDisplayPresenter _entityToHealthDisplayPresenter;
        private IDisposable _mainHeroHolderServiceDisposable;
        private CurrencyPresenter _mainHeroCoinsPresenter;

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            GameplayPresentersFactory gameplayPresentersFactory,
            MainHeroHolderService mainHeroHolderService,
            ProjectPresentersFactory projectPresentersFactory)
        {
            _screen = screen;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _mainHeroHolderService = mainHeroHolderService;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            CreateStageNumer();
            CreateEntitiesHealthDisplay();
            CreateMainHeroExperienceView();

            _mainHeroHolderServiceDisposable = _mainHeroHolderService.HeroRegistered.Subscribe(OnHeroRegistered);

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            _mainHeroHolderService?.Dispose();
            _mainHeroCoinsPresenter?.Dispose();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        public void LateUpdate()
        {
            _entityToHealthDisplayPresenter.LateUpdate();
        }

        private void CreateStageNumer()
        {
            StagePresenter stagePresenter = _gameplayPresentersFactory.CreateStagePresenter(_screen.StageView);

            _childPresenters.Add(stagePresenter);
        }

        private void CreateEntitiesHealthDisplay()
        {
            _entityToHealthDisplayPresenter =
                _gameplayPresentersFactory.CreateEntitiesHealthDisplayPresenter(_screen.EntitiesHealthDisplay);
            _childPresenters.Add(_entityToHealthDisplayPresenter);
        }

        private void CreateMainHeroExperienceView()
        {
            MainHeroExperiencePresenter experiencePresenter =
                _gameplayPresentersFactory.CreateMainHeroExperiencePresenter(_screen.ExperienceBarView);

            _childPresenters.Add(experiencePresenter);
        }

        private void OnHeroRegistered(Entity entity)
        {
            _mainHeroCoinsPresenter = _projectPresentersFactory.CreateCurrencyPresenter(
                    _screen.CoinsView, entity.Coins, CurrencyTypes.Gold);
            
            _mainHeroCoinsPresenter.Initialize();
        }
    }
}
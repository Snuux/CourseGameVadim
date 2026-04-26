using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
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

        private readonly List<IPresenter> _childPresenters = new();

        private EntitiesHealthDisplayPresenter _entityToHealthDisplayPresenter;
        private CurrencyPresenter _coinsPresenter;

        private readonly WalletService _walletService;

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            GameplayPresentersFactory gameplayPresentersFactory,
            ProjectPresentersFactory projectPresentersFactory,
            WalletService walletService)
        {
            _screen = screen;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
            _walletService = walletService;
        }

        public void Initialize()
        {
            CreateStageNumer();
            CreateEntitiesHealthDisplay();
            CreateCoinsDisplay();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        private void CreateCoinsDisplay()
        {
            CurrencyPresenter currencyPresenter = _projectPresentersFactory.CreateCurrencyPresenter(
                _screen.CoinsView,
                _walletService.GetCurrency(CurrencyType.Gold),
                CurrencyType.Gold);
            
            _childPresenters.Add(currencyPresenter);
        }

        public void Dispose()
        {
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
    }
}
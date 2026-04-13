using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay.HealthDisplay;
using _Project.Develop.Runtime.UI.Gameplay.Stages;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly List<IPresenter> _childPresenters = new();
        
        private EntitiesHealthDisplayPresenter _entityToHealthDisplayPresenter;

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            GameplayPresentersFactory gameplayPresentersFactory)
        {
            _screen = screen;
            _gameplayPresentersFactory = gameplayPresentersFactory;
        }

        public void Initialize()
        {
            CreateStageNumber();
            CreateEntitiesHealthDisplay();
            
            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
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

        private void CreateStageNumber()
        {
            StagePresenter stagePresenter = _gameplayPresentersFactory.CreateStagePresenter(_screen.StageView);
            
            _childPresenters.Add(stagePresenter);
        }

        private void CreateEntitiesHealthDisplay()
        {
            _entityToHealthDisplayPresenter = _gameplayPresentersFactory.CreateEntitiesHealthDisplayPresenter(_screen.EntitiesHealthDisplay);
            _childPresenters.Add(_entityToHealthDisplayPresenter);
        }
    }
}
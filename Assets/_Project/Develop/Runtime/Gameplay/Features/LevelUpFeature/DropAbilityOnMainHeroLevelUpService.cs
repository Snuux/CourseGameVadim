using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.UI.AbilitySelectPopup;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LevelUpFeature
{
    public class DropAbilityOnMainHeroLevelUpService : IInitializable, IDisposable
    {
        private MainHeroHolderService _mainHeroHolderService;
        private GameplayPopupService _popupService;
        private ICoroutinesPerformer _coroutinesPerformer;
        private IPauseService _pauseService;

        private Queue<int> _levelUpRequest = new();

        private AbilitySelectPopupPresenter _popup;
        private Coroutine _selectAbilityProcess;

        private IDisposable _heroRegisteredDisposable;
        private IDisposable _heroLevelUpDisposable;

        public DropAbilityOnMainHeroLevelUpService(
            MainHeroHolderService mainHeroHolderService,
            GameplayPopupService popupService,
            ICoroutinesPerformer coroutinesPerformer,
            IPauseService pauseService)
        {
            _mainHeroHolderService = mainHeroHolderService;
            _popupService = popupService;
            _coroutinesPerformer = coroutinesPerformer;
            _pauseService = pauseService;
        }

        private bool PopupIsOpened => _popup != null;

        public void Initialize()
        {
            _heroRegisteredDisposable = _mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered);
        }

        public void Dispose()
        {
            _heroRegisteredDisposable?.Dispose();
            _heroLevelUpDisposable?.Dispose();
        }

        private void OnMainHeroRegistered(Entity hero)
        {
            _heroLevelUpDisposable = hero.Level.Subscribe(OnHeroLevelChanged);
        }

        private void OnHeroLevelChanged(int arg1, int currentLevel)
        {
            _levelUpRequest.Enqueue(currentLevel);

            if (_selectAbilityProcess != null)
                return;

            _selectAbilityProcess = _coroutinesPerformer.StartPerform(SelectAbilityProcess());
        }

        private IEnumerator SelectAbilityProcess()
        {
            while (_levelUpRequest.Count > 0)
            {
                int level = _levelUpRequest.Dequeue();

                _pauseService.Pause();
                _popup = _popupService.OpenAbilityPopupPresenter(_mainHeroHolderService.MainHero, 
                    level, () =>
                    {
                        _popup = null;
                        _pauseService.Unpause();
                    });

                yield return new WaitUntil(() => PopupIsOpened == false);
            }

            _selectAbilityProcess = null;
        }
    }
}
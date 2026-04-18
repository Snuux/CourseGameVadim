using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Experience
{
    public class MainHeroExperiencePresenter : IPresenter
    {
        private BarWithText _view;

        private MainHeroHolderService _mainHeroHolderService;
        private ExperienceForUpgradeConfig _levelUpConfig;
        private ReactiveVariable<float> _experience;
        private ReactiveVariable<int> _currentLevel;
        
        private List<IDisposable> _disposables = new();

        public MainHeroExperiencePresenter(
            BarWithText view,
            MainHeroHolderService mainHeroHolderService,
            ExperienceForUpgradeConfig experienceForUpgradeConfig)
        {
            _view = view;
            _mainHeroHolderService = mainHeroHolderService;
            _levelUpConfig = experienceForUpgradeConfig;
        }

        public void Initialize()
        {
            _disposables.Add(_mainHeroHolderService.HeroRegistered.Subscribe(OnMainHeroRegistered));
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables) 
                disposable.Dispose();
        }

        private void OnMainHeroRegistered(Entity entity)
        {
            _experience = entity.Experience;
            _currentLevel = entity.Level;
            
            _disposables.Add(_experience.Subscribe(OnCurrentExperienceChanged));
            _disposables.Add(_currentLevel.Subscribe(OnCurrentLevelChanged));
            
            UpdateBarText(_currentLevel.Value);
            
            UpdateCurrentExperience(_experience.Value);
        }

        private void UpdateCurrentExperience(float value)
        {
            _view.UpdateSlider(value / _levelUpConfig.GetExperienceFor(_currentLevel.Value));
        }

        private void UpdateBarText(int currentLevel)
        {
            Debug.Log("UpdateBarText: " + currentLevel);
            _view.UpdateText($"Lv. {currentLevel}");
        }

        private void OnCurrentLevelChanged(int arg1, int currentLevel)
        {
            UpdateBarText(currentLevel);
        }

        private void OnCurrentExperienceChanged(float arg1, float experience)
        {
            UpdateCurrentExperience(experience);
        }
    }
}
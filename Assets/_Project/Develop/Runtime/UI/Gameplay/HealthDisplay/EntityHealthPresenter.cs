using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay.HealthDisplay
{
    public class EntityHealthPresenter : IPresenter
    {
        private BarWithText _bar;
        private Entity _entity;

        private ReactiveVariable<Teams> _team;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<float> _maxHealth;

        private List<IDisposable> _disposables = new();

        public EntityHealthPresenter(Entity entity, BarWithText bar)
        {
            _bar = bar;
            _entity = entity;
        }
        
        public BarWithText Bar => _bar;

        public void Initialize()
        {
            _health = _entity.CurrentHealth;
            _maxHealth = _entity.MaxHealth;
            _team = _entity.Team;

            _disposables.Add(_health.Subscribe(OnHealthChanged));
            _disposables.Add(_maxHealth.Subscribe(OnMaxHealthChanged));
            _disposables.Add(_team.Subscribe(OnTeamChanged));

            UpdateHealth();
            UpdateFillerColorBy(_team.Value);
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in _disposables) 
                disposable.Dispose();
        }

        private void OnHealthChanged(float oldValue, float newValue) => UpdateHealth();

        private void OnMaxHealthChanged(float oldValue, float newValue) => UpdateHealth();

        private void OnTeamChanged(Teams oldValue, Teams newValue) => UpdateFillerColorBy(newValue);

        private void UpdateHealth()
        {
            _bar.UpdateText(_health.Value.ToString("0"));
            _bar.UpdateValue(_health.Value / _maxHealth.Value);
        }

        private void UpdateFillerColorBy(Teams team)
        {
            if (team == Teams.Ally)
                _bar.SetFillerColor(Color.green);
            else if (team == Teams.Enemies)
                _bar.SetFillerColor(Color.red);
        }
    }
}
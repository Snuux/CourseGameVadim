using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergyRecoverySystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _energyRecoverAmount;
        private ReactiveVariable<float> _energyRecoverInterval;
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _currentEnergy;

        private float _timer;

        public void OnInit(Entity entity)
        {
            _energyRecoverAmount = entity.EnergyRecoverAmount;
            _energyRecoverInterval = entity.EnergyRecoverInterval;
            _maxEnergy = entity.MaxEnergy;
            _currentEnergy = entity.CurrentEnergy;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_currentEnergy.Value >= _maxEnergy.Value)
                return;
            
            _timer += deltaTime;

            if (_timer >= _energyRecoverInterval.Value)
            {
                _currentEnergy.Value = Math.Min(_maxEnergy.Value, _currentEnergy.Value + _energyRecoverAmount.Value);
                _timer = 0;

                Debug.Log($"Энергия восстановилась {_currentEnergy.Value}");
            }
        }
    }
}
using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergySpendSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentEnergy;

        private ReactiveEvent<float> _energySpendRequest;
        private ReactiveEvent _energySpendEvent;

        private IDisposable _energySpendRequestDisposable;

        public void OnInit(Entity entity)
        {
            _currentEnergy = entity.CurrentEnergy;
            _energySpendEvent = entity.EnergySpendEvent;
            _energySpendRequest = entity.EnergySpendRequest;

            _energySpendRequestDisposable = _energySpendRequest.Subscribe(OnEnergySpend);
        }

        public void OnDispose()
        {
            _energySpendRequestDisposable.Dispose();
        }

        private void OnEnergySpend(float energyToSpent)
        {
            if (_currentEnergy.Value >= energyToSpent)
            {
                _currentEnergy.Value = Math.Max(0, _currentEnergy.Value - energyToSpent);

                Debug.Log($"Энергии потратилось: {energyToSpent}");

                _energySpendEvent.Invoke();
            }
            else
            {
                Debug.Log($"Недостаточно энергии, нужно больше: {energyToSpent}");
            }
        }
    }
}
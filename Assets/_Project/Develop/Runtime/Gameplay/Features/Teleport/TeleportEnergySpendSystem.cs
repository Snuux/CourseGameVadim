using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportEnergySpendSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _teleportInProcess;
        private ReactiveVariable<float> _teleportCostEnergy;
        private ReactiveEvent<float> _energySpendRequest;

        public void OnInit(Entity entity)
        {
            _teleportInProcess = entity.TeleportInProcess;
            _teleportCostEnergy = entity.TeleportCostEnergy;
            _energySpendRequest = entity.EnergySpendRequest;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_teleportInProcess.Value)
                _energySpendRequest.Invoke(_teleportCostEnergy.Value);
        }
    }
}
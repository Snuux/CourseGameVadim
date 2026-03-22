using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportInitiationSystem : IInitializableSystem, IDisposableSystem
    {
        private ICompositeCondition _canStartTeleport;

        private ReactiveEvent _teleportEvent;
        private ReactiveEvent _doTeleportInTargetPositionRequest;

        private ReactiveEvent<float> _energySpendRequest;
        private ReactiveVariable<float> _teleportationCostEnergy;
        
        private IDisposable _teleportRequestDisposable;

        public void OnInit(Entity entity)
        {
            _teleportEvent = entity.TeleportEvent;

            _canStartTeleport = entity.CanStartTeleport;

            _energySpendRequest = entity.EnergySpendRequest;
            _teleportationCostEnergy = entity.TeleportationCostEnergy;
            
            _doTeleportInTargetPositionRequest = entity.DoTeleportInTargetPositionRequest;
                
            _teleportRequestDisposable = entity.TeleportRequest.Subscribe(OnStartTeleport);
        }

        public void OnDispose()
        {
            _teleportRequestDisposable.Dispose();
        }

        private void OnStartTeleport()
        {
            if (_canStartTeleport.Evaluate() == false)
            {
                Debug.Log("Нельзя начать телепорт");
                return;
            }
            
            _energySpendRequest.Invoke(_teleportationCostEnergy.Value);
            _doTeleportInTargetPositionRequest.Invoke();
            
            Debug.Log("Телепортация завершена");
            _teleportEvent.Invoke();
        }
    }
}
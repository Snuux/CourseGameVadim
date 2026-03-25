using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportInitSystem : IInitializableSystem, IUpdatableSystem
    {
        private ICompositeCondition _canStartTeleport;
        private ReactiveVariable<bool> _teleportRequested;
        private ReactiveVariable<bool> _teleportInProcess;

        public void OnInit(Entity entity)
        {
            _canStartTeleport = entity.CanStartTeleport;
            _teleportRequested = entity.TeleportRequested;
            _teleportInProcess = entity.TeleportInProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_teleportRequested.Value == false)
                return;

            _teleportRequested.Value = false;

            if (_canStartTeleport.Evaluate())
            {
                _teleportInProcess.Value = true;

                Debug.Log("Телепортация начата");

                return;
            }

            Debug.Log("Нельзя начать телепорт");
        }
    }
}
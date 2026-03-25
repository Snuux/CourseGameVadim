using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportEndSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _teleportInProcess;

        public void OnInit(Entity entity)
        {
            _teleportInProcess = entity.TeleportInProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_teleportInProcess.Value)
            {
                _teleportInProcess.Value = false;
                
                Debug.Log("Телепортация завершена");
            }
        }
    }
}
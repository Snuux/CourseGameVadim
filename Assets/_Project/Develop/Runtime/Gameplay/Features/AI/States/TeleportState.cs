using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportState : State, IUpdatableState
    {
        private ReactiveVariable<bool> _teleportRequested;
        
        public TeleportState(Entity entity)
        {
            _teleportRequested = entity.TeleportRequested;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("Состояние старта телеппорта запущено");
            _teleportRequested.Value = true;
        }

        public void Update(float deltaTime)
        {
        }
    }
}
using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportSetTargetPositionState : State, IUpdatableState
    {
        private readonly ReactiveVariable<Vector3> _teleportTargetPosition;
        private readonly Entity _entity;

        private readonly Func<Entity, Vector3> _positionResolver;

        public TeleportSetTargetPositionState(Entity entity, Func<Entity, Vector3> positionResolver)
        {
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _entity = entity;
            _positionResolver = positionResolver;
        }

        public override void Enter()
        {
            base.Enter();

            _teleportTargetPosition.Value = _positionResolver.Invoke(_entity);
        }

        public void Update(float deltaTime)
        {
        }
    }
}
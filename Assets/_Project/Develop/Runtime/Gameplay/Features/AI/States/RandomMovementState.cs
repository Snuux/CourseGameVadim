using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RandomMovementState : State, IUpdatableState
    {
        private ReactiveVariable<Vector3> _movementDirection;
        private ReactiveVariable<Vector3> _movementRotation;

        private float _cooldownBetweenDirectionGeneration;

        private float _time;

        public RandomMovementState(Entity entity, float cooldownBetweenDirectionGeneration)
        {
            _movementDirection = entity.MoveDirection;
            _movementRotation = entity.RotationDirection;

            _cooldownBetweenDirectionGeneration = cooldownBetweenDirectionGeneration;
        }

        public override void Enter()
        {
            base.Enter();

            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1), 0, Random.Range(-1f, 1f)).normalized;

            _movementDirection.Value = randomDirection;
            _movementRotation.Value = randomDirection;

            _time = 0;
        }

        public override void Exit()
        {
            base.Exit();
            
            _movementDirection.Value = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldownBetweenDirectionGeneration)
            {
                GenerateNewDirection();
                _time = 0;
            }
        }

        private void GenerateNewDirection()
        {
            Vector3 invertedDirection = _movementDirection.Value.normalized * -1;
            Quaternion randomTurn = Quaternion.Euler(Random.Range(-30, 30), Random.Range(-30, 30), 0);
            Vector3 newDirection = randomTurn * invertedDirection;

            _movementDirection.Value = newDirection;
            _movementRotation.Value = newDirection;
        }
    }
}
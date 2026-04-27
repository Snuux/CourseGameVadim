using System;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class GameplayStateContext : IDisposable
    {
        public event Action<IState> StateChanged;
        
        private readonly GameplayStateMachine _gameplayStateMachine;

        private bool _isRunning;
        
        public GameplayStateContext(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _gameplayStateMachine.StateChanged += OnStateChanged;
        }

        public void Run()
        {
            _gameplayStateMachine.Enter();
            _isRunning = true;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;
            
            _gameplayStateMachine.Update(deltaTime);
        }

        public void Dispose()
        {
            _isRunning = false;
            _gameplayStateMachine.StateChanged -= OnStateChanged;
            _gameplayStateMachine.Dispose();
        }

        private void OnStateChanged(IState state)
        {
            StateChanged?.Invoke(state);
        }
    }
}
using System;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class GameplayStateContext : IDisposable
    {
        private readonly GameplayStateMachine _gameplayStateMachine;

        private bool _isRunning;
        
        public GameplayStateContext(GameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
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
            _gameplayStateMachine.Dispose();
        }
    }
}
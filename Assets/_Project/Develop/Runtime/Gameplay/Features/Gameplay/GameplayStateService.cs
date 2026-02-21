using System;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplayStateService
    {
        public event Action<GameplayState> StateChanged;
        
        private readonly int _targetLength;
        private readonly string _sourceSequence;

        private GameplayState _state;

        public GameplayState State
        {
            get => _state;
            private set
            {
                GameplayState oldState = _state;
                _state = value;
                
                if (oldState.Equals(_state) == false)
                    StateChanged?.Invoke(_state);
            }
        }

        public GameplayStateService(int targetLength, string sourceSequence)
        {
            _targetLength = targetLength;
            _sourceSequence = sourceSequence;

            State = GameplayState.Run;
        }

        public void StateCompute(string inputSymbols)
        {
            if (inputSymbols.Length > _targetLength)
            {
                State = GameplayState.Defeat;
                return;
            }

            if (inputSymbols.Length < _targetLength)
            {
                State = GameplayState.Run;
                return;
            }

            if (_sourceSequence.Equals(inputSymbols))
            {
                State = GameplayState.Win;
                return;
            }

            State = GameplayState.Defeat;
        }

        public void SetStopState() => State = GameplayState.Stop;
    }
}
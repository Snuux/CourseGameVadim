using _Project.Develop.Runtime.Gameplay.Features.Sequences;

namespace _Project.Develop.Runtime.Gameplay.Features.Gameplay
{
    public class GameplaySetFinishStateService
    {
        private readonly int _targetLength;
        private readonly string _sourceSequence;

        public GameplaySetFinishStateService(int targetLength, string sourceSequence)
        {
            State = GameFinishState.Running;

            _targetLength = targetLength;
            _sourceSequence = sourceSequence;
        }

        public GameFinishState State { get; private set; }

        public void SetStateByEquality(string inputSymbols)
        {
            if (inputSymbols.Length > _targetLength)
                State = GameFinishState.Defeat;

            if (inputSymbols.Length < _targetLength)
                return;

            if (State == GameFinishState.Running)
            {
                if (InputSequenceService.IsSame(inputSymbols, _sourceSequence))
                    State = GameFinishState.Win;
                else
                    State = GameFinishState.Defeat;
            }
        }
    }
}
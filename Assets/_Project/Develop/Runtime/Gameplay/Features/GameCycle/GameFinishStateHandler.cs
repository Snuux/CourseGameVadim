using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.GameCycle
{
    public class GameFinishStateHandler
    {
        private readonly int _targetLength;
        private readonly string _sourceSequence;

        public GameFinishStateHandler(int targetLength, string sourceSequence)
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
                if (IsSame(inputSymbols, _sourceSequence))
                    Win();
                else
                    Defeat();
            }
        }

        private void Win()
        {
            State = GameFinishState.Win;
            Debug.Log("Вы победили! Нажите 'Space' для продолжения...");
        }

        private void Defeat()
        {
            State = GameFinishState.Defeat;
            Debug.Log("Вы проиграли! Нажите 'Space' для продолжения...");
        }

        private bool IsSame(string sequenceTarget, string sequenceSource)
        {
            if (sequenceSource.Length != sequenceTarget.Length || sequenceTarget.Length == 0)
                throw new InvalidOperationException("Wrong length of sequence!");

            if (sequenceSource.Length == 0)
                throw new InvalidOperationException("Not generated sequence! Please generate first");

            return sequenceTarget.Equals(sequenceSource);
        }
    }
}
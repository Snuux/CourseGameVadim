using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sequences
{
    public class InputSequenceService
    {
        public string InputSymbols { get; private set; }
        
        public void ProcessInputKeys()
        {
            if (Input.inputString.Length > 0 && Input.anyKeyDown)
            {
                foreach (char character in Input.inputString)
                    InputSymbols += character;

                Debug.Log(InputSymbols);
            }
        }

        public void Clear() => InputSymbols = "";
        
        public static bool IsSame(string sequenceTarget, string sequenceSource)
        {
            if (sequenceSource.Length != sequenceTarget.Length || sequenceTarget.Length == 0)
                throw new InvalidOperationException("Wrong length of sequence!");

            if (sequenceSource.Length == 0)
                throw new InvalidOperationException("Not generated sequence! Please generate first");

            return sequenceTarget.Equals(sequenceSource);
        }
    }
}
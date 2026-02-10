using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sequences
{
    public class InputSequenceService
    {
        public void ProcessInputKeys()
        {
            if (Input.inputString.Length > 0 && Input.anyKeyDown)
            {
                foreach (char character in Input.inputString)
                    InputSymbols += character;

                Debug.Log(InputSymbols);
            }
        }
        
        public string InputSymbols { get; private set; }

        public void Clear() => InputSymbols = "";
    }
}
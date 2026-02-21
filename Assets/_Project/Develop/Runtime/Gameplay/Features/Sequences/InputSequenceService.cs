using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sequences
{
    public class InputSequenceService
    {
        public event Action<string> InputPress;
        
        private string _inputSymbols = "";

        public string GetCurrentInput()
        {
            string oldInput = _inputSymbols;
            
            if (Input.inputString.Length > 0 && Input.anyKeyDown)
            {
                foreach (char character in Input.inputString)
                    _inputSymbols += character;
            }
            
            if (oldInput?.Equals(_inputSymbols) == false)
                InputPress?.Invoke(_inputSymbols);

            return _inputSymbols;
        }

        public void Clear() => _inputSymbols = "";
    }
}
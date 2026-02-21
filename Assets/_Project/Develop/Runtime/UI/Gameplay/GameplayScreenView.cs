using System;
using _Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        private const string SequenceTitleText = "Sequence:";
        private const string InputTitleText = "Input:";
        
        public event Action CloseGameplayScreenButtonClicked;

        [field: SerializeField] private TMP_Text _sequenceTitle;
        [field: SerializeField] private TMP_Text _sequenceString;
        [field: SerializeField] private TMP_Text _inputTitle;
        [field: SerializeField] private TMP_Text _inputString;
        
        [SerializeField] private Button _closeGameplayScreen;

        
        private void OnEnable()
        {
            _sequenceTitle.text = SequenceTitleText;
            _inputTitle.text = InputTitleText;
            
            _closeGameplayScreen.onClick.AddListener(OnCloseGameplayScreenButtonClicked);
        }

        private void OnDisable()
        {
            _closeGameplayScreen.onClick.RemoveListener(OnCloseGameplayScreenButtonClicked);
        }

        public void SetSequence(string text) => _sequenceString.text = text;
        
        public void SetInput(string text) => _inputString.text = text;

        public void OnCloseGameplayScreenButtonClicked()
        {
            CloseGameplayScreenButtonClicked?.Invoke();
        }
    }
}
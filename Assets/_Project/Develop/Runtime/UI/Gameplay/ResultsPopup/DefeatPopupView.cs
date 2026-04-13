using System;
using _Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sequence = Unity.VisualScripting.Sequence;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class DefeatPopupView : PopupViewBase
    {
        public event Action ContinueClicked;
        public event Action RestartClicked;

        [SerializeField] private TMPro.TMP_Text _title;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;

        public void SetTitle(string title) => _title.text = title;

        protected override void OnPreShow()
        {
            base.OnPreShow();

            _continueButton.onClick.AddListener(OnContinueButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            RestartClicked?.Invoke();
        }

        private void OnContinueButtonClicked()
        {
            ContinueClicked?.Invoke();
        }
    }
}
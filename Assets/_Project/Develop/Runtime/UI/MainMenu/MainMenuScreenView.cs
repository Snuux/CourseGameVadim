using System;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action StartMenuButtonClicked;
        public event Action AbilitiesPopupButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public IconTextListView StatisticsView { get; private set; }

        [SerializeField] private Button _openLevelsMenuButton;
        [SerializeField] private Button _openAbilitiesPopupButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
            _openAbilitiesPopupButton.onClick.AddListener(OnOpenAbilitiesPopupButton);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
            _openAbilitiesPopupButton.onClick.RemoveListener(OnOpenAbilitiesPopupButton);
        }

        private void OnOpenLevelsMenuButtonClicked() => StartMenuButtonClicked?.Invoke();

        private void OnOpenAbilitiesPopupButton() => AbilitiesPopupButtonClicked?.Invoke();
    }
}

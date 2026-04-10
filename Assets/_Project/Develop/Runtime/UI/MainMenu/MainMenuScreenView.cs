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

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public IconTextListView StatisticsView { get; private set; }

        [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
        }

        private void OnOpenLevelsMenuButtonClicked() => StartMenuButtonClicked?.Invoke();
    }
}

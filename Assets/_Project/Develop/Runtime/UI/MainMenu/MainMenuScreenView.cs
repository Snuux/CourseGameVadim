using System;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;
        public event Action SaveButtonClicked;
        public event Action LoadButtonClicked;
        public event Action ClearButtonClicked;
        
        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        
        [SerializeField] private Button _openLevelsMenu;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _clearSaveButton;

        private void OnEnable()
        {
            _openLevelsMenu.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
            _saveButton.onClick.AddListener(OnSaveButtonClicked);
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
            _clearSaveButton.onClick.AddListener(OnClearSaveClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenu.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
            _saveButton.onClick.RemoveListener(OnSaveButtonClicked);
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
            _clearSaveButton.onClick.RemoveListener(OnClearSaveClicked);
        }

        private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();
        
        private void OnSaveButtonClicked() => SaveButtonClicked?.Invoke();
        
        private void OnLoadButtonClicked() => LoadButtonClicked?.Invoke();
        
        private void OnClearSaveClicked() => ClearButtonClicked?.Invoke();
    }
}
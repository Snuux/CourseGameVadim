using System;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelTileView : MonoBehaviour, IShowableView
    {
        public event Action Clicked;
        
        [SerializeField] private TMP_Text _symbols;
        [SerializeField] private TMP_Text _length;
        [SerializeField] private TMP_Text _winReward;
        [SerializeField] private TMP_Text _defeatPenalty;
        
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetSymbols(string symbols) => _symbols.text = "Symbols: " + symbols;
        
        public void SetLength(int length) => _length.text = "Length: " + length.ToString();
        
        public void SetWinReward(CurrencyTypes currencyTypes, int value) =>
            _winReward.text = "Win Reward: " + value + " " + currencyTypes;
        
        public void SetDefeatPenalty(CurrencyTypes currencyTypes, int value) =>
            _winReward.text = "Defeat Penalty: " + value + " " + currencyTypes;

        public Tween Show()
        {
            transform.DOKill();

            return transform
                .DOScale(1, 0.1f)
                .From(0)
                .SetUpdate(true)
                .Play();
        }

        public Tween Hide()
        {
            transform.DOKill();

            return DOTween.Sequence();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void OnClick() => Clicked?.Invoke();
    }
}

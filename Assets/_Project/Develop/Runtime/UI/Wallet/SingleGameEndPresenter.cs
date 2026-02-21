using System;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.UI.CommonViews;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.UI.Wallet
{
    public class SingleGameEndPresenter : IPresenter
    {
        //Logic 
        private readonly IReadOnlyVariable<int> _gameEnd;
        private readonly GameEndTypes _gameEndType;
        private readonly GameEndIconsConfig _gameEndIconsConfig;
        
        //View
        private readonly IconTextView _view;

        private IDisposable _disposable;
        
        public SingleGameEndPresenter(
            IReadOnlyVariable<int> gameEnd,
            GameEndTypes gameEndType,
            GameEndIconsConfig gameEndIconsConfig,
            IconTextView view)
        {
            _gameEnd = gameEnd;
            _gameEndType = gameEndType;
            _gameEndIconsConfig = gameEndIconsConfig;
            _view = view;
        }
        
        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_gameEnd.Value);
            _view.SetIcon(_gameEndIconsConfig.GetSpriteFor(_gameEndType));

            _disposable = _gameEnd.Subscribe(OnGameEndChanged);
        }

        public void Dispose() => _disposable.Dispose();
        
        private void OnGameEndChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
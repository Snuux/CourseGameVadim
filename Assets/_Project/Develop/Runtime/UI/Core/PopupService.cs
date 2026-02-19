using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core.TestPopup;
using _Project.Develop.Runtime.UI.LevelsMenuPopup;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core
{
    public abstract class PopupService : IDisposable
    {
        protected readonly ViewsFactory ViewsFactory;

        private readonly ProjectPresentersFactory _presentersFactory;

        private readonly Dictionary<PopupPresenterBase, PopupInfo> _presenterToInfo = new();

        protected PopupService(
            ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory)
        {
            ViewsFactory = viewsFactory;
            _presentersFactory = presentersFactory;
        }

        protected abstract Transform _popupLayer { get; }

        public TestPopupPresenter OpenTestPopup(Action closedCallback = null)
        {
            TestPopupView view = ViewsFactory.Create<TestPopupView>(ViewIDs.TestPopup, _popupLayer);
            
            TestPopupPresenter popup = _presentersFactory.CreateTestPopupPresenter(view);
            
            OnPopupCreated(popup, view, closedCallback);

            return popup;
        }

        public LevelsMenuPopupPresenter OpenLevelsMenuPopupPresenter(Action closedCallback = null)
        {
            LevelsMenuPopupView levelsMenuPopupView =
                ViewsFactory.Create<LevelsMenuPopupView>(ViewIDs.LevelsMenuPopup, _popupLayer);

            LevelsMenuPopupPresenter levelsMenuPopupPresenter =
                _presentersFactory.LevelsMenuPopupPresenter(levelsMenuPopupView);
            
            OnPopupCreated(levelsMenuPopupPresenter, levelsMenuPopupView, closedCallback);

            return levelsMenuPopupPresenter;
        }

        public void ClosePopup(PopupPresenterBase popup)
        {
            popup.CloseRequest -= ClosePopup;
            
            popup.Hide(() =>
            {
                _presenterToInfo[popup].ClosedCallback?.Invoke();
                
                DisposeFor(popup);
                _presenterToInfo.Remove(popup);
            });
        }

        public void Dispose()
        {
            foreach (PopupPresenterBase popup in _presenterToInfo.Keys)
            {
                popup.CloseRequest -= ClosePopup;
                DisposeFor(popup);
            }
            
            _presenterToInfo.Clear();
        }

        protected void OnPopupCreated(PopupPresenterBase popup, PopupViewBase view, Action closedCallback = null)
        {
            _presenterToInfo.Add(popup, new PopupInfo(view, closedCallback));
            
            popup.Initialize();
            popup.Show();

            popup.CloseRequest += ClosePopup;
        }

        private void DisposeFor(PopupPresenterBase popup)
        {
            popup.Dispose();
            ViewsFactory.Release(_presenterToInfo[popup].View);
        }

        private class PopupInfo
        {
            public PopupViewBase View { get; }
            public Action ClosedCallback { get; }

            public PopupInfo(PopupViewBase view, Action closedCallback)
            {
                View = view;
                ClosedCallback = closedCallback;
            }
        }
    }
}
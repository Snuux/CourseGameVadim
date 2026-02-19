using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core
{
    public abstract class PopupPresenterBase : IPresenter
    {
        public event Action<PopupPresenterBase> CloseRequest;
        
        private  ICoroutinesPerformer _coroutinesPerformer;
        
        private Coroutine _process;

        protected PopupPresenterBase(ICoroutinesPerformer coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
        }

        protected abstract PopupViewBase PopupView { get; }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
            PopupView.CloseRequest -= OnCloseRequest; //если перешли на другую сцену..
        }

        public void Show()
        {
            KillProcess();
            
            _process = _coroutinesPerformer.StartPerform(ProcessShow());
        }

        public void Hide(Action callback = null)
        {
            KillProcess();
            
            _process = _coroutinesPerformer.StartPerform(ProcessHide(callback));
        }


        protected virtual void OnPreShow()
        {
            PopupView.CloseRequest += OnCloseRequest;
        }

        protected virtual void OnPostShow()
        {
        }

        protected virtual void OnPreHide()
        {
            PopupView.CloseRequest -= OnCloseRequest;
        }

        protected virtual void OnPostHide()
        {
        }

        private IEnumerator ProcessShow()
        {
            OnPreShow();

            yield return PopupView.Show().WaitForCompletion();

            OnPostShow();
        }

        private IEnumerator ProcessHide(Action callback)
        {
            OnPreHide();

            yield return PopupView.Hide().WaitForCompletion();

            OnPostHide();

            callback?.Invoke(); //нужен для того чтобы исполнить код после закрытия
        }
        
        //protected чтобы дочерние классы могли дергать метод и событие срабатывало
        protected void OnCloseRequest() => CloseRequest?.Invoke(this);

        private void KillProcess()
        {
            if (_process != null)
                _coroutinesPerformer.StopPerform(_process);
        }
    }
}
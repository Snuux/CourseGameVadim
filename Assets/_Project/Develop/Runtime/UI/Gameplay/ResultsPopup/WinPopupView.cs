using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Gameplay.ResultsPopup
{
    public class WinPopupView : PopupViewBase
    {
        public event Action ConinueClicked;

        [SerializeField] private TMPro.TMP_Text _title;
        [SerializeField] private List<Transform> _stars;

        public void SetTitle(string title) => _title.text = title;

        public void OnContinueClicked() => ConinueClicked?.Invoke();

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            foreach (var star in _stars)
            {
                animation
                    .Append(star.DOScale(1, 0.3f).SetEase(Ease.OutBack).From(0))
                    .Join(star.DOLocalRotate(Vector3.forward * 360, .3f, RotateMode.LocalAxisAdd)
                        .SetEase(Ease.OutCubic)
                        .From(Vector3.zero));
                animation.AppendInterval(.1f);
            }
        }
    }
}
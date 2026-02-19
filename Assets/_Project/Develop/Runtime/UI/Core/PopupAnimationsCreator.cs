using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Core
{
    public class PopupAnimationsCreator
    {
        public static Sequence CreateShowAnimation(
            CanvasGroup body,
            Image anticlicker,
            PopupAnimationTypes animationTypes,
            float anticlickerMaxAlpha)
        {
            switch (animationTypes)
            {
                case PopupAnimationTypes.None:
                    return DOTween.Sequence();
                case PopupAnimationTypes.Expand:
                    return DOTween.Sequence()
                        .Append(
                            anticlicker
                                .DOFade(anticlickerMaxAlpha, .2f)
                                .From(0))
                        .Join(body.transform
                            .DOScale(1, .5f)
                            .From(0)
                            .SetEase(Ease.OutBack));
                default:
                    throw new ArgumentException(nameof(animationTypes));
            }
        }

        public static Sequence CreateHideAnimation(
            CanvasGroup body,
            Image anticlicker,
            PopupAnimationTypes animationTypes,
            float anticlickerMaxAlpha)
        {
            return DOTween.Sequence();
        }
    }
}
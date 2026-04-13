using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Common
{
    public class CommonAnimationsCreator
    {
        public static Sequence CreateBeforeExplosionAnimation(
            Transform transform,
            float maxScale,
            float duration)
        {
            if (transform.localScale.x > maxScale ||
                transform.localScale.y > maxScale ||
                transform.localScale.z > maxScale)
                throw new ArgumentException($"transform scale : {transform.localScale} < maxScale: {maxScale}");

            return DOTween.Sequence()
                .Join(transform
                    .DOScale(maxScale, duration)
                    .From(transform.localScale)
                    .SetEase(Ease.OutBack));
        }
    }
}
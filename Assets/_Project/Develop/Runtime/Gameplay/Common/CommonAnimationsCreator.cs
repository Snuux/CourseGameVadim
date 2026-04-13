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
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            if (maxScale <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxScale));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(duration));
            
            if (transform.localScale.x > maxScale ||
                transform.localScale.y > maxScale ||
                transform.localScale.z > maxScale)
                throw new ArgumentException($"transform scale : {transform.localScale} > maxScale: {maxScale}");
            
            const float preExplosionDurationPart = 0.8f;

            float preExplosionDuration = duration * preExplosionDurationPart;
            float finalExplosionDuration = duration - preExplosionDuration;

            return DOTween.Sequence()
                .Append(transform
                    .DOScale(maxScale, preExplosionDuration)
                    .SetEase(Ease.InExpo))
                .Append(transform
                    .DOScale(Vector3.zero, finalExplosionDuration)
                    .SetEase(Ease.InExpo));
        }
    }
}

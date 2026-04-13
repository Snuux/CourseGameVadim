using _Project.Develop.Runtime.Gameplay.Common;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using DG.Tweening;

namespace _Project.Develop.Runtime.Gameplay.Features.LifeCycle
{
    public class BreathView : EntityView
    {
        private const float ExplosionMaxScale = 1.3f;
        private const float ExplosionDuration = .5f;

        private Sequence _breathAnimation;

        protected override void OnEntityStartedWork(Entity entity)
        {
            _breathAnimation = CommonAnimationsCreator.CreateBreathAnimation(
                entity.Transform,
                ExplosionMaxScale,
                ExplosionDuration);

            _breathAnimation.Play();
        }

        public override void Cleanup(Entity entity)
        {
            base.Cleanup(entity);

            _breathAnimation?.Kill();
            _breathAnimation = null;
        }
    }
}
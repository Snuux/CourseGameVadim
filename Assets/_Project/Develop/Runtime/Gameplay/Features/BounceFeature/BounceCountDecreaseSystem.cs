using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.BounceFeature
{
    public class BounceCountDecreaseSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<int> _boundCount;
        private ReactiveEvent<RaycastHit> _bounceEvent;

        private IDisposable _bounceDisposable;

        public void OnInit(Entity entity)
        {
            _bounceEvent = entity.BounceEvent;
            _boundCount = entity.BounceCount;

            _bounceDisposable = _bounceEvent.Subscribe(OnBounceHit);
        }

        private void OnBounceHit(RaycastHit hit)
        {
            _boundCount.Value--;
        }

        public void OnDispose()
        {
            _bounceDisposable?.Dispose();
        }
    }
}
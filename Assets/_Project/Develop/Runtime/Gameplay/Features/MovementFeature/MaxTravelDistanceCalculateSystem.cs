using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MaxTravelDistanceCalculateSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _currentTravelDistance;
        
        private Vector3 _startPosition;
        private Transform _entityTransform;

        public void OnInit(Entity entity)
        {
            _startPosition = entity.Transform.position;
            
            _currentTravelDistance = entity.CurrentTravelDistance;
            _entityTransform = entity.Transform;
        }

        public void OnUpdate(float deltaTime)
        {
            _currentTravelDistance.Value = Vector3.Distance(_startPosition, _entityTransform.position);
        }
    }
}
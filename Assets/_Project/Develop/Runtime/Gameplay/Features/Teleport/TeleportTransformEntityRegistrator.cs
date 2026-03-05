using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportTransformEntityRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private Transform _transform;
        
        public override void Register(Entity entity)
        {
            entity.AddTeleportSourceTransform(_transform);
        }
    }
}
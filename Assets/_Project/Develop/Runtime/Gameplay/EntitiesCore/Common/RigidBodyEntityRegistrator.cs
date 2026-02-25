using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Common
{
    public class RigidBodyEntityRegistrator : MonoEntityRegistrator
    {
        public override void Register(Entity entity)
        {
            entity.AddRigidbody(GetComponent<Rigidbody>());
        }
    }
}
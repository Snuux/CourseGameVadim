using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore.Common
{
    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }
    
    public class CharacterControllerComponent : IEntityComponent
    {
        public CharacterController Value;
    }
}
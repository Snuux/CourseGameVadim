using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportSourceTransform : IEntityComponent
    {
        public Transform Value;
    }
    
    public class TeleportTargetPosition : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }
    
    public class TeleportRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanStartTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class CalculateTeleportTargetRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }
    
    public class DoTeleportInTargetPositionRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}
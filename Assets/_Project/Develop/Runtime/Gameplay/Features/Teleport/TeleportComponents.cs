using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Teleport
{
    public class TeleportTargetPosition : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }
    
    public class TeleportRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportCostEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanStartTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportRequested : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
    
    public class TeleportInProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class TeleportCompleted : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
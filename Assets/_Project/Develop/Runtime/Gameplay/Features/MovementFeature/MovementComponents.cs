using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MoveDirection : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class MoveSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class Rotation : IEntityComponent
    {
        public ReactiveVariable<Quaternion> Value;
    }

    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class Position : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }
}
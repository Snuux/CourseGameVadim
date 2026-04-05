using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        Vector3 Direction { get; }
        Vector3 MouseWorldPosition { get; }
        bool LeftMouseButtonDown { get; }
        bool RightMouseButtonDown { get; }
    }
}

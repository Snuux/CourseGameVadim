using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        public bool IsEnabled { get; set; } = true;

        public Vector3 Direction
        {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;

                return new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
            }
        }

        public bool MouseButtonDown => Input.GetMouseButtonDown(0);

        public Vector2 MousePosition
        {
            get
            {
                if (IsEnabled == false)
                    return Vector2.zero;

                return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        public Vector3 MousePositionOnZeroPlane
        {
            get
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(MousePosition);
                
                if (plane.Raycast(ray, out float dist))
                    return ray.GetPoint(dist);
                
                return Vector3.zero;
            }
        }
    }
}
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class InputMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private const float SmallFloat = 0.0001f;
        
        private Entity _entity;
        private Camera _camera;
        
        public void OnInit(Entity entity)
        {
            _entity = entity;
            _camera = Camera.main;
        }

        public void OnUpdate(float deltaTime)
        {
            MoveEntity(_entity);
            RotateRigidbodyEntity(_entity);
        }

        private void MoveEntity(Entity entity)
        {
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            entity.MoveDirection.Value = input;
        }

        private void RotateRigidbodyEntity(Entity entity)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero); // плоскость в нуле
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);

                Vector3 dir = hitPoint - entity.Position.Value;
                dir.y = 0f;

                if (dir.sqrMagnitude > SmallFloat) 
                    entity.Rotation.Value = Quaternion.LookRotation(dir, Vector3.up);
            }
        }
    }
}
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Common
{
    public class ViewContainerRegistrator : MonoEntityRegistrator
    {
        [SerializeField] Transform _viewContainer;
        
        public override void Register(Entity entity)
        {
            entity.AddViewContainer(_viewContainer);
        }
    }
}
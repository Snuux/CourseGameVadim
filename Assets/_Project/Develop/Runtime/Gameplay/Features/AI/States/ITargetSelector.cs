using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> target);
    }
}
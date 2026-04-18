using System.Collections.Generic;

namespace _Project.Develop.Runtime.Gameplay.Features.StatFeature
{
    public interface IStatsEffect
    {
        void ApplyTo(Dictionary<StatTypes, float> stats);
    }
}
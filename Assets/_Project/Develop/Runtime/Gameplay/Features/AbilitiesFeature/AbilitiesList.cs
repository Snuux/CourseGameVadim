using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;

namespace _Project.Develop.Runtime.Configs.Gameplay.Abilities
{
    public class AbilitiesList
    {
        public event Action<Ability> Added;

        private readonly List<Ability> _elements = new();

        public IReadOnlyList<Ability> Elements => _elements;

        public virtual void Add(Ability element)
        {
            _elements.Add(element);
            Added?.Invoke(element);
        }
    }
}
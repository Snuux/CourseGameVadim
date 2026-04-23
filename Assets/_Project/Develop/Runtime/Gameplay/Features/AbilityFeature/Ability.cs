using System;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature
{
    public abstract class Ability
    {
        private ReactiveVariable<int> _currentLevel;

        protected Ability(string id, int currentLevel, int maxLevel)
        {
            ID = id;
            MaxLevel = maxLevel;
            _currentLevel = new ReactiveVariable<int>(currentLevel);
        }


        public string ID { get; }
        public int MaxLevel { get; }
        
        public IReadOnlyVariable<int> CurrentLevel => _currentLevel;

        public void AddLevel(int level)
        {
            int temp = _currentLevel.Value + level;
            if (temp > MaxLevel)
                throw new ArgumentOutOfRangeException($"Level {level} is greater than maximum {MaxLevel}");
            
            _currentLevel.Value = temp;
        }

        public abstract void Activate();
    }
}
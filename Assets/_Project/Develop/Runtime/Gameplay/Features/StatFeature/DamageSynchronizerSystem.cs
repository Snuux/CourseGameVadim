using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.StatFeature
{
    public class DamageSynchronizerSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _damage;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _damage = entity.InstantAttackDamage;
            _modifiedStats = entity.ModifiedStats;
        }

        public void OnUpdate(float deltaTime)
        {
            float tempValue = _modifiedStats[StatTypes.Damage];

            if (tempValue < 0)
                tempValue = 0;

            _damage.Value = tempValue;
        }
    }
}
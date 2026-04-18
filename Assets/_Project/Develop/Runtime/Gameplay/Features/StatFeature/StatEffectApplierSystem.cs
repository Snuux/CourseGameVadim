using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;

namespace _Project.Develop.Runtime.Gameplay.Features.StatFeature
{
    public class StatEffectApplierSystem : IInitializableSystem, IDisposableSystem
    {
        private StatsEffectsList _statsEffects;
        private Dictionary<StatTypes, float> _baseStats;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _baseStats = entity.BaseStats;
            _modifiedStats = entity.ModifiedStats;
            _statsEffects = entity.StatsEffects;

            _statsEffects.Added += OnStatsEffectsAdded;
            _statsEffects.Removed += OnStatsEffectsRemoved;

            RecalculateStats();
        }

        public void OnDispose()
        {
            _statsEffects.Added -= OnStatsEffectsAdded;
            _statsEffects.Removed -= OnStatsEffectsRemoved;
        }

        private void OnStatsEffectsAdded(IStatsEffect effect) => RecalculateStats();

        private void OnStatsEffectsRemoved(IStatsEffect effect) => RecalculateStats();

        private void RecalculateStats()
        {
            foreach (StatTypes stat in _baseStats.Keys)
                _modifiedStats[stat] = _baseStats[stat];

            foreach (IStatsEffect effect in _statsEffects.Elements)
                effect.ApplyTo(_modifiedStats);
        }
    }
}
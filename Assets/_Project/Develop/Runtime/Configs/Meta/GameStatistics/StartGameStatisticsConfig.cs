using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.GameStatistics
{
    [CreateAssetMenu(menuName = "Configs/Meta/GameStatistic", fileName = "GameStatisticConfig")]
    public class StartGameStatisticsConfig : ScriptableObject
    {
        [SerializeField] private List<GameStatisticsConfig> _values;

        public int GetValueFor(GameStatisticsTypes statisticsTypes)
            => _values.First(config => config.Type == statisticsTypes).Value;

        [Serializable]
        private class GameStatisticsConfig
        {
            [field: SerializeField] public GameStatisticsTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
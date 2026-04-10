using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Statistics
{
    [CreateAssetMenu(menuName = "Configs/Meta/Statistics/StartStatisticsConfig", fileName = "StartStatisticsConfig")]
    public class StartStatisticsConfig : ScriptableObject
    {
        [SerializeField] private List<Record> _values;

        public int GetValueFor(StatisticType statisticType)
            => _values.First(record => record.Type == statisticType).Value;
        
        [Serializable]
        private class Record
        {
            [field: SerializeField] public StatisticType Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
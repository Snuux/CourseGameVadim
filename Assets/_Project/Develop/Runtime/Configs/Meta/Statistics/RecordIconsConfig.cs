using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Statistics
{
    [CreateAssetMenu(menuName = "Configs/Meta/Statistics/NewRecordIconsConfig", fileName = "RecordIconsConfig")]
    public class RecordIconsConfig : ScriptableObject
    {
        [SerializeField] private List<StatisticsConfig> _configs;

        public Sprite GetSpriteFor(StatisticType statisticType)
            => _configs.First(config => config.Type == statisticType).Sprite;

        [Serializable]
        private class StatisticsConfig
        {
            [field: SerializeField] public StatisticType Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
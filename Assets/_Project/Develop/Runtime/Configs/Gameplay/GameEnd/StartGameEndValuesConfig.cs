using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Gameplay.GameEnd
{
    [CreateAssetMenu(menuName = "Configs/Meta/StartGameEndValuesConfig", fileName = "StartGameEndValuesConfig")]
    public class StartGameEndValuesConfig : ScriptableObject
    {
        [SerializeField] private List<GameStatisticsConfig> _values;

        public int GetValueFor(GameEndTypes endTypes)
            => _values.First(config => config.Type == endTypes).Value;

        [Serializable]
        private class GameStatisticsConfig
        {
            [field: SerializeField] public GameEndTypes Type { get; private set; }
            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
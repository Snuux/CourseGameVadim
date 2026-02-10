using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Statistics
{
    public class GameStatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<GameStatisticsTypes, ReactiveVariable<int>> _gameStatistics;

        public GameStatisticsService(
            Dictionary<GameStatisticsTypes, ReactiveVariable<int>> gameStatistics,
            PlayerDataProvider playerDataProvider)
        {
            _gameStatistics = new Dictionary<GameStatisticsTypes, ReactiveVariable<int>>(gameStatistics);
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<GameStatisticsTypes> AllGameStatistics => _gameStatistics.Keys.ToList();

        public IReadOnlyVariable<int> GetGameStatistics(GameStatisticsTypes type) => _gameStatistics[type];

        public void Add(GameStatisticsTypes type, int amount = 1)
        {
            _gameStatistics[type].Value += amount;
        }

        public void Sub(GameStatisticsTypes type, int amount = 1)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _gameStatistics[type].Value -= amount;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<GameStatisticsTypes, int> gameStatistics in data.GameStatisticsData)
            {
                if (_gameStatistics.ContainsKey(gameStatistics.Key))
                    _gameStatistics[gameStatistics.Key].Value = gameStatistics.Value;
                else
                    _gameStatistics.Add(gameStatistics.Key, new ReactiveVariable<int>(gameStatistics.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<GameStatisticsTypes, ReactiveVariable<int>> gameStatistics in _gameStatistics)
            {
                if (data.GameStatisticsData.ContainsKey(gameStatistics.Key))
                    data.GameStatisticsData[gameStatistics.Key] = gameStatistics.Value.Value;
                else
                    data.GameStatisticsData.Add(gameStatistics.Key, gameStatistics.Value.Value);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.GameStatistics;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Statistics
{
    public class GameStatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<GameStatisticsTypes, ReactiveVariable<int>> _gameStatistics;
        private readonly ConfigsProviderService _configsProviderService;

        public GameStatisticsService(
            Dictionary<GameStatisticsTypes, ReactiveVariable<int>> gameStatistics,
            ConfigsProviderService configsProviderService,
            PlayerDataProvider playerDataProvider)
        {
            _gameStatistics = new Dictionary<GameStatisticsTypes, ReactiveVariable<int>>(gameStatistics);
            _configsProviderService = configsProviderService;
            
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<GameStatisticsTypes> AllGameStatistics => _gameStatistics.Keys.ToList();

        public IReadOnlyVariable<int> GetGameStatistics(GameStatisticsTypes type) => _gameStatistics[type];

        public void Add(GameStatisticsTypes type, int amount = 1)
        {
            _gameStatistics[type].Value += amount;
        }

        public void Reset(GameStatisticsTypes type)
        {
            _gameStatistics[type].Value = _configsProviderService
                .GetConfig<StartGameStatisticsConfig>().GetValueFor(type);
        }
        
        public void ResetAll()
        {
            foreach (GameStatisticsTypes gameStatisticsTypes in Enum.GetValues(typeof(GameStatisticsTypes)))
                _gameStatistics[gameStatisticsTypes].Value 
                    = _configsProviderService.GetConfig<StartGameStatisticsConfig>().GetValueFor(gameStatisticsTypes);
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
        
        public string AsString()
        {
            string result = "";
            foreach (GameStatisticsTypes gameStatisticsTypes in AllGameStatistics)
                result += $"{gameStatisticsTypes.ToString()}: {GetGameStatistics(gameStatisticsTypes).Value} ";

            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Statistics
{
    public class StatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<StatisticType, ReactiveVariable<int>> _records;

        public StatisticsService(
            Dictionary<StatisticType, ReactiveVariable<int>> statistics, 
            PlayerDataProvider playerDataProvider)
        {
            _records = new Dictionary<StatisticType, ReactiveVariable<int>>(statistics);
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<StatisticType> AvailableRecords => _records.Keys.ToList();

        public IReadOnlyVariable<int> GetRecord(StatisticType type) => _records[type];

        public void Add(StatisticType type, int amount = 1)
        {
            _records[type].Value += amount;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<StatisticType, int> record in data.StatisticsData)
            {
                if (_records.ContainsKey(record.Key))
                    _records[record.Key].Value = record.Value;
                else
                    _records.Add(record.Key, new ReactiveVariable<int>(record.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<StatisticType, ReactiveVariable<int>> stat in _records)
            {
                if (data.StatisticsData.ContainsKey(stat.Key))
                    data.StatisticsData[stat.Key] = stat.Value.Value;
                else
                    data.StatisticsData.Add(stat.Key, stat.Value.Value);
            }
        }
    }
}
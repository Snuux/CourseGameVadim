using System.Collections.Generic;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;

namespace _Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyType, int> WalletData;
        public Dictionary<StatisticType, int> StatisticsData;
        public Dictionary<string, bool> AbilitiesData;
    }
}

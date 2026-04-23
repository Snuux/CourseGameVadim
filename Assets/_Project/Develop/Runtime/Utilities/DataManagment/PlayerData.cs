using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.Features.StatFeature;
using _Project.Develop.Runtime.Meta.Features.Wallet;

namespace _Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        public List<int> CompletedLevels;
        public Dictionary<StatTypes, int> StatsUpgradeLevel;
    }
}

using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Configs.Meta.Wallet;

namespace _Project.Develop.Runtime.Utilities.DataManagment
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> CurrenciesData;
        public Dictionary<GameEndTypes, int> GameEndData;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.GameEnd;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Wallet
{
    public class LevelOutcomeService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<GameEndTypes, ReactiveVariable<int>> _gameEnd;
        private readonly ConfigsProviderService _configsProviderService;

        public LevelOutcomeService(
            Dictionary<GameEndTypes, ReactiveVariable<int>> gameEnd,
            ConfigsProviderService configsProviderService,
            PlayerDataProvider playerDataProvider)
        {
            _gameEnd = new Dictionary<GameEndTypes, ReactiveVariable<int>>(gameEnd);
            _configsProviderService = configsProviderService;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<GameEndTypes> AllGameEnds => _gameEnd.Keys.ToList();

        public IReadOnlyVariable<int> GameEnd(GameEndTypes type) => _gameEnd[type];

        public void Add(GameEndTypes type, int amount = 1)
        {
            _gameEnd[type].Value += amount;
        }

        public void ResetAll()
        {
            foreach (GameEndTypes gameEnd in Enum.GetValues(typeof(GameEndTypes)))
                _gameEnd[gameEnd].Value
                    = _configsProviderService.GetConfig<StartGameEndValuesConfig>().GetValueFor(gameEnd);
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<GameEndTypes, int> gameEnd in data.GameEndData)
            {
                if (_gameEnd.ContainsKey(gameEnd.Key))
                    _gameEnd[gameEnd.Key].Value = gameEnd.Value;
                else
                    _gameEnd.Add(gameEnd.Key, new ReactiveVariable<int>(gameEnd.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<GameEndTypes, ReactiveVariable<int>> gameEnd in _gameEnd)
            {
                if (data.GameEndData.ContainsKey(gameEnd.Key))
                    data.GameEndData[gameEnd.Key] = gameEnd.Value.Value;
                else
                    data.GameEndData.Add(gameEnd.Key, gameEnd.Value.Value);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Utilities.DataManagment;
using _Project.Develop.Runtime.Utilities.DataManagment.DataProviders;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Meta.Features.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies;

        public WalletService(
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies,
            PlayerDataProvider playerDataProvider)
        {
            _currencies = new Dictionary<CurrencyTypes, ReactiveVariable<int>>(currencies);
            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<CurrencyTypes> AvailableCurrencies => _currencies.Keys.ToList();

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes type) => _currencies[type];

        public void Add(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value += amount;
        }

        public bool Enough(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            return _currencies[type].Value >= amount;
        }

        public void Spend(CurrencyTypes type, int amount)
        {
            if (Enough(type, amount) == false)
                throw new InvalidOperationException("Not enough: " + type.ToString());

            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            _currencies[type].Value -= amount;
        }

        public void Sub(CurrencyTypes type, int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (Enough(type, amount) == false)
                _currencies[type].Value = 0;
            else
                _currencies[type].Value -= amount;
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in data.CurrenciesData)
            {
                if (_currencies.ContainsKey(currency.Key))
                    _currencies[currency.Key].Value = currency.Value;
                else
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currency in _currencies)
            {
                if (data.CurrenciesData.ContainsKey(currency.Key))
                    data.CurrenciesData[currency.Key] = currency.Value.Value;
                else
                    data.CurrenciesData.Add(currency.Key, currency.Value.Value);
            }
        }

        public string AsString()
        {
            string result = "";

            foreach (CurrencyTypes currencyTypes in AvailableCurrencies)
                result += $"{currencyTypes.ToString()}: {GetCurrency(currencyTypes).Value} ";

            return result;
        }
    }
}
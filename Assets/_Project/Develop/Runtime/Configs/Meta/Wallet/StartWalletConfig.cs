using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<Currency> _values;

        public int GetValueFor(CurrencyType currencyType)
            => _values.First(currency => currency.Type == currencyType).Value;
    }

    [Serializable]
    public class Currency
    {
        [field: SerializeField] public CurrencyType Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
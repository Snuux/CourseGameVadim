using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Meta/StartCurrenciesConfig", fileName = "StartCurrenciesConfig")]
    public class StartCurrenciesConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        public int GetValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).Value;
    }
}

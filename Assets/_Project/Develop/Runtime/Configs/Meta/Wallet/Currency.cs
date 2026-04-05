using System;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Configs.Meta.Wallet
{
    [Serializable]
    public class Currency
    {
        [field: SerializeField] public CurrencyTypes Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
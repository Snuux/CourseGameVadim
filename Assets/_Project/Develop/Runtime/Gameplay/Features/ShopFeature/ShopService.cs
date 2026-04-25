using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.ShopFeature
{
    public class ShopService : IDisposable
    {
        private readonly TowerHolderService _towerHolderService;
        private readonly WalletService _walletService;
        private readonly AllyFactory _allyFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ShopConfig _shopConfig;

        private readonly Dictionary<Entity, IDisposable> _spawnedItemsToRemoveReason = new();
        private IDisposable _towerRegisteredDisposable;

        public ShopService(
            TowerHolderService towerHolderService,
            AllyFactory allyFactory,
            WalletService walletService,
            EntitiesLifeContext entitiesLifeContext,
            ShopConfig shopConfig)
        {
            _towerHolderService = towerHolderService;
            _allyFactory = allyFactory;
            _walletService = walletService;
            _entitiesLifeContext = entitiesLifeContext;
            _shopConfig = shopConfig;
        }

        public bool Buy(ShopItemTypes itemType, Vector3 position)
        {
            (CurrencyType currencyType, int price) itemPrice = _shopConfig.GetPriceFor(itemType);

            if (_walletService.Enough(itemPrice.currencyType, itemPrice.price) == false)
                return false;

            _walletService.Spend(itemPrice.currencyType, itemPrice.price);
            Entity spawnedItem = SpawnItem(itemType, position);
            RegisterSpawnedItem(spawnedItem);

            return true;
        }

        public void CleanupSpawnedItems()
        {
            foreach (KeyValuePair<Entity, IDisposable> item in _spawnedItemsToRemoveReason)
            {
                item.Value.Dispose();
                _entitiesLifeContext.Release(item.Key);
            }

            _spawnedItemsToRemoveReason.Clear();
        }

        public void Dispose()
        {
            _towerRegisteredDisposable?.Dispose();
            _towerRegisteredDisposable = null;

            foreach (IDisposable removeReason in _spawnedItemsToRemoveReason.Values)
                removeReason.Dispose();

            _spawnedItemsToRemoveReason.Clear();
        }

        private Entity SpawnItem(ShopItemTypes itemType, Vector3 position)
        {
            switch (itemType)
            {
                case ShopItemTypes.Mine:
                    return _allyFactory.CreateMine(position);
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemType), itemType, "Unsupported shop item type");
            }
        }

        private void RegisterSpawnedItem(Entity spawnedItem)
        {
            IDisposable removeReason = spawnedItem.IsDead.Subscribe((_, isDead) =>
            {
                if (isDead == false)
                    return;

                if (_spawnedItemsToRemoveReason.TryGetValue(spawnedItem, out IDisposable disposable) == false)
                    return;

                disposable.Dispose();
                _spawnedItemsToRemoveReason.Remove(spawnedItem);
            });

            _spawnedItemsToRemoveReason.Add(spawnedItem, removeReason);
        }
    }
}

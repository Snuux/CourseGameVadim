using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.ShopFeature
{
    public class ShopService : IDisposable
    {
        public event Action Bought;
        public event Action Spawned;
        
        private readonly WalletService _walletService;
        private readonly AllyFactory _allyFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly ShopItemsConfig _shopItemsConfig;

        private readonly Dictionary<Entity, IDisposable> _spawnedItemsToRemoveReason = new();
        private IDisposable _towerRegisteredDisposable;

        private ShopItemTypes _shopItemTypeToSpawn;

        public ShopService(
            AllyFactory allyFactory,
            WalletService walletService,
            EntitiesLifeContext entitiesLifeContext,
            ShopItemsConfig shopItemsConfig)
        {
            _allyFactory = allyFactory;
            _walletService = walletService;
            _entitiesLifeContext = entitiesLifeContext;
            _shopItemsConfig = shopItemsConfig;
        }

        public IReadOnlyList<ShopItemTypes> AvailableShopItemTypes =>
            _shopItemsConfig.Configs.Select(t => t.ItemType).ToList();
        
        public IReadOnlyList<ShopItemConfig> AvailableShopItemsConfigs => _shopItemsConfig.Configs;
        
        public bool CanSpawn { get; private set; }

        public bool TryToPurchase(ShopItemTypes shopItemType)
        {
            ShopItemConfig shopItemConfig = _shopItemsConfig.GetPriceFor(shopItemType);
            
            if (_walletService.Enough(shopItemConfig.CurrencyType, shopItemConfig.Price) == false)
                return false;

            Purchase(shopItemType);
            
            return true;
        }
        
        public void Purchase(ShopItemTypes shopItemType)
        {
            ShopItemConfig shopItemConfig = _shopItemsConfig.GetPriceFor(shopItemType);
            _walletService.Spend(shopItemConfig.CurrencyType, shopItemConfig.Price);
            _shopItemTypeToSpawn = shopItemType;

            Bought?.Invoke();
            CanSpawn = true;
        }
        
        public void DeclinePurchase()
        {
            ShopItemConfig shopItemConfig = _shopItemsConfig.GetPriceFor(_shopItemTypeToSpawn);
            _walletService.Add(shopItemConfig.CurrencyType, shopItemConfig.Price);
            CanSpawn = false;
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

        public Entity SpawnBoughtItem(Vector3 position)
        {
            Entity entityToSpawn;

            switch (_shopItemTypeToSpawn)
            {
                case ShopItemTypes.Mine:
                    entityToSpawn = _allyFactory.CreateMine(position);
                    break;
                case ShopItemTypes.Turret:
                    entityToSpawn = _allyFactory.CreateTurret(position);
                    break;
                case ShopItemTypes.Puddle:
                    entityToSpawn = _allyFactory.CreatePuddle(position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_shopItemTypeToSpawn), _shopItemTypeToSpawn,
                        "Unsupported shop item type");
            }

            RegisterSpawnedItem(entityToSpawn);

            Spawned?.Invoke();
            CanSpawn = false;

            return entityToSpawn;
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
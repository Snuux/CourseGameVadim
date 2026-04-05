using System;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class CursorShopState : State, IUpdatableState
    {
        private readonly TowerHolderService _towerHolderService;
        private readonly WalletService _walletService;
        private readonly AllyFactory _allyFactory;
        private readonly IInputService _inputService;
        private readonly ShopConfig _shopConfig;
        private Entity _entityParent;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;
        
        private IDisposable _towerRegisteredDisposable;

        public CursorShopState(
            TowerHolderService towerHolderService, 
            AllyFactory entitiesFactory, 
            IInputService inputService, 
            WalletService walletService, 
            ShopConfig shopConfig)
        {
            _towerHolderService = towerHolderService;
            _allyFactory = entitiesFactory;
            _inputService = inputService;
            _walletService = walletService;
            _shopConfig = shopConfig;

            _towerRegisteredDisposable = _towerHolderService.TowerRegistered.Subscribe(OnTowerRegistered);
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Shopping!!!");
        }

        public void Update(float deltaTime)
        {
            if (_entityParent == null)
                return;
            
            if (_inputService.LeftMouseButtonDown)
            {
                var mineItemPrice = _shopConfig.GetPriceFor(ShopItemTypes.Mine);
                
                if (_walletService.Enough(mineItemPrice.currencyType, mineItemPrice.price))
                {
                    _walletService.Spend(mineItemPrice.currencyType, mineItemPrice.price);
                    _allyFactory.CreateMine(_inputService.MouseWorldPosition, _entityParent);
                }
            }
        }

        private void OnTowerRegistered(Entity tower)
        {
            _entityParent = tower;
            _towerRegisteredDisposable.Dispose();
            _towerRegisteredDisposable = null;
        }
    }
}
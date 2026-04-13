using System;
using _Project.Develop.Runtime.Configs.Gameplay.Shop;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Ally;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class ShopState : State, IUpdatableState
    {
        private readonly TowerHolderService _towerHolderService;
        private readonly WalletService _walletService;
        private readonly AllyFactory _allyFactory;
        private readonly IInputService _inputService;
        private readonly ShopConfig _shopConfig;
        private readonly GameplayPopupService _popupService;
        private Entity _entityParent;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;
        
        private IDisposable _towerRegisteredDisposable;

        public ShopState(
            TowerHolderService towerHolderService, 
            AllyFactory entitiesFactory, 
            IInputService inputService, 
            WalletService walletService, 
            ShopConfig shopConfig, 
            GameplayPopupService popupService)
        {
            _towerHolderService = towerHolderService;
            _allyFactory = entitiesFactory;
            _inputService = inputService;
            _walletService = walletService;
            _shopConfig = shopConfig;
            _popupService = popupService;

            _towerRegisteredDisposable = _towerHolderService.TowerRegistered.Subscribe(OnTowerRegistered);
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Shopping!!!");
            
            _popupService.OpenShopPopup();
        }

        public void Update(float deltaTime)
        {
            if (_entityParent == null)
                return;
            
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            
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
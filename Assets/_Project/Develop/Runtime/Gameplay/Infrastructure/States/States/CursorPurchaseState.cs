using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature.States
{
    public class CursorPurchaseState : State, IUpdatableState
    {
        private readonly TowerHolderService _towerHolderService;
        private readonly WalletService _walletService;
        private readonly AllyFactory _allyFactory;
        private readonly IInputService _inputService;
        private Entity _entityParent;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;
        
        private IDisposable _towerRegistered;

        public CursorPurchaseState(TowerHolderService towerHolderService, AllyFactory entitiesFactory, IInputService inputService, WalletService walletService)
        {
            _towerHolderService = towerHolderService;
            _allyFactory = entitiesFactory;
            _inputService = inputService;
            _walletService = walletService;

            _towerRegistered = _towerHolderService.TowerRegistered.Subscribe(OnTowerRegistered);
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
            
            if (_inputService.LeftMouseButton)
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 100))
                {
                    _allyFactory.CreateMine(_inputService.MouseWorldPosition, _entityParent);
                }
            }
        }

        private void OnTowerRegistered(Entity tower)
        {
            _entityParent = tower;
            _towerRegistered.Dispose();
            _towerRegistered = null;
        }
    }
}
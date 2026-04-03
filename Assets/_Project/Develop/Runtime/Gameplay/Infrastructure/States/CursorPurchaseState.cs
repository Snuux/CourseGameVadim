using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature.States
{
    public class CursorPurchaseState : State, IUpdatableState
    {
        private readonly TowerHolderService _towerHolderService;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly IInputService _inputService;
        private Entity _entityParent;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;

        public CursorPurchaseState(TowerHolderService towerHolderService, EntitiesFactory entitiesFactory, IInputService inputService)
        {
            _towerHolderService = towerHolderService;
            _entitiesFactory = entitiesFactory;
            _inputService = inputService;

            _towerHolderService.TowerRegistered.Subscribe(OnTowerRegistered);
        }

        public virtual void Enter()
        {
            base.Enter();

            Debug.Log("Shopping!!!");
        }

        public void Update(float deltaTime)
        {
            if (_entityParent == null)
                return;
            
            //logic
        }

        private void OnTowerRegistered(Entity tower)
        {
            _entityParent = tower;
        }
    }
}
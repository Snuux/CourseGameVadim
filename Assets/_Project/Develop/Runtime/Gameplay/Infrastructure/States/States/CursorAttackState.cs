using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class CursorAttackState : State, IUpdatableState
    {
        private readonly TowerHolderService _towerHolderService;
        
        private readonly IInputService _inputService;
        private readonly EntitiesFactory _entitiesFactory;
        private Entity _entityParent;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;

        public CursorAttackState(TowerHolderService towerHolderService, IInputService inputService, EntitiesFactory entitiesFactory)
        {
            _inputService = inputService;
            _entitiesFactory = entitiesFactory;
            _cursorAttackRadius = new ReactiveVariable<float>(5);
            _cursorAttackDamage = new ReactiveVariable<float>(99);
            //todo add _cursorAttackRadius;

            _towerHolderService = towerHolderService;
            _towerHolderService.TowerRegistered.Subscribe(OnTowerRegistered);
        }

        public void Update(float deltaTime)
        {
            if (_entityParent == null)
                return;
            
            if (_inputService.LeftMouseButton)
            {
                _entitiesFactory.CreateAreaProjectile(_inputService.MouseWorldPosition, _cursorAttackRadius.Value,
                    _cursorAttackDamage.Value, _entityParent);
            }
        }

        private void OnTowerRegistered(Entity tower)
        {
            _entityParent = tower;
        }
    }
}
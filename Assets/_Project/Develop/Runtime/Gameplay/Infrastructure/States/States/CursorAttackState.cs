using System;
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
        private readonly IInputService _inputService;
        private readonly EntitiesFactory _entitiesFactory;
        private readonly AllyFactory _allyFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        
        private Entity _cursorAttacker;

        private ReactiveVariable<float> _cursorAttackRadius;
        private ReactiveVariable<float> _cursorAttackDamage;

        public CursorAttackState(
            IInputService inputService,
            EntitiesFactory entitiesFactory,
            AllyFactory allyFactory,
            EntitiesLifeContext entitiesLifeContext)
        {
            _inputService = inputService;
            _entitiesFactory = entitiesFactory;
            _allyFactory = allyFactory;
            _entitiesLifeContext = entitiesLifeContext;
        }
        
        public override void Enter()
        {
            base.Enter();

            _cursorAttacker = _allyFactory.CreateCursorAttacker();
            
            _cursorAttackRadius = _cursorAttacker.AttackRadius;
            _cursorAttackDamage = _cursorAttacker.AttackDamage;
            

            Debug.Log("Attacking!!!");
        }

        public void Update(float deltaTime)
        {
            if (_inputService.LeftMouseButtonDown)
            {
                _entitiesFactory.CreateAreaProjectile(_inputService.MouseWorldPosition, _cursorAttackRadius.Value,
                    _cursorAttackDamage.Value, _cursorAttacker);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _entitiesLifeContext.Release(_cursorAttacker);
        }
    }
}
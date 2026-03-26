using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        private Entity _hero;
        private Entity _ghost;
        private Entity _mage;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
        }

        public void Run()
        {
            _hero = _entitiesFactory.CreateHero(Vector3.zero);
            _hero.AddCurrentTarget();
            
            _ghost = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5);

            _mage = _entitiesFactory.CreateMage(Vector3.zero + Vector3.forward * 3);
            _mage.AddCurrentTarget();
            
            _isRunning = true;
        }

        private StateMachineBrain _heroBrain;
        private StateMachineBrain _mageBrain;
        
        private void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1)) // auto attack player behaviour
                _heroBrain = _brainsFactory.CreateMainHeroBrain(_hero, new NearestDamageableTargetSelector(_hero));
            
            if (Input.GetKeyDown(KeyCode.Alpha2)) // manual attack player behaviour
                _heroBrain = _brainsFactory.CreateMainHeroManualInputBrain(_hero);
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _heroBrain.Disable();
            
            if (Input.GetKeyDown(KeyCode.Alpha4)) // test mage random teleport
                _mageBrain = _brainsFactory.CreateRandomTeleportMageBrain(_mage); 

            if (Input.GetKeyDown(KeyCode.Alpha5)) // test mage teleport to lowest health target
                _mageBrain = _brainsFactory.CreateMageBrainLowHealthTarget(_mage, new LowestHealthTargetSelector(_mage));
            
            if (Input.GetKeyDown(KeyCode.Alpha6))
                _mageBrain.Disable();
            
            /*if (Input.GetKeyDown(KeyCode.Space))
                _entity.TakeDamageRequest.Invoke(50);

            if (Input.GetKeyDown(KeyCode.R))
                _entity.StartAttackRequest.Invoke();

            if (Input.GetKeyDown(KeyCode.I))
                _brainsFactory.CreateGhostBrain(_ghost);*/
        }
    }
}

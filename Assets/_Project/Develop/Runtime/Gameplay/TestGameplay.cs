using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Gameplay.Features.AI.States;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private AllyFactory _allyFactory;
        private EnemiesFactory _enemiesFactory;
        private BrainsFactory _brainsFactory;
        
        [SerializeField] GhostConfig _ghostConfig;
        [SerializeField] LevelConfig _levelConfig;
        
        private Entity _tower;
        private Entity _ghost;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            
            _allyFactory = _container.Resolve<AllyFactory>();
            _enemiesFactory = _container.Resolve<EnemiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
        }

        public void Run()
        {
            _tower = _allyFactory.CreateTower(Vector3.zero, _levelConfig);
            _ghost = _enemiesFactory.Create(Vector3.zero + Vector3.forward * 5, _ghostConfig);
            
            //_brainsFactory.CreateGhostBrain(_ghost);
            
            //_hero = _mainHeroFactory.Create(Vector3.zero);

            //_stage = _stagesFactory.Create(_stageConfig);
            //_stage.Completed.Subscribe(OnCompleted);
            //_stage.Start();
            
            //_mage = _entitiesFactory.CreateMage(Vector3.zero + Vector3.forward * 3);
            //_mage.AddCurrentTarget();
            
            _isRunning = true;
        }

        //private void OnCompleted()
        //{
        //    Debug.Log("Победа");
        //    _stage.Cleanup();
        //}

        //private StateMachineBrain _heroBrain;
        //private StateMachineBrain _mageBrain;
        
        private void Update()
        {
            if (_isRunning == false)
                return;
            
            if (Input.GetKeyDown(KeyCode.Alpha1)) // auto attack player behaviour
                _ghost.AttackRequested.Value = true;
            
            /*
            _stage.Update(Time.deltaTime);

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
            */
            
            //if (Input.GetKeyDown(KeyCode.Space))
            //    _entity.TakeDamageRequest.Invoke(50);

            //if (Input.GetKeyDown(KeyCode.R))
            //    _entity.StartAttackRequest.Invoke();

            //if (Input.GetKeyDown(KeyCode.I))
            //    _brainsFactory.CreateGhostBrain(_ghost);
        }
    }
}

using System;
using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Enemies;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class ClearAllEnemiesStage : IStage
    {
        private EntitiesLifeContext _entitiesLifeContext;
        private EnemiesFactory _enemiesFactory;
        private EnemiesSpawnerService _enemiesSpawnerService;
        
        private ClearAllEnemiesStageConfig _config;
        private ReactiveEvent _completed = new();
        private bool _inProcess;

        private Dictionary<Entity, IDisposable> _spawnEnemiesToRemoveReason = new();

        public ClearAllEnemiesStage(ClearAllEnemiesStageConfig config,
            EnemiesFactory enemiesFactory,
            EntitiesLifeContext entitiesLifeContext,
            EnemiesSpawnerService enemiesSpawnerService)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
            _entitiesLifeContext = entitiesLifeContext;
            _enemiesSpawnerService = enemiesSpawnerService;
        }

        public IReadOnlyEvent Completed => _completed;

        public void Start()
        {
            if (_inProcess)
                throw new InvalidOperationException("Game mode already started;");

            _inProcess = true;

            Debug.Log("Spawning Enemies");
            
            SpawnEnemies();
        }

        public void Update(float deltaTime)
        {
            if (_inProcess == false)
                return;

            if (_spawnEnemiesToRemoveReason.Count == 0)
                ProcessEnd();
        }

        public void Cleanup()
        {
            foreach (KeyValuePair<Entity, IDisposable> item in _spawnEnemiesToRemoveReason)
            {
                item.Value.Dispose();
                _entitiesLifeContext.Release(item.Key);
            }
            
            _spawnEnemiesToRemoveReason.Clear();
            _inProcess = false;
        }

        public void Dispose()
        {
            foreach (KeyValuePair<Entity, IDisposable> item in _spawnEnemiesToRemoveReason)
            {
                item.Value.Dispose();
            }
            
            _spawnEnemiesToRemoveReason.Clear();
            _inProcess = false;
        }

        private void SpawnEnemies()
        {
            foreach (EnemyItemConfig enemyItemConfig in _config.EnemyItems)
                SpawnEnemy(enemyItemConfig);
        }

        private void SpawnEnemy(EnemyItemConfig enemyItemConfig)
        {
            Entity spawnedEnemy = _enemiesSpawnerService.Spawn(enemyItemConfig);
            
            IDisposable removeReason = spawnedEnemy.IsDead.Subscribe((oldValue, isDead) =>
            {
                if (isDead)
                {
                    IDisposable disposable = _spawnEnemiesToRemoveReason[spawnedEnemy];
                    disposable.Dispose();
                    _spawnEnemiesToRemoveReason.Remove(spawnedEnemy);
                }
            });

            _spawnEnemiesToRemoveReason.Add(spawnedEnemy, removeReason);
        }

        private void ProcessEnd()
        {
            _inProcess = false;
            _completed.Invoke();
        }
    }
}
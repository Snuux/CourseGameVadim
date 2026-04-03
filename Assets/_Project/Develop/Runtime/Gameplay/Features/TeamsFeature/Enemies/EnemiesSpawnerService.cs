using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Enemies
{
    public class EnemiesSpawnerService
    {
        private readonly EnemiesFactory _enemiesFactory;

        private readonly Vector3 _spawnCenterPosition;
        private readonly Vector2 _randomOffset;
        private readonly float _radius;

        public EnemiesSpawnerService(EnemiesFactory enemiesFactory, SpawnerEnemiesConfig spawnerEnemiesConfig)
        {
            _enemiesFactory = enemiesFactory;
            _spawnCenterPosition = spawnerEnemiesConfig.SpawnPosition;
            _radius = spawnerEnemiesConfig.Radius;
            _randomOffset = spawnerEnemiesConfig.Offset;
        }

        public Entity Spawn(EnemyItemConfig enemyItemConfig)
        {
            Vector2 randomEdgePoint = Random.insideUnitCircle.normalized * (_radius + Random.Range(_randomOffset.x, _randomOffset.y));
            Vector3 spawnPoint = _spawnCenterPosition + new Vector3(randomEdgePoint.x, 0, randomEdgePoint.y);
            Entity spawnedEnemy = _enemiesFactory.Create(spawnPoint, enemyItemConfig.EnemyConfig);

            return spawnedEnemy;
        }
    }
}
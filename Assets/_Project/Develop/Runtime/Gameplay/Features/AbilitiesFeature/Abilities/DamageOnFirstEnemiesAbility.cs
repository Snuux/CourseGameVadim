using System;
using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Enemies;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature.Abilities
{
    public class DamageOnFirstEnemiesAbility : Ability
    {
        private readonly ApplyDamageOnFirstEnemiesAbilityConfig _config;
        private readonly EnemiesSpawnerService _enemiesSpawnerService;
        private readonly Entity _entity;

        public DamageOnFirstEnemiesAbility(
            Entity entity,
            ApplyDamageOnFirstEnemiesAbilityConfig config, 
            EnemiesSpawnerService enemiesSpawnerService) : base(config.ID)
        {
            _entity = entity;
            _config = config;
            _enemiesSpawnerService = enemiesSpawnerService;
        }

        public override void Activate()
        {
            if (_enemiesSpawnerService.Count <= _config.EnemiesCount)
            {
                float newHealth = _entity.CurrentHealth.Value - _entity.CurrentHealth.Value * _config.DamagePercent / 100f;
                _entity.CurrentHealth.Value -= Math.Max(0, newHealth);
            }
        }
    }
}
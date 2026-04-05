using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class AllyFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public AllyFactory(DIContainer container)
        {
            _container = container;

            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
        }
        
        public Entity CreateTower(Vector3 position, LevelConfig levelConfig)
        {
            TowerConfig towerConfig = _configsProviderService.GetConfig<TowerConfig>();

            Entity tower = _entitiesFactory.CreateTower(position, towerConfig, levelConfig);
            tower.AddCurrentTarget();
            tower.AddIsTower();
            tower.AddTeam(new ReactiveVariable<Teams>(Teams.Ally));
            
            _entitiesLifeContext.Add(tower);

            return tower;
        }

        public Entity CreateMine(Vector3 position, Entity owner)
        {
            Entity mine = _entitiesFactory.CreateMine(position, owner);
            mine.AddTeam(new ReactiveVariable<Teams>(owner.Team.Value));
            
            _brainsFactory.CreateMineBrain(mine, new NearestDamageableTargetSelector(mine));
            _entitiesLifeContext.Add(mine);

            return mine;
        }

        public Entity CreateCursorAttacker()
        {
            Entity entity = _entitiesFactory.CreateCursorAttacker();
            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Ally));
            
            _entitiesLifeContext.Add(entity);
            return entity;
        }
    }
}
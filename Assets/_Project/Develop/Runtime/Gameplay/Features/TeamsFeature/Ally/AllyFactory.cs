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

            Entity entity = _entitiesFactory.CreateTower(position, towerConfig, levelConfig);
            entity.AddCurrentTarget();
            entity.AddIsTower();
            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Ally));
            
            //_brainsFactory.CreateMainHeroBrain(entity, new NearestDamageableTargetSelector(entity));

            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
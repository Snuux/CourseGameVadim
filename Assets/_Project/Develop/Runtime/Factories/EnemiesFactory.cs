using System;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Enemies
{
    public class EnemiesFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public EnemiesFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
        }
        
        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entitiesFactory.CreateGhost(position, ghostConfig);
                    _brainsFactory.CreateGhostBrain(entity, new MainTowerTargetSelector(entity));
                    
                    break;
                case ArcherConfig archerConfigConfig:
                    entity = _entitiesFactory.CreateArcher(position, archerConfigConfig);
                    _brainsFactory.CreateArcherBrain(entity, new MainTowerTargetSelector(entity));
                    
                    break;
                default:
                    throw new ArgumentException($"Not support {config.GetType()} type config");
            }

            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));
            
            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
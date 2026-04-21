using System;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Gameplay.Features.LootFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Enemies
{
    public class EnemiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly DropLootService _dropLootService;

        public EnemiesFactory(DIContainer container)
        {
            _container = container;

            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
            _dropLootService = container.Resolve<DropLootService>();
        }

        public Entity Create(Vector3 position, EntityConfig config)
        {
            Entity entity;

            switch (config)
            {
                case GhostConfig ghostConfig:
                    entity = _entitiesFactory.CreateGhost(position, ghostConfig);
                    _brainsFactory.CreateGhostBrain(entity);
                    break;
                default:
                    throw new ArgumentException($"Not support {config.GetType()} type config");
            }

            AddDropLootBehaviourTo(entity);

            entity.AddTeam(new ReactiveVariable<Teams>(Teams.Enemies));
            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private void AddDropLootBehaviourTo(Entity entity)
        {
            ICompositeCondition dropLootCondition = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.LootIsDropped.Value == false));

            entity
                .AddLootIsDropped()
                .AddCanDropLoot(dropLootCondition);

            entity.MustSelfRelease.Add(new FuncCondition(() => entity.LootIsDropped.Value));
            
            Debug.Log("AddDropLootBehaviourTo");

            entity.AddSystem(new DropLootSystem(_dropLootService));
        }
    }
}
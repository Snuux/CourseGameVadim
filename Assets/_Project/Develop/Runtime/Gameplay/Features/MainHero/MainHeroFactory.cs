using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Configs.Gameplay.Entities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.AI.Selectors;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class MainHeroFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesFactory _entitiesFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProviderService _configsProviderService;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        public MainHeroFactory(DIContainer container)
        {
            _container = container;

            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
        }

        public Entity Create(Vector3 position)
        {
            HeroConfig config = _configsProviderService.GetConfig<HeroConfig>();

            Entity entity = _entitiesFactory.CreateHero(position, config);
            
            entity
                .AddIsMainHero()
                .AddCurrentTarget()
                .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero));

            //AbilityFactory abilityFactory = _container.Resolve<AbilityFactory>();
            //AbilitiesList abilitiesList = new AbilitiesList();
            //
            //abilitiesList.Add(abilityFactory.CreateAbilityFor(entity, _configsProviderService.GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[1]));
            //abilitiesList.Add(abilityFactory.CreateAbilityFor(entity, _configsProviderService.GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[3]));
            //abilitiesList.Add(abilityFactory.CreateAbilityFor(entity, _configsProviderService.GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[3]));
            //abilitiesList.Add(abilityFactory.CreateAbilityFor(entity, _configsProviderService.GetConfig<AbilitiesConfigsContainer>().AbilityConfigs[3]));
            
            entity
                .AddAbilities()//abilitiesList)
                .AddSystem(new AbilityOnAddActivatorSystem());
            
            _brainsFactory.CreateMainHeroBrain(entity, new NearestDamageableTargetSelector(entity));

            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
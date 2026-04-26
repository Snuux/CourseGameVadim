using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature;
using _Project.Develop.Runtime.Infrastructure.DI;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature
{
    public class AbilityOnAddActivatorSystem : IInitializableSystem, IDisposableSystem
    {
        private AbilitiesList _abilitiesList;

        public void OnInit(Entity entity)
        {
            _abilitiesList = entity.Abilities;

            _abilitiesList.Added += OnAbilityAdd;

            foreach (Ability ability in _abilitiesList.Elements) 
                ability.Activate();
        }

        public void OnDispose()
        {
            _abilitiesList.Added -= OnAbilityAdd;
        }

        private void OnAbilityAdd(Ability ability)
        {
            ability.Activate();
        }
    }
}
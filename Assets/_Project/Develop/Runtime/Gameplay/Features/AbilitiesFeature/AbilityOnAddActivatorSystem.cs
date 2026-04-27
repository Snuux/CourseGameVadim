using _Project.Develop.Runtime.Configs.Meta.Abilities;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesFeature;

namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature
{
    public class AbilityOnAddActivatorSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly ShopAbilitiesConfig _shopAbilitiesConfig;
        private AbilitiesList _abilitiesList;

        public AbilityOnAddActivatorSystem(ShopAbilitiesConfig shopAbilitiesConfig)
        {
            _shopAbilitiesConfig = shopAbilitiesConfig;
        }

        public void OnInit(Entity entity)
        {
            _abilitiesList = entity.Abilities;

            _abilitiesList.Added += OnAbilityAdd;

            foreach (Ability ability in _abilitiesList.Elements)
                TryActivate(ability);
        }

        public void OnDispose()
        {
            if (_abilitiesList != null)
                _abilitiesList.Added -= OnAbilityAdd;
        }

        private void OnAbilityAdd(Ability ability)
        {
            TryActivate(ability);
        }

        private void TryActivate(Ability ability)
        {
            if (_shopAbilitiesConfig.GetConfigBy(ability.ID).ActivateOnType == AbilityActivationTypes.OnAdd)
                ability.Activate();
        }
    }
}

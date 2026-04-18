using System;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.AbilitySelectPopup
{
    public class SelectableAbilityPresenter : IPresenter
    {
        public event Action<SelectableAbilityPresenter> Selected;

        private AbilityFactory _abilityFactory;
        private Entity _entity;

        public SelectableAbilityPresenter(
            AbilityConfig abilityConfig,
            SelectableAbilityView view,
            AbilityFactory abilityFactory,
            Entity entity)
        {
            AbilityConfig =  abilityConfig;
            View = view;
            _abilityFactory = abilityFactory;
            _entity = entity;
        }
        
        public AbilityConfig AbilityConfig { get; }
        public SelectableAbilityView View { get; }

        public void Initialize()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.Icon.SetIcon(AbilityConfig.Icon);
            
            View.Icon.HideLevel();
            View.SetTabletText("NEW");

            View.Clicked += OnViewClicked;
        }

        public void Dispose()
        {
            View.Clicked -= OnViewClicked;
        }

        public void Provide()
        {
            Ability ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig);
            _entity.Abilities.Add(ability);
        }

        private void OnViewClicked() => Selected?.Invoke(this);
    }
}
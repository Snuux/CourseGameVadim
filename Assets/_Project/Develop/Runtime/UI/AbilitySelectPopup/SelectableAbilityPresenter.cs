using System;
using System.Linq;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilityFeature;
using _Project.Develop.Runtime.UI.Core;

namespace _Project.Develop.Runtime.UI.AbilitySelectPopup
{
    public class SelectableAbilityPresenter : IPresenter
    {
        public event Action<SelectableAbilityPresenter> Selected;

        private readonly AbilityFactory _abilityFactory;
        private readonly Entity _entity;

        private readonly int _level;

        public SelectableAbilityPresenter(
            AbilityConfig abilityConfig,
            SelectableAbilityView view,
            AbilityFactory abilityFactory,
            Entity entity,
            int level)
        {
            AbilityConfig = abilityConfig;
            View = view;
            _abilityFactory = abilityFactory;
            _entity = entity;
            _level = level;
        }

        public AbilityConfig AbilityConfig { get; }
        public SelectableAbilityView View { get; }

        public void Initialize()
        {
            View.SetName(AbilityConfig.Name);
            View.SetDescription(AbilityConfig.Description);
            View.Icon.SetIcon(AbilityConfig.Icon);

            InitByLevelConfig();

            View.Clicked += OnViewClicked;
        }

        public void Dispose()
        {
            View.Clicked -= OnViewClicked;
        }

        public void Provide()
        {
            Ability ability;

            if (AbilityConfig.IsUpgradable())
            {
                ability = _entity.Abilities.Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if (ability != null)
                {
                    ability.AddLevel(_level);
                    return;
                }
            }
            
            ability = _abilityFactory.CreateAbilityFor(_entity, AbilityConfig, _level);
            _entity.Abilities.Add(ability);
        }

        private void InitByLevelConfig()
        {
            if (AbilityConfig.IsUpgradable())
            {
                Ability ability = _entity.Abilities.Elements.FirstOrDefault(abil => abil.ID == AbilityConfig.ID);

                if (ability != null)
                {
                    View.Icon.ShowLevel();
                    View.Icon.SetLevel("LV."  + ability.CurrentLevel.Value);
                    View.SetTabletText("LV."  + ability.CurrentLevel.Value + "->" + "LV."  + ability.CurrentLevel.Value + _level);
                }
                else
                {
                    View.Icon.HideLevel();
                    View.SetTabletText("NEW LV."  + _level);
                }
            }
            else
            {
                View.Icon.HideLevel();
                View.SetTabletText("NEW");
            }
        }

        private void OnViewClicked() => Selected?.Invoke(this);
    }
}
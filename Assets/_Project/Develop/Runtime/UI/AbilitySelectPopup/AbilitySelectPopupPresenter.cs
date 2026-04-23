using System.Collections.Generic;
using _Project.Develop.Runtime.Configs.Gameplay.Abilities;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.AbilitiesDroppingFeature;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;

namespace _Project.Develop.Runtime.UI.AbilitySelectPopup
{
    public class AbilitySelectPopupPresenter : PopupPresenterBase
    {
        private const int AbilitiesCount = 3;

        private const string Title = "LEVEL {0} IN THIS ADVENTURE";
        private const string SelectAbilityText = "Select ability";

        private readonly AbilitySelectPopupView _view;

        private readonly Entity _entity;
        private readonly AbilityDropService _abilityDropService;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private List<SelectableAbilityPresenter> _presenters = new();
        private SelectableAbilityPresenter _selectedPresenter;

        private int _level;

        public AbilitySelectPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            AbilitySelectPopupView view,
            Entity entity,
            AbilityDropService abilityDropService,
            GameplayPresentersFactory gameplayPresentersFactory,
            ViewsFactory viewsFactory, 
            int level) : base(coroutinesPerformer)
        {
            _view = view;
            _entity = entity;
            _abilityDropService = abilityDropService;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _viewsFactory = viewsFactory;
            _level = level;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(string.Format(Title, _level));
            _view.SetAdditionalText(SelectAbilityText);
            _view.SelectButtonOff();

            _view.SelectButtonClicked += OnSelectButtonClicked;

            List<AbilityDropOption> dropOptions = _abilityDropService.Drop(AbilitiesCount, _entity);

            for (int i = 0; i < dropOptions.Count; i++)
            {
                SelectableAbilityView selectableAbilityView = _viewsFactory.Create<SelectableAbilityView>(
                    ViewIDs.SelectableAbilityView, _view.transform);

                _view.AbilityListView.Add(selectableAbilityView);

                SelectableAbilityPresenter selectableAbilityPresenter = _gameplayPresentersFactory
                    .CreateSelectableAbilityPresenter(dropOptions[i].Config, selectableAbilityView, _entity, dropOptions[i].Level);

                selectableAbilityPresenter.Selected += OnPresenterSelected;
                selectableAbilityPresenter.Initialize();

                _presenters.Add(selectableAbilityPresenter);
            }
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.SelectButtonOff();
            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (SelectableAbilityPresenter abilityPresenter in _presenters)
                abilityPresenter.Selected -= OnPresenterSelected;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.SelectButtonClicked -= OnSelectButtonClicked;

            foreach (SelectableAbilityPresenter abilityPresenter in _presenters)
            {
                abilityPresenter.Selected -= OnPresenterSelected;
                _view.AbilityListView.Remove(abilityPresenter.View);
                _viewsFactory.Release(abilityPresenter.View);
                abilityPresenter.Dispose();
            }
        }

        private void OnSelectButtonClicked()
        {
            _selectedPresenter.Provide();
            OnCloseRequest();
        }

        private void OnPresenterSelected(SelectableAbilityPresenter selected)
        {
            _view.SelectButtonOn();
            _view.AbilityListView.Select(selected.View);
            _selectedPresenter = selected;
        }
    }
}
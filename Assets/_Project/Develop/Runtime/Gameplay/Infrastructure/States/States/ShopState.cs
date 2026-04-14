using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class ShopState : State, IUpdatableState
    {
        private readonly ShopService _shopService;
        private readonly IInputService _inputService;
        private readonly GameplayPopupService _popupService;

        public ShopState(
            ShopService shopService,
            IInputService inputService,
            GameplayPopupService popupService)
        {
            _shopService = shopService;
            _inputService = inputService;
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Shopping!!!");

            _popupService.OpenShopPopup();
        }

        public void Update(float deltaTime)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_inputService.LeftMouseButtonDown)
                _shopService.Buy(ShopItemTypes.Mine, _inputService.MouseWorldPosition);
        }
    }
}

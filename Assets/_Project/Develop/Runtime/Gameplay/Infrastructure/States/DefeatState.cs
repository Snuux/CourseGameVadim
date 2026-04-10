using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.UI.Gameplay.ResultsPopup;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly GameplayPopupService _popupService;

        public DefeatState(
            IInputService inputService, 
            GameplayPopupService popupService) : base(inputService)
        {
            _popupService = popupService;
        }

        public override void Enter()
        {
            base.Enter();
            
            _popupService.OpenDefeatPopup();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.PauseFeature;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public abstract class EndGameState : State
    {
        private readonly IInputService _inputService;
        private readonly IPauseService _pauseService;

        protected EndGameState(IInputService inputService, IPauseService pauseService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
        }

        public override void Enter()
        {
            base.Enter();

            _inputService.IsEnabled = false;
            _pauseService.Pause();
        }

        public override void Exit()
        {
            base.Exit();

            _inputService.IsEnabled = true;
            _pauseService.Unpause();
        }
    }
}
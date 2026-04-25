using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackProcessSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _attackStarted;
        
        private ReactiveVariable<float> _attackProcessInitialTime;
        private ReactiveVariable<float> _attackProcessCurrentTime;
        private ReactiveVariable<bool> _inAttackProcess;

        private ReactiveVariable<float> _attackInitialActionTime;
        private ReactiveVariable<bool> _hasReachedActionTime;

        private bool _isAlreadyHasReachedActionTime;

        public void OnInit(Entity entity)
        {
            _attackStarted = entity.AttackStarted;

            _attackProcessInitialTime = entity.AttackProcessInitialTime;
            _attackProcessCurrentTime = entity.AttackProcessCurrentTime;
            _inAttackProcess = entity.InAttackProcess;

            _attackInitialActionTime = entity.AttackInitialActionTime;
            _hasReachedActionTime = entity.HasReachedActionTime;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_attackStarted.Value == false && _inAttackProcess.Value == false)
                return;
            
            _attackStarted.Value = false;
            _inAttackProcess.Value = true;
            
            _attackProcessCurrentTime.Value += deltaTime;

            //turn off bool _hasReachedActionTime on next frame
            //if (_isAlreadyHasReachedActionTime == true && _hasReachedActionTime.Value == true)
            //    _hasReachedActionTime.Value = false;
            
            //if action time is presented
            if (_attackProcessCurrentTime.Value >= _attackInitialActionTime.Value && _isAlreadyHasReachedActionTime == false)
            {
                _hasReachedActionTime.Value = true;
                _isAlreadyHasReachedActionTime = true;
            }
            
            //on end process attack
            if (_attackProcessCurrentTime.Value >= _attackProcessInitialTime.Value)
            {
                _inAttackProcess.Value = false;
                _attackProcessCurrentTime.Value = 0;
                _isAlreadyHasReachedActionTime = false;
            }
        }
    }
}
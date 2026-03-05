using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class SingleFrameDeathSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<bool> _isDead;
        
        private int _counter = 0;

        public void OnInit(Entity entity)
        {
            _isDead = entity.IsDead;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_counter > 0)
                _isDead.Value = true;

            _counter++;
        }
    }
}
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;

namespace _Project.Develop.Runtime.Gameplay.Features.SpawnFeature
{
    public class SpawnProcessTimerSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _spawnInitialTime;
        private ReactiveVariable<float> _spawnCurrentTime;
        private ReactiveVariable<bool> _inSpawnProcess;

        public void OnInit(Entity entity)
        {
            _spawnInitialTime = entity.SpawnInitialTime;
            _spawnCurrentTime = entity.SpawnCurrentTime;
            _inSpawnProcess = entity.InSpawnProcess;

            _spawnCurrentTime.Value = 0;
            _inSpawnProcess.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inSpawnProcess.Value == false)
                return;
            
            _spawnCurrentTime.Value += deltaTime;
            
            if (_spawnCurrentTime.Value >= _spawnInitialTime.Value) 
                _inSpawnProcess.Value = false;
        }
    }
}
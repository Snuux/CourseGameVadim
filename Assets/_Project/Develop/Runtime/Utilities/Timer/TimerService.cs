using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Timer
{
    public class TimerService : IDisposable
    {
        private readonly float _cooldown;
        private readonly ReactiveEvent _cooldownEnded;
        private readonly ReactiveVariable<float> _currentTime;
        private readonly ICoroutinesPerformer _coroutinePerformer;
        private Coroutine _cooldownProcess;

        public TimerService(
            float cooldown,
            ICoroutinesPerformer coroutinePerformer)
        {
            _cooldown = cooldown;
            _coroutinePerformer = coroutinePerformer;

            _cooldownEnded = new ReactiveEvent();
            _currentTime = new ReactiveVariable<float>();
        }

        public IReadOnlyEvent CooldownEnded => _cooldownEnded;
        public IReadOnlyVariable<float> CurrentTime => _currentTime;
        public bool IsOver => _currentTime.Value <= 0;

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            if (_cooldownProcess != null)
                _coroutinePerformer.StopPerform(_cooldownProcess);
        }

        public void Restart()
        {
            Stop();

            _cooldownProcess = _coroutinePerformer.StartPerform(CooldownProcess());
        }

        private IEnumerator CooldownProcess()
        {
            _currentTime.Value = _cooldown;

            while (IsOver == false)
            {
                _currentTime.Value -= Time.deltaTime;
                yield return null;
            }

            _cooldownEnded.Invoke();
        }
    }
}

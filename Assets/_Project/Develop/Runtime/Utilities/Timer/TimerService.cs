using System;
using System.Collections;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.Timer
{
    public class TimerService : IDisposable
    {
        private float _cooldown;

        private ReactiveEvent _cooldownEnded;

        private ReactiveVariable<float> _currentTime;

        private ICoroutinesPerformer _coroutinesPerformer;
        private Coroutine _cooldownProcess;

        public TimerService(float cooldown, ICoroutinesPerformer coroutinesPerformer)
        {
            _cooldown = cooldown;
            _coroutinesPerformer = coroutinesPerformer;

            _cooldownEnded = new ReactiveEvent();
            _currentTime = new ReactiveVariable<float>();
        }

        public ReactiveEvent CooldownEnded => _cooldownEnded;

        public ReactiveVariable<float> CurrentTime => _currentTime;

        public bool IsOver => _currentTime.Value <= 0;

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            if (_cooldownProcess != null) 
                _coroutinesPerformer.StopPerform(_cooldownProcess);
        }

        public void Restart()
        {
            Stop();

            _cooldownProcess = _coroutinesPerformer.StartPerform(CooldownProcess());
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
using System.Collections;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.CoroutinesManagment
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);
        void StopPerform(Coroutine coroutine);
    }
}
using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.SimpleTools
{
    public class IgnoreParentRotation : MonoBehaviour
    {
        private Quaternion _defaultRotation;

        private void Awake()
        {
            _defaultRotation = transform.rotation;
        }

        void LateUpdate()
        {
            transform.rotation = _defaultRotation;
        }
    }
}
using System.Collections.Generic;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class RemoveCollidersFromContacts : IInitializableSystem, IUpdatableSystem
    {
        private Buffer<Collider> _contacts;
        private LayerMask _mask;

        private readonly List<CapsuleCollider> _collidersToRemove;

        public RemoveCollidersFromContacts(List<CapsuleCollider> collidersToRemove)
        {
            _collidersToRemove = collidersToRemove;
        }

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactCollidersBuffer;
            _mask = entity.ContactsDetectingMask;
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CapsuleCollider collider in _collidersToRemove) 
                RemoveSelfFromContacts(collider);
        }

        private void RemoveSelfFromContacts(CapsuleCollider collider)
        {
            int indexToRemove = -1;

            for (int i = 0; i < _contacts.Count; i++)
            {
                if (_contacts.Items[i] == collider)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                for (int i = indexToRemove; i < _contacts.Count - 1; i++)
                {
                    _contacts.Items[i] = _contacts.Items[i + 1];
                }

                _contacts.Count--;
            }
        }
    }
}
using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Utilities;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    /*public class PreparationTriggerService
    {
        private ReactiveVariable<bool> _hasMainHeroContact = new();

        private EntitiesFactory _entitiesFactory;
        private EntitiesLifeContext _entitiesLifeContext;

        private Entity _nextStageTrigger;
        private Buffer<Entity> _nextStateTriggerContacts;

        public PreparationTriggerService(EntitiesFactory entitiesFactory, EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesFactory = entitiesFactory;
            _entitiesLifeContext = entitiesLifeContext;
        }

        public ReactiveVariable<bool> HasMainHeroContact => _hasMainHeroContact;

        public void Create(Vector3 position)
        {
            if (_nextStageTrigger != null)
                throw new InvalidOperationException("Trigger Already Created");

            _nextStageTrigger = _entitiesFactory.CreateContactTrigger(position);
            _nextStateTriggerContacts = _nextStageTrigger.ContactEntitiesBuffer;
        }

        public void Update(float deltaTime)
        {
            if (_nextStageTrigger == null)
                return;

            for (int i = 0; i < _nextStateTriggerContacts.Count; i++)
            {
                Entity contact = _nextStateTriggerContacts.Items[i];

                if (contact.HasComponent<IsMainHero>())
                {
                    _hasMainHeroContact.Value = true;
                    return;
                }
            }

            _hasMainHeroContact.Value = false;
        }

        public void Cleanup()
        {
            _entitiesLifeContext.Release(_nextStageTrigger);
            _hasMainHeroContact.Value = false;
            _nextStageTrigger = null;
            _nextStateTriggerContacts = null;
        }
    }*/
}
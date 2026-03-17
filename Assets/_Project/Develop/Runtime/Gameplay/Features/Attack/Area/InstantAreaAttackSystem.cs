using System;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.Attack.Area
{
    public class InstantAreaAttackSystem : IInitializableSystem, IDisposableSystem
    {
        private readonly EntitiesFactory _entitiesFactory;
        
        private ReactiveVariable<float> _areaAttackRadius;
        private ReactiveVariable<Vector3> _teleportTargetPosition;

        private ReactiveVariable<float> _damage;

        private IDisposable _teleportEventDisposable;

        private Entity _entity;

        public InstantAreaAttackSystem(EntitiesFactory entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }
        
        public void OnInit(Entity entity)
        {
            _damage = entity.InstantAttackDamage;
            _areaAttackRadius = entity.AreaAttackRadius;
            _teleportTargetPosition = entity.TeleportTargetPosition;
            _entity = entity;

            _teleportEventDisposable = entity.TeleportEvent.Subscribe(OnTeleportEnd);
        }

        public void OnDispose()
        {
            _teleportEventDisposable.Dispose();
        }

        private void OnTeleportEnd()
        {
            //_entitiesFactory.CreateAreaProjectile(_teleportTargetPosition.Value, Vector3.forward, _areaAttackRadius.Value, _damage.Value, _entity);

            Debug.Log($"Урон: {_damage.Value}, Радиус атаки: {_areaAttackRadius.Value}, Позиция атаки: {_teleportTargetPosition.Value}");
        }
    }
}
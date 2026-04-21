using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Features.LootFeature
{
    public class DropLootSystem : IInitializableSystem, IUpdatableSystem
    {
        private DropLootService _dropLootService;

        private ICompositeCondition _dropLootCondition;
        private ReactiveVariable<bool> _lootIsDropped;
        private Entity _entity;

        public DropLootSystem(DropLootService dropLootService)
        {
            _dropLootService = dropLootService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _dropLootCondition = entity.CanDropLoot;
            _lootIsDropped = entity.LootIsDropped;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_dropLootCondition.Evaluate())
            {
                DropLoot();
                _lootIsDropped.Value = true;
            }
        }

        private void DropLoot() => _dropLootService.DropLootFor(_entity);
    }
}
using _Project.Develop.Runtime.Gameplay.Features.LootFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Utilities.Reactive;
using _Project.Develop.Runtime.Utilities.StateMachineCore;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States
{
    public class CollectLootState : State, IUpdatableState
    {
        private LootPullingService _lootPullingService;
        private MainHeroHolderService _heroHolderService;

        public CollectLootState(LootPullingService lootPullingService, MainHeroHolderService heroHolderService)
        {
            _lootPullingService = lootPullingService;
            _heroHolderService = heroHolderService;
        }
        
        public override void Enter()
        {
            base.Enter();
            _lootPullingService.PullTo(_heroHolderService.MainHero);
        }

        public override void Exit()
        {
            base.Exit();
            _lootPullingService.Reset();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.ShopFeature;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.Conditions;
using _Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure.States.States
{
    public class DefeatState : EndGameState, IUpdatableState
    {
        private readonly StatisticsService _statisticsService;
        private readonly GameplayPopupService _popupService;
        private readonly ShopService _shopService;

        public DefeatState(
            IInputService inputService, 
            StatisticsService statisticsService, 
            GameplayPopupService popupService, 
            ShopService shopService) : base(inputService)
        {
            _statisticsService = statisticsService;
            _popupService = popupService;
            _shopService = shopService;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("ПОРАЖЕНИЕ!");
            Debug.Log($"Нажмите Q для перехода в меню");
            
            _statisticsService.Add(StatisticType.Defeats);
            
            _popupService.OpenDefeatPopup();
            _shopService.CleanupSpawnedItems();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
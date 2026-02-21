using _Project.Develop.Runtime.Gameplay.Features.Gameplay;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.UI;
using _Project.Develop.Runtime.UI.Core;
using _Project.Develop.Runtime.UI.Gameplay;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            container.RegisterAsSingle(c => CreateRandomSymbolsSequenceService(c, args));
            
            container.RegisterAsSingle(c => CreateGameplayRunningService(c, args));
            
            container.RegisterAsSingle(CreateGameplayStateService).NonLazy();
            
            container.RegisterAsSingle(CreateInputSequenceHandler);
            
            container.RegisterAsSingle(CreateGameplayPresenterFactory);
            
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            
            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static SequenceGenerationService CreateRandomSymbolsSequenceService(
            DIContainer c,
            GameplayInputArgs args)
        {
            SequenceGenerationService generationService = new SequenceGenerationService(args.Length, args.Symbols);
            
            generationService.GenerateRandomSequence();
            
            return generationService;
        }

        private static GameplayRunningService CreateGameplayRunningService(
            DIContainer c,
            GameplayInputArgs args)
        {
            return new GameplayRunningService(
                c.Resolve<GameplayStateService>(),
                c.Resolve<InputSequenceService>(),
                args.WinRewardGold,
                args.DefeatPenaltyGold,
                c.Resolve<WalletService>()
            );
        }

        private static GameplayStateService CreateGameplayStateService(DIContainer c)
        {
            SequenceGenerationService service = c.Resolve<SequenceGenerationService>();

            return new GameplayStateService(
                service.Length,
                service.Sequence
            );
        }

        private static InputSequenceService CreateInputSequenceHandler(DIContainer c)
        {
            return new InputSequenceService();
        }
        
        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();
            
            GameplayScreenView gameplayScreenView =
                c.Resolve<ViewsFactory>().Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);
            
            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresenterFactory>()
                .CreateGameplayScreenPresenter(
                    gameplayScreenView,
                    c.Resolve<SequenceGenerationService>().Sequence
                    );

            return presenter;
        }
        
        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gameplayUIRoot = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(gameplayUIRoot);
        }
        
        public static GameplayPresenterFactory CreateGameplayPresenterFactory(DIContainer c)
        {
            return new GameplayPresenterFactory(c);
        }

        public static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<GameplayUIRoot>());
        }
    }
}
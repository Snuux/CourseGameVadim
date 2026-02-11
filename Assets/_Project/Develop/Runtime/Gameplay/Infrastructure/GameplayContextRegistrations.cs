using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Gameplay.Features.Gameplay;
using _Project.Develop.Runtime.Gameplay.Features.Sequences;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Meta.Features.Statistics;
using _Project.Develop.Runtime.Meta.Features.Wallet;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;
using _Project.Develop.Runtime.Utilities.CoroutinesManagment;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации геймплейных сервисов");

            container.RegisterAsSingle(c => CreateRandomSymbolsSequenceService(c, args));
            container.RegisterAsSingle(CreateGameCycle);
            container.RegisterAsSingle(CreateGameFinishStateHandler);
            container.RegisterAsSingle(CreateInputSequenceHandler);
            container.RegisterAsSingle(CreateGameplaySwitcherSceneService);
            container.RegisterAsSingle(CreateGameplayGetRewardService);
        }

        private static SequenceGenerationService CreateRandomSymbolsSequenceService(DIContainer c,
            GameplayInputArgs args)
        {
            return new SequenceGenerationService(args.Length, args.Symbols);
        }

        private static GameplayRunningService CreateGameCycle(DIContainer c)
        {
            return new GameplayRunningService(
                c.Resolve<SequenceGenerationService>().Sequence,
                c.Resolve<GameplaySetFinishStateService>(),
                c.Resolve<InputSequenceService>(),
                c.Resolve<GameplayGetRewardService>(),
                c.Resolve<GameplaySwitcherSceneService>()
            );
        }

        private static GameplaySwitcherSceneService CreateGameplaySwitcherSceneService(DIContainer c)
        {
            return new GameplaySwitcherSceneService(
                c.Resolve<GameplaySetFinishStateService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>());
        }

        private static GameplaySetFinishStateService CreateGameFinishStateHandler(DIContainer c)
        {
            SequenceGenerationService service = c.Resolve<SequenceGenerationService>();

            return new GameplaySetFinishStateService(
                service.Length,
                service.Sequence
            );
        }

        public static GameplayGetRewardService CreateGameplayGetRewardService(DIContainer c)
        {
            LevelsRewardConfig levelsRewardConfig
                = c.Resolve<ConfigsProviderService>().GetConfig<LevelsRewardConfig>();

            return new GameplayGetRewardService(
                c.Resolve<GameStatisticsService>(),
                c.Resolve<WalletService>(),
                levelsRewardConfig
                );
        }

        private static InputSequenceService CreateInputSequenceHandler(DIContainer c)
        {
            return new InputSequenceService();
        }
    }
}
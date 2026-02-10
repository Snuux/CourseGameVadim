using _Project.Develop.Runtime.Configs;
using _Project.Develop.Runtime.Configs.Meta.Levels;
using _Project.Develop.Runtime.Gameplay.Features.GameCycle;
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
            Debug.Log("Process of services registration in gameplay scene");

            container.RegisterAsSingle(c => CreateRandomSymbolsSequenceService(c, args));
            container.RegisterAsSingle(CreateGameCycle);
            container.RegisterAsSingle(CreateGameFinishStateHandler);
            container.RegisterAsSingle(CreateInputSequenceHandler);
        }

        private static RandomSymbolsSequenceGenerationService CreateRandomSymbolsSequenceService(DIContainer c, GameplayInputArgs args)
        {
            return new RandomSymbolsSequenceGenerationService(args.Length, args.Symbols);
        }

        private static GameRunningStateHandler CreateGameCycle(DIContainer c)
        {
            LevelsRewardConfig levelsRewardConfig 
                = c.Resolve<ConfigsProviderService>().GetConfig<LevelsRewardConfig>();
            
            return new GameRunningStateHandler(
                c.Resolve<RandomSymbolsSequenceGenerationService>(),
                c.Resolve<GameFinishStateHandler>(),
                c.Resolve<InputSequenceService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<GameStatisticsService>(),
                c.Resolve<WalletService>(),
                levelsRewardConfig
                );
        }

        private static GameFinishStateHandler CreateGameFinishStateHandler(DIContainer c)
        {
            RandomSymbolsSequenceGenerationService service = c.Resolve<RandomSymbolsSequenceGenerationService>();
            
            Debug.Log("--- " + service.Sequence + " " + service.Length);
            
            return new GameFinishStateHandler(service.Length, service.Sequence);
        }

        private static InputSequenceService CreateInputSequenceHandler(DIContainer c)
        {
            return new InputSequenceService();
        }
    }
}
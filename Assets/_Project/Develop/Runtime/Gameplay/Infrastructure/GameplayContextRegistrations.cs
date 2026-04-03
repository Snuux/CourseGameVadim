using _Project.Develop.Runtime.Configs.Gameplay.Levels;
using _Project.Develop.Runtime.Configs.Gameplay.Stages;
using _Project.Develop.Runtime.Gameplay.EntitiesCore;
using _Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using _Project.Develop.Runtime.Gameplay.Features.AI;
using _Project.Develop.Runtime.Gameplay.Features.Enemies;
using _Project.Develop.Runtime.Gameplay.Features.InputFeature;
using _Project.Develop.Runtime.Gameplay.Features.MainHero;
using _Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature.Enemies;
using _Project.Develop.Runtime.Gameplay.Infrastructure.States;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.AssetsManagment;
using _Project.Develop.Runtime.Utilities.ConfigsManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _inputArgs;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _inputArgs = args;

            container.RegisterAsSingle(CreateEntitiesFactory);

            container.RegisterAsSingle(CreateEntitiesLifeContext);

            container.RegisterAsSingle(CreateCollidersRegistryService);

            container.RegisterAsSingle(CreateBrainsFactory);

            container.RegisterAsSingle(CreateAIBrainsContext);

            container.RegisterAsSingle<IInputService>(CreateDesktopInput);

            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();

            container.RegisterAsSingle(CreateAllyFactory);

            container.RegisterAsSingle(CreateEnemiesFactory);

            container.RegisterAsSingle(CreateStageFactory);

            container.RegisterAsSingle(CreateStagesProviderService);

            container.RegisterAsSingle(CreateTowerHolderService).NonLazy();

            container.RegisterAsSingle(CreateGameplayStateFactory);

            container.RegisterAsSingle(CreateGameplayStateContext);
            
            container.RegisterAsSingle(CreateEnemiesSpawnerService);
        }

        public static EnemiesSpawnerService CreateEnemiesSpawnerService(DIContainer c)
        {
            return new EnemiesSpawnerService(c.Resolve<EnemiesFactory>(), 
                c.Resolve<ConfigsProviderService>().GetConfig<SpawnerEnemiesConfig>());
        }

        public static GameplayStateContext CreateGameplayStateContext(DIContainer c)
        {
            return new GameplayStateContext(c.Resolve<GameplayStateFactory>().CreateGameplayStateMachine(_inputArgs));
        }

        public static GameplayStateFactory CreateGameplayStateFactory(DIContainer c)
        {
            return new GameplayStateFactory(c);
        }

        private static TowerHolderService CreateTowerHolderService(DIContainer c)
        {
            return new TowerHolderService(c.Resolve<EntitiesLifeContext>());
        }

        private static StageProviderService CreateStagesProviderService(DIContainer c)
        {
            return new StageProviderService(
                c.Resolve<ConfigsProviderService>()
                    .GetConfig<LevelsListConfig>().GetBy(_inputArgs.LevelNumber),
                c.Resolve<StagesFactory>());
        }

        private static StagesFactory CreateStageFactory(DIContainer c)
        {
            return new StagesFactory(c);
        }

        private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
        {
            return new EnemiesFactory(c);
        }

        private static AllyFactory CreateAllyFactory(DIContainer c)
        {
            return new AllyFactory(c);
        }

        private static DesktopInput CreateDesktopInput(DIContainer c)
        {
            return new DesktopInput();
        }

        private static AIBrainsContext CreateAIBrainsContext(DIContainer c)
        {
            return new AIBrainsContext();
        }

        private static BrainsFactory CreateBrainsFactory(DIContainer c)
        {
            return new BrainsFactory(c);
        }

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
        {
            return new CollidersRegistryService();
        }

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
        {
            return new MonoEntitiesFactory(
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesLifeContext>(),
                c.Resolve<CollidersRegistryService>());
        }

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
        {
            return new EntitiesLifeContext();
        }

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
        {
            return new EntitiesFactory(c);
        }
    }
}
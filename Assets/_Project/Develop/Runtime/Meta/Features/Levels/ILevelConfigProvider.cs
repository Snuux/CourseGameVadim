using _Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace _Project.Develop.Runtime.Meta.Features.Levels
{
    public interface ILevelConfigProviderService
    {
        LevelConfig Get();
    }
}
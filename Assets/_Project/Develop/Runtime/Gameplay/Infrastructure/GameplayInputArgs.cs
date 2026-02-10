using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int length, string symbols, int reward)
        {
            Length = length;
            Symbols = symbols;
            Reward = reward;
        }

        public int Length { get; }
        public string Symbols { get; }
        public int Reward { get; }
    }
}
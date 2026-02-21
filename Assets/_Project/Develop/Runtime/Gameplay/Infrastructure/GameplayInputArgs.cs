using _Project.Develop.Runtime.Configs.Meta.Wallet;
using _Project.Develop.Runtime.Utilities.SceneManagment;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(
            int length,
            string symbols,
            (CurrencyTypes, int) winRewardGold,
            (CurrencyTypes, int) defeatPenaltyGold)
        {
            Length = length;
            Symbols = symbols;
            WinRewardGold = winRewardGold;
            DefeatPenaltyGold = defeatPenaltyGold;
        }

        public int Length { get; }
        public string Symbols { get; }

        public (CurrencyTypes, int) WinRewardGold;
        public (CurrencyTypes, int) DefeatPenaltyGold;
    }
}
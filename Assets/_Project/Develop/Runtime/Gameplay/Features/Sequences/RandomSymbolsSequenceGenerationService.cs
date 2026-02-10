using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Sequences
{
    public class RandomSymbolsSequenceGenerationService
    {
        public RandomSymbolsSequenceGenerationService(int length, string symbols)
        {
            Length = length;
            Symbols = symbols;
            Sequence = GenerateSequence();
        }

        public string Symbols { get; private set; }
        public string Sequence { get; private set; }
        public int Length { get; private set; }
        
        private string GenerateSequence()
        {
            string sequence = "";

            for (int i = 0; i < Length; i++)
            {
                char randomSymbol = Symbols[Random.Range(0, Symbols.Length)];
                sequence += randomSymbol;
            }

            return sequence;
        }
    }
}
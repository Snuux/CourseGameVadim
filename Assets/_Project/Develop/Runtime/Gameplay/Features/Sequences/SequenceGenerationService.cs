using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features.Sequences
{
    public class SequenceGenerationService
    {
        public SequenceGenerationService(int length, string symbols)
        {
            Length = length;
            Symbols = symbols;
        }

        public string Symbols { get; private set; }
        public string Sequence { get; private set; }
        public int Length { get; private set; }
        
        public void GenerateRandomSequence()
        {
            string sequence = "";

            for (int i = 0; i < Length; i++)
            {
                char randomSymbol = Symbols[Random.Range(0, Symbols.Length)];
                
                sequence += randomSymbol;
            }

            Sequence = sequence;
        }
    }
}
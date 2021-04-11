using Oz.Algorithms;

namespace Oz.Rob
{
    public class Dice
    {
        private const int MinDiceValue = 1;
        private const int MaxDiceValue = 6;
        
        private readonly IRandomSource _randomSource = new DefaultRandomSource();
        
        public int Roll()
            => _randomSource.RandomValue(MinDiceValue, MaxDiceValue + 1);
    }
}
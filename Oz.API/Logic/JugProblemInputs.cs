namespace Oz.API.Logic
{
    public class JugProblemInputs
    {
        public int FirstJugCapacity { get; }
        public int SecondJugCapacity { get; }
        public int TargetFillAmount { get; }

        public JugProblemInputs(int firstJugCapacity, int secondJugCapacity, int targetFillAmount)
        {
            FirstJugCapacity = firstJugCapacity;
            SecondJugCapacity = secondJugCapacity;
            TargetFillAmount = targetFillAmount;
        }
    }
}
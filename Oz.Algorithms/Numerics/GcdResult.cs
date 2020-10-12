using Newtonsoft.Json;

namespace Oz.Algorithms.Numerics
{
    public struct GcdResult
    {
        public int FirstNumber { get; }
        public int FirstMultiplier { get; }
        public int SecondNumber { get; }
        public int SecondMultiplier { get; }
        public int GreaterCommonDivider { get; }

        public GcdResult(int firstNumber, int firstMultiplier, int secondNumber, int secondMultiplier,
            int greaterCommonDivider)
        {
            FirstNumber = firstNumber;
            FirstMultiplier = firstMultiplier;
            SecondNumber = secondNumber;
            SecondMultiplier = secondMultiplier;
            GreaterCommonDivider = greaterCommonDivider;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
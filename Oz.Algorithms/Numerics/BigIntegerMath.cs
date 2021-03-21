using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    public static class BigIntegerMath
    {
        public static BigInteger FloorFromDivision(BigInteger firstNumber, BigInteger secondNumber)
        {
            var result = BigInteger.DivRem(firstNumber, secondNumber, out var remainder);
            if (remainder == 0)
            {
                return result;
            }

            if ((firstNumber <= 0 && secondNumber < 0) || (firstNumber >= 0 && secondNumber > 0))
            {
                return result;
            }
            return result - 1;
        }
    }
}
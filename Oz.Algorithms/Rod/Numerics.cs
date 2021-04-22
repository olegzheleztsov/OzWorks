using System.Numerics;

namespace Oz.Algorithms.Rod
{
    public static class Numerics
    {
        public static BigInteger? FindPrime(int numDigits, int maxTests)
        {
            var randomSource = new DefaultRandomSource();
            for (int i = 0; i < 1000; i++)
            {
                BigInteger ten = 10;
                var rndNumber =
                    randomSource.RandomBigInteger(ten.Exponentiate(numDigits - 1), ten.Exponentiate(numDigits) - 1);
                if (rndNumber.IsPrime(maxTests))
                {
                    return rndNumber;
                }
            }

            return null;
        }
    }
}
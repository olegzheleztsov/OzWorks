using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    public class ExtendedEuclid
    {
        private readonly BigInteger _firstNumber;
        private readonly BigInteger _secondNumber;

        public ExtendedEuclid(BigInteger firstNumber, BigInteger secondNumber)
        {
            _firstNumber = firstNumber;
            _secondNumber = secondNumber;
        }

        public ExtendedEuclidResult Value
            => FindGcd(_firstNumber, _secondNumber);
        
        private ExtendedEuclidResult FindGcd(BigInteger firstNumber, BigInteger secondNumber)
        {
            if (secondNumber == 0)
            {
                return new ExtendedEuclidResult(firstNumber, 1, 0);
            }

            var extendedResult = FindGcd(secondNumber, firstNumber % secondNumber);
            return new ExtendedEuclidResult(extendedResult.Gcd, extendedResult.SecondMult,
                extendedResult.FirstMult - BigIntegerMath.FloorFromDivision(firstNumber, secondNumber) * extendedResult.SecondMult);
        }
    }

    public record ExtendedEuclidResult(BigInteger Gcd, BigInteger FirstMult, BigInteger SecondMult);
}
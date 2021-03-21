using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Oz.Algorithms.Numerics
{
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    public class Euclid
    {
        private readonly BigInteger _firstNumber;
        private readonly BigInteger _secondNumber;

        public Euclid(BigInteger firstNumber, BigInteger secondNumber)
        {
            _firstNumber = firstNumber;
            _secondNumber = secondNumber;
        }

        public BigInteger Value
            => FindGcd(_firstNumber, _secondNumber);

        private BigInteger FindGcd(BigInteger first, BigInteger second)
        {
            return second == 0 ? first : FindGcd(second, first % second);
        }
    }
}
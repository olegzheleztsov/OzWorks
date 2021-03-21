using System.Numerics;
using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class BigIntegerMathTests
    {
        [Theory]
        [InlineData(5, 3, 1)]
        [InlineData(6, 3, 2)]
        [InlineData(7, 3, 2)]
        [InlineData(0, 3, 0)]
        [InlineData(0, -3, 0)]
        [InlineData(-1, 3, -1)]
        [InlineData(-1, -3, 0)]
        [InlineData(1, -3, -1)]
        [InlineData(-5, 3, -2)]
        [InlineData(5, -3, -2)]
        [InlineData(-5, -3, 1)]
        public void Should_Correctly_Find_BigInteger_Floor(BigInteger firstNumber, BigInteger secondNumber, BigInteger result)
        {
            BigIntegerMath.FloorFromDivision(firstNumber, secondNumber).Should().Be(result);
        }

    }
}
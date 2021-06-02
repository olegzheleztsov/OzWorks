using System.Numerics;
using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class MillerRabinPseudorandomTests
    {
        [Theory]
        [InlineData(34, 1, 17)]
        [InlineData(32, 5, 1)]
        [InlineData(2, 1, 1)]
        [InlineData(100, 2, 25)]
        public void Should_Correctly_Split_Integer(int number, int power, int mult)
        {
            var result = MillerRabinPseudorandom.SplitEvenInteger(number);
            result.Power.Should().Be(power);
            result.Mult.Should().Be(mult);
        }
    }
}
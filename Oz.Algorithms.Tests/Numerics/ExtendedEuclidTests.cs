using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class ExtendedEuclidTests
    {
        [Fact]
        public void Should_Correctly_Find_Gcd_And_Mults()
        {
            var euclid = new ExtendedEuclid(99, 78);
            var result = euclid.Value;
            result.Gcd.Should().Be(3);
            result.FirstMult.Should().Be(-11);
            result.SecondMult.Should().Be(14);
        }
    }
}
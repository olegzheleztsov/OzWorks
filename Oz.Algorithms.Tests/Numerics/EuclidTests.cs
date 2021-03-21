using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class EuclidTests
    {
        [Fact]
        public void Should_Find_Gcd_Correctly()
        {
            var euclid = new Euclid(30, 21);
            euclid.Value.Should().Be(3);
        }
    }
}
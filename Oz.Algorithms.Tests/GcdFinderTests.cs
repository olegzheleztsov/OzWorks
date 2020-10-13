using System;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests
{
    public class GcdFinderTests
    {
        [Fact]
        public void Should_Find_Gcd_Correctly()
        {
            const int m = 1769;
            const int n = 551;
            var finder = new GcdFinder(m, n);

            var result = finder.Run();
            Assert.Equal(m, result.FirstNumber);
            Assert.Equal(n, result.SecondNumber);
            Assert.Equal(5, result.FirstMultiplier);
            Assert.Equal(-16, result.SecondMultiplier);
            Assert.Equal(29, result.GreaterCommonDivider);
        }

        [Fact]
        public void Should_Throws_When_Incorrect_Input()
        {
            const int m = 0;
            const int n = 0;
            Assert.Throws<ArgumentException>(() => new GcdFinder(m, 15));
            Assert.Throws<ArgumentException>(() => new GcdFinder(23, n));
        }
    }
}
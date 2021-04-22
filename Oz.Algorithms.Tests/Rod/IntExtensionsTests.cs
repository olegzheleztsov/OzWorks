using System;
using FluentAssertions;
using Oz.Algorithms.Rod;
using Xunit;

namespace Oz.Algorithms.Tests.Rod
{
    public class IntExtensionsTests
    {
        [Theory]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(2, 5, 32)]
        [InlineData(3, 3, 27)]
        [InlineData(5, 3, 125)]
        [InlineData(2, 10, 1024)]
        [InlineData(2, 0, 1)]
        [InlineData(-5, 3, -125)]
        public void Should_Correctly_Exponentiate(int value, int factor, int answer)
        {
            value.Exponentiate(factor).Should().Be(answer);
        }
        
        [Theory]
        [InlineData(2, 1,  5, 2)]
        [InlineData(2, 2,  5, 4)]
        [InlineData(2, 5,  5, 2)]
        [InlineData(3, 3,  5, 2)]
        [InlineData(5, 3,  5, 0)]
        [InlineData(2, 10,  5, 4)]
        [InlineData(2, 0, 5, 1)]
        [InlineData(-5, 3, 5, 0)]
        public void Should_Correctly_Exponentiate_By_Modulus(int value, int factor, int modulus, int answerMod)
        {
            value.Exponentiate(factor, modulus).Should().Be(answerMod);
        }

        [Fact]
        public void Should_Throws_Overflow_Exception_When_Out_Of_Range()
        {
            const int value = 1234567;
            var exponent = 123456;
            Assert.Throws<OverflowException>(() => value.Exponentiate(exponent));
        }

        [Fact]
        public void Should_Throws_Argument_Exception_When_Exponent_Negative()
        {
            const int value = 3;
            var exponent = -3;
            Assert.Throws<ArgumentException>(() => value.Exponentiate(exponent));
        }
    }
}
using FluentAssertions;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class ModularExponentiationTests
    {
        [Fact]
        public void Should_Correctly_Find_Exp_By_Modulo()
        {
            var exp = new ModularExponentiation(7, 560, 561);
            exp.Value.Should().Be(1);
        }
    }
}
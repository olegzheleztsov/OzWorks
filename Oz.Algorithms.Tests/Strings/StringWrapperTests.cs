using FluentAssertions;
using Oz.Algorithms.Strings;
using Xunit;

namespace Oz.Algorithms.Tests.Strings
{
    public class StringWrapperTests
    {
        [Theory]
        [InlineData("adfsdfsdf", "sdf", true)]
        [InlineData("sfgsfsdfsdf", "f", true)]
        [InlineData("sssss", "sssss", true)]
        [InlineData("ddddd", "ad", false)]
        public void Should_Correctly_Identify_Suffix(string source, string pattern, bool isSuffix)
        {
            new StringWrapper(source).IsSuffix(pattern).Should().Be(isSuffix);
        }
    }
}
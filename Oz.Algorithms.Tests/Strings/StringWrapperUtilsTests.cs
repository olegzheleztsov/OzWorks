using System.Linq;
using FluentAssertions;
using Oz.Algorithms.Strings;
using Xunit;

namespace Oz.Algorithms.Tests.Strings
{
    public class StringWrapperUtilsTests
    {
        [Fact]
        public void Should_Naive_Match_Work_Correctly()
        {
            var source = new StringWrapper("abaaabababaaab");
            var pattern = new StringWrapper("aab");
            var indices = StringWrapperUtils.MatchNaive(source, pattern);
            indices.OrderBy(i => i).Should().Equal(new int[] {4, 12});
        }
    }
}
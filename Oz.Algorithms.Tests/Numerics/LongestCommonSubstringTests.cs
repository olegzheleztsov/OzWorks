using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.Numerics
{
    public class LongestCommonSubstringTests
    {
        [Fact]
        public void Should_Correctly_Find_LCS()
        {
            char[] x = {'A', 'B', 'C', 'B', 'D', 'A', 'B'};
            char[] y = {'B', 'D', 'C', 'A', 'B', 'A'};
            var longestSubstring = new LongestCommonSubstring<char>(x, y);
            var result = longestSubstring.GetLongestSubstring();
            Assert.Equal(new[] {'B', 'C', 'B', 'A'}, result);
        }

        [Fact]
        public void Should_Correctly_Find_LCS_By_Both_Approaches()
        {
            char[] x = {'A', 'B', 'C', 'B', 'D', 'A', 'B'};
            char[] y = {'B', 'D', 'C', 'A', 'B', 'A'};
            var longestSubstring = new LongestCommonSubstring<char>(x, y);
            var result1 = longestSubstring.GetLongestSubstring();
            var result2 = longestSubstring.GetLongestSubstring2();
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void Should_Correctly_Find_LCS_By_TopDown_And_BottomTop()
        {
            char[] x = {'A', 'B', 'C', 'B', 'D', 'A', 'B'};
            char[] y = {'B', 'D', 'C', 'A', 'B', 'A'};
            var longestSubstring = new LongestCommonSubstring<char>(x, y);
            var topDownResult = longestSubstring.GetLongestSubstringTopDown();
            var bottomUpResult = longestSubstring.GetLongestSubstring();
            Assert.Equal(topDownResult, bottomUpResult);
        }
    }
}
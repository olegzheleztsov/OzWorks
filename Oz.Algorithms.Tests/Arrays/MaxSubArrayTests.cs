using Oz.Algorithms.Arrays;
using Xunit;

namespace Oz.Algorithms.Tests.Arrays
{
    public class MaxSubArrayTests
    {
        [Fact]
        public void Should_Correctly_Find_Max_SubArray()
        {
            int[] arr = {-1, -1, 1, 2, 1, -1, -1};
            MaxSubArray maxSubArray = new MaxSubArray(arr);
            var (maxLeftIndex, maxRightIndex, sum) = maxSubArray.FindMaxCrossingSubarray(1, 3, 5);
            Assert.Equal(2, maxLeftIndex);
            Assert.Equal(4, maxRightIndex);
            Assert.Equal(4, sum);
        }

        [Fact]
        public void Should_Correctly_Find_Max_Sub_Array_When_All_Elements_Are_Negative()
        {
            int[] arr = {-1, -1, -1, -1, -1, -1, -1};
            MaxSubArray maxSubArray = new MaxSubArray(arr);
            var (maxLeftIndex, maxRightIndex, sum) = maxSubArray.FindMaxCrossingSubarray(1, 3, 5);
            Assert.Equal(3, maxLeftIndex);
            Assert.Equal(4, maxRightIndex);
            Assert.Equal(-2, sum);
        }
    }
}
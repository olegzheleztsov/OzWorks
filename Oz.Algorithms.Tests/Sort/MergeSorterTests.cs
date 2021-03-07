using FluentAssertions;
using Oz.Algorithms.Sort;
using Xunit;

namespace Oz.Algorithms.Tests.Sort
{
    public class MergeSorterTest
    {
        [Theory]
        [InlineData(new int[] { }, new int[] { })]
        [InlineData(new[] {1}, new[] {1})]
        [InlineData(new[] {1, 2}, new[] {1, 2})]
        [InlineData(new[] {2, 1}, new[] {1, 2})]
        [InlineData(new[] {1, 1}, new[] {1, 1})]
        [InlineData(new[] {2, 2, 1, 1, 3, 3, 3}, new[] {1, 1, 2, 2, 3, 3, 3})]
        [InlineData(new[] {1, 0, 1, 0, 1, 0}, new[] {0, 0, 0, 1, 1, 1})]
        public void MergeSorter_Should_Sort_Correctly(int[] inputArray, int[] expectedArray)
        {
            static int KeySelector(int element) => element;

            var mergeSorter = new MergeSorter<int>();
            mergeSorter.Sort(inputArray, KeySelector, Comparisions.StandardComparision);
            inputArray.Should().BeInAscendingOrder();
            inputArray.Should().Equal(expectedArray);
        }
    }
}
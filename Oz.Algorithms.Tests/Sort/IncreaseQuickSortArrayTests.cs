using System.Linq;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort;
using Xunit;

namespace Oz.Algorithms.Tests.Sort
{
    public class IncreaseQuickSortArrayTests
    {
        [Fact]
        public void Should_Be_Sorted_Correctly()
        {
            var array = Enumerable.Range(1, 1000).ToArray();
            var arrayToSort = new ShuffledArray<int>(array);
            var sortedArray = new IncreaseQuickSortArray<int>(arrayToSort, k => k);
            Assert.Equal(array, sortedArray);
        }
    }
}
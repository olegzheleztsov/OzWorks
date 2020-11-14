using System.Linq;
using Oz.Algorithms.Numerics;
using Oz.Algorithms.Sort;
using Xunit;

namespace Oz.Algorithms.Tests.Sort
{
    public class DecreaseQuickSortArrayTests
    {
        [Fact]
        public void Should_Sort_Correctly()
        {
            var array = Enumerable.Range(1, 1000).OrderByDescending(k => k).ToArray();
            var arrayToSort = new ShuffledArray<int>(array);
            var sortedArray = new DecreaseQuickSortArray<int>(arrayToSort, k => k);
            Assert.Equal(array, sortedArray);
        }
    }
}
#region

using System.Collections.Generic;
using FluentAssertions;
using Oz.Algorithms.Sort;
using Xunit;

#endregion

namespace Oz.Algorithms.Tests.Sort
{
    public class MergeSorterTest
    {
        [Fact]
        public void MergeSorter_Should_Sort_Correctly()
        {
            var inputList =
                new List<(int[] inputArray, int[] expectedArray)>
                {
                    (new int[] { }, new int[] { }),
                    (new[] {1}, new[] {1}),
                    (new[] {1, 2}, new[] {1, 2}),
                    (new[] {2, 1}, new[] {1, 2}),
                    (new[] {1, 1}, new[] {1, 1}),
                    (new[] {2, 2, 1, 1, 3, 3, 3}, new[] {1, 1, 2, 2, 3, 3, 3}),
                    (new[] {1, 0, 1, 0, 1, 0}, new[] {0, 0, 0, 1, 1, 1})
                };

            static int KeySelector(int element)
            {
                return element;
            }

            var mergeSorter = new MergeSorter<int>();

            foreach (var (inputArray, expectedArray) in inputList)
            {
                mergeSorter.Sort(inputArray, KeySelector, Comparisions.StandardComparision);
                inputArray.Should().BeInAscendingOrder();
                inputArray.Should().Equal(expectedArray);
            }
        }
    }
}
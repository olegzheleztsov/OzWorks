using System;
using System.Linq;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests
{
    public class ShuffledArrayTests
    {

        [Fact]
        public void Shuffled_Array_Should_Have_Same_Size_As_Source_Array()
        {
            var sourceArray = new int[] {1, 2, 3};
            var shuffledArray = new ShuffledArray<int>(sourceArray);
            int[] resultArray = shuffledArray;
            
            Assert.Equal(sourceArray.Length, resultArray.Length);
        }

        [Fact]
        public void Shuffled_Array_Returns_The_Same_Array_When_Source_Size_Is_One()
        {
            var sourceArray = new int[] {1};
            var shuffledArray = new ShuffledArray<int>(sourceArray);
            int[] resultArray = shuffledArray;
            
            Assert.Equal(sourceArray.Length, resultArray.Length);
            Assert.Equal(sourceArray[0], resultArray[0]);
            Assert.NotSame(sourceArray, resultArray);
        }

        [Fact]
        public void Shuffled_Array_Should_Throws_When_Initialized_By_Null_Reference()
        {
            int[] sourceArray = null;

            Assert.Throws<NullReferenceException>(() => new ShuffledArray<int>(sourceArray));
        }

        [Fact]
        public void Shuffled_Array_Shuffle_The_Same_Elements()
        {
            var sourceArray = new int[] {1, 2, 3, 4, 5};
            var shuffledArray = new ShuffledArray<int>(sourceArray);
            int[] resultArray = shuffledArray;
            Assert.Equal(sourceArray, resultArray.OrderBy(e => e).ToArray());
            Assert.NotSame(sourceArray, resultArray);
        }
    }
}
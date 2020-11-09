using System;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Numerics;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class HeapTests
    {

        [Theory]
        [InlineData(0, 14, 1)]
        [InlineData(1, 8, 3)]
        [InlineData(3, 2, 7)]
        [InlineData(4, 1, 9)]
        [InlineData(2, 9, 5)]
        public void Should_Represent_Correct_Left_Children(int parentIndex, int leftValue, int leftIndex)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            
            Assert.Equal((leftValue, leftIndex), heap.Left(parentIndex));
        }

        [Theory]
        [InlineData(0, 10, 2)]
        [InlineData(2, 3, 6)]
        [InlineData(1, 7, 4)]
        [InlineData(3, 4, 8)]
        public void Should_Represent_Correct_Right_Children(int parentIndex, int rightValue, int rightIndex)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.Equal((rightValue, rightIndex), heap.Right(parentIndex));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(5)]
        [InlineData(6)]
        public void Leaves_Should_Not_Have_Left_Child(int nodeIndex)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.False(heap.HasLeft(nodeIndex));
        }
        
        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(5)]
        [InlineData(6)]
        public void Leaves_Should_Not_Have_Right_Child(int nodeIndex)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.False(heap.HasRight(nodeIndex));
        }

        [Fact]
        public void Root_Should_Not_Have_Parent()
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.False(heap.HasParent(0));
        }

        [Theory]
        [InlineData(7, 8, 3)]
        [InlineData(8, 8, 3)]
        [InlineData(3, 14, 1)]
        [InlineData(9, 7, 4)]
        [InlineData(4, 14, 1)]
        [InlineData(1, 16, 0)]
        [InlineData(5, 10, 2)]
        [InlineData(6, 10, 2)]
        [InlineData(2, 16, 0)]
        public void Should_Have_Valid_Parents(int index, int parentValue, int parentIndex)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.Equal((parentValue, parentIndex), heap.Parent(index));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(5)]
        [InlineData(6)]
        public void Throws_When_Request_Invalid_Left_Child(int index)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.Throws<ArgumentException>(() => heap.Left(index));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(5)]
        [InlineData(6)]
        public void Throws_When_Request_Invalid_Right_Child(int index)
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            Assert.Throws<ArgumentException>(() => heap.Right(index));  
        }

        [Fact]
        public void MaxHeapify_Should_Correctly_Rearrange_Heap()
        {
            var heap = new Heap<int>(new int[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1}, val => val, StandardComparision);
            heap[1] = -1;
            heap.MaxHeapify(1);
            Assert.Equal(8, heap[1]);
            Assert.Equal(4, heap[3]);
            Assert.Equal(2, heap[7]);
            Assert.Equal(-1, heap[8]);
        }

        [Fact]
        public void Should_Build_Max_Heap_Correctly()
        {
            int[] data = {16, 10, 14, 7, 8, 3, 9, 4, 2, 1};
            var shuffledData = new ShuffledArray<int>(data);
            data = shuffledData;
            var heap = Heap<int>.MaxHeap(data, key => key, StandardComparision);

            for (int i = 0; i < heap.HeapSize; i++)
            {
                if (heap.HasLeft(i))
                {
                    Assert.True(heap[i] >= heap.Left(i).value);
                }

                if (heap.HasRight(i))
                {
                    Assert.True(heap[i] >= heap.Right(i).value);
                }
            }
        }

        [Fact]
        public void Should_Correctly_Heapify_Special_Case_621()
        {
            var heap = new Heap<int>(new int[]{27, 17, 3, 16, 13, 10, 1, 5, 7, 12, 4, 8, 9, 0}, data => data, StandardComparision);
            heap.MaxHeapify(2);
            Assert.Equal(10, heap[2]);
            Assert.Equal(9, heap[5]);
            Assert.Equal(3, heap[12]);
        }

        private static readonly Comparison<int> StandardComparision = (a, b) => a.CompareTo(b);
    }
}
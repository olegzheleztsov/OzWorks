using System;
using Oz.Algorithms.DataStructures;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class OzDequeueTests
    {
        [Fact]
        public void Should_Fill_Correctly()
        {
            var dequeue = new OzDequeue<int>(5);
            dequeue.EnqueueLeft(1);
            dequeue.EnqueueLeft(2);
            dequeue.EnqueueRight(3);
            dequeue.EnqueueRight(4);
            dequeue.EnqueueRight(5);
            
            Assert.True(dequeue.IsFull);
            Assert.Equal(2, dequeue.DequeueLeft());
            Assert.Equal(1, dequeue.DequeueLeft());
            Assert.Equal(3, dequeue.DequeueLeft());
            Assert.Equal(5, dequeue.DequeueRight());
            Assert.Equal(4, dequeue.DequeueRight());
            Assert.True(dequeue.IsEmpty);
        }

        [Fact]
        public void Should_Correctly_Enqueue_Left_And_Dequeue_Right()
        {
            var dequeue = new OzDequeue<int>(3);
            dequeue.EnqueueLeft(1);
            dequeue.EnqueueLeft(2);
            dequeue.EnqueueLeft(3);
            
            Assert.True(dequeue.IsFull);
            Assert.Equal(1, dequeue.DequeueRight());
            Assert.Equal(2, dequeue.DequeueRight());
            Assert.Equal(3, dequeue.DequeueRight());
            Assert.True(dequeue.IsEmpty);
        }

        [Fact]
        public void Should_Throws_On_Overflow_From_Left()
        {
            var dequeue = new OzDequeue<int>(3);
            dequeue.EnqueueLeft(1);
            dequeue.EnqueueLeft(2);
            dequeue.EnqueueLeft(3);
            Assert.Throws<IndexOutOfRangeException>(() => dequeue.EnqueueLeft(4));
        }

        [Fact]
        public void Should_Throws_On_Overflow_From_Right()
        {
            var dequeue = new OzDequeue<int>(3);
            dequeue.EnqueueRight(1);
            dequeue.EnqueueRight(2);
            dequeue.EnqueueRight(3);
            Assert.Throws<IndexOutOfRangeException>(() => dequeue.EnqueueRight(4));
        }

        [Fact]
        public void Should_Throws_On_Empty_From_Left()
        {
            var dequeue = new OzDequeue<int>(3);
            dequeue.EnqueueLeft(1);
            dequeue.EnqueueRight(2);
            dequeue.EnqueueLeft(3);

            dequeue.DequeueLeft();
            dequeue.DequeueLeft();
            dequeue.DequeueLeft();
            Assert.Throws<IndexOutOfRangeException>(() => dequeue.DequeueLeft());
        }

        [Fact]
        public void Should_Throws_On_Empty_From_Right()
        {
            var dequeue = new OzDequeue<int>(3);
            dequeue.EnqueueLeft(1);
            dequeue.EnqueueRight(2);
            dequeue.EnqueueLeft(3);

            while (!dequeue.IsEmpty)
            {
                dequeue.DequeueRight();
            }

            Assert.Throws<IndexOutOfRangeException>(() => dequeue.DequeueRight());
        }
    }
}
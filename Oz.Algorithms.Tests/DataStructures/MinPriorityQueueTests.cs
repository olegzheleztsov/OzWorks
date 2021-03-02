using System;
using System.Linq;
using FluentAssertions;
using Oz.Algorithms.DataStructures;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class MinPriorityQueueTests
    {
        public record Element(int Data);

        [Fact]
        public void Should_Correctly_Return_Minimum()
        {
            var queue = new MinPriorityQueue<Element>();
            queue.Insert(new Element(10), 10);
            queue.Insert(new Element(5), 5);
            queue.Length.Should().Be(2);
            queue.Minimum().Data.Should().Be(5);
        }

        [Fact]
        public void Should_Correctly_Extract_Minimum()
        {
            var queue = new MinPriorityQueue<Element>();
            queue.Insert(new Element(10), 10);
            queue.Insert(new Element(5), 5);
            queue.ExtractMinimum().Data.Should().Be(5);
            queue.Length.Should().Be(1);
            queue.ExtractMinimum().Data.Should().Be(10);
            queue.IsEmpty.Should().BeTrue();
            Assert.Throws<IndexOutOfRangeException>(() => queue.ExtractMinimum());
            Assert.Throws<InvalidOperationException>(() => queue.Minimum());
        }

        [Fact]
        public void Should_Correctly_Enumerate_Elements()
        {
            var queue = new MinPriorityQueue<Element>();
            queue.Insert(new Element(5), 5);
            queue.Insert(new Element(2), 2);
            queue.Insert(new Element(100), 100);

            queue.Select(e => e.Data).ToList().Should().Equal(new[] {2, 5, 100});
        }
        
    }
}
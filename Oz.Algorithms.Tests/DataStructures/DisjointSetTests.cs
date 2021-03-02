using System;
using System.Linq;
using FluentAssertions;
using Oz.Algorithms.DataStructures;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures
{
    public class DisjointSetTests
    {
        private readonly  Func<int, int, bool> _comparer = (a, b) => a == b;
        
        [Fact]
        public void Should_Correctly_Create_Sets()
        {
            var newSet = DisjointSet<int>.MakeSet(1, (a, b) => a == b);
            newSet.Enumerate().Should().Equal(new[] {1});
            newSet.Count.Should().Be(1);
        }

        [Fact]
        public void Should_Correctly_Union_Sets()
        {
            var firstSet = DisjointSet<int>.MakeSet(1, (a, b) => a == b);
            var secondSet = DisjointSet<int>.MakeSet(2, (a, b) => a == b);
            var unionSet = DisjointSet<int>.Union(firstSet, secondSet);
            unionSet.Enumerate().OrderBy(e => e).Should().Equal(new[] {1, 2});
            unionSet.Count.Should().Be(2);
        }

        [Fact]
        public void Should_Union_With_Empty_Set_Correctly()
        {
            var firstSet = DisjointSet<int>.Empty(_comparer);
            var secondSet = DisjointSet<int>.MakeSet(2, _comparer);
            var unionSet = DisjointSet<int>.Union(firstSet, secondSet);
            unionSet.Enumerate().Should().Equal(new[] {2});
            unionSet.Count.Should().Be(1);
        }

        [Fact]
        public void Should_Union_Empty_Sets_Correctly()
        {
            var first = DisjointSet<int>.Empty(_comparer);
            var second = DisjointSet<int>.Empty(_comparer);
            var union = DisjointSet<int>.Union(first, second);
            union.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void Should_Find_Set_Correctly()
        {
            var testSet = DisjointSet<int>.MakeSet(new int[] {1, 2, 3}, _comparer);
            var element = testSet.Find(data => data == 2);
            DisjointSet<int>.FindSet(element).Should().BeSameAs(testSet);
        }

        [Fact]
        public void Should_Delete_By_Data_Correctly()
        {
            var testSet = DisjointSet<int>.MakeSet(new int[] { 1, 2, 3 }, _comparer);
            var success = testSet.Delete(2);
            success.Should().BeTrue();
            testSet.Enumerate().OrderBy(i => i).Should().Equal(new int[] { 1, 3 });
            testSet.Count.Should().Be(2);
        }

        [Fact]
        public void Should_Delete_Fail_Non_Existing_Element()
        {
            var testSet = DisjointSet<int>.MakeSet(new int[] { 1, 2, 3 }, _comparer);
            var success = testSet.Delete(20);
            success.Should().BeFalse();
            testSet.Enumerate().OrderBy(i => i).Should().Equal(new int[] { 1, 2, 3 });
            testSet.Count.Should().Be(3);
        }

        [Fact]
        public void Should_Delete_From_Empty_Set_Correctly()
        {
            var testSet = DisjointSet<int>.Empty(_comparer);
            var success = testSet.Delete(10);
            success.Should().BeFalse();
            testSet.Count.Should().Be(0);
            testSet.IsEmpty.Should().BeTrue();
        }


        [Fact]
        public void Should_Delete_From_Single_Set_Correctly()
        {
            var testSet = DisjointSet<int>.MakeSet(1, _comparer);
            var success = testSet.Delete(1);
            testSet.IsEmpty.Should().BeTrue();
            testSet.Count.Should().Be(0);
        }

        [Fact]
        public void Should_Delete_All_Elements_Correctly()
        {
            var testSet = DisjointSet<int>.MakeSet(new int[] { 1, 2, 3, 4, 5 }, _comparer);
            testSet.Count.Should().Be(5);
            for(int i = 1; i <= 5; i++)
            {
                var success = testSet.Delete(i);
                success.Should().BeTrue();
            }
            testSet.IsEmpty.Should().BeTrue();
            testSet.Count.Should().Be(0);
        }

        [Fact]
        public void Should_Delete_By_Ref_Correctly()
        {
            var testSet = DisjointSet<int>.MakeSet(new int[] { 1, 2, 3 }, _comparer);
            var elem = testSet.Find(i => i == 2);
            testSet.Delete(elem);
            testSet.Count.Should().Be(2);
            testSet.Enumerate().OrderBy(i => i).Should().Equal(new int[] { 1, 3 });

            testSet.Delete(testSet.Find(i => i == 3));
            testSet.Count.Should().Be(1);
            testSet.Enumerate().OrderBy(i => i).Should().Equal(new int[] { 1 });

            var success = testSet.Delete(testSet.Find(i => i == 10));
            success.Should().BeFalse();
            testSet.Count.Should().Be(1);

            testSet.Delete(testSet.Find(i => i == 1));
            testSet.IsEmpty.Should().BeTrue();
            testSet.Count.Should().Be(0);
        }
    }
}
using FluentAssertions;
using Oz.Algorithms.Rod.Trees;
using Xunit;

namespace Oz.Algorithms.Tests.Rod.Trees
{
    public class BinaryNodeExtensionsTests
    {
        private BinaryNode<int> BuildLCATestTree()
        {
            var n17 = new BinaryNode<int>(17);
            var n6 = new BinaryNode<int>(6);
            var n24 = new BinaryNode<int>(24);
            var n3 = new BinaryNode<int>(3);
            var n12 = new BinaryNode<int>(12);
            var n20 = new BinaryNode<int>(20);
            var n26 = new BinaryNode<int>(26);
            var n2 = new BinaryNode<int>(2);
            var n5 = new BinaryNode<int>(5);
            var n7 = new BinaryNode<int>(7);
            var n25 = new BinaryNode<int>(25);
            
            n17.SetLeftRight(n6, n24);
            n6.SetLeftRight(n3, n12);
            n24.SetLeftRight(n20, n26);
            n3.SetLeftRight(n2, n5);
            n12.SetLeftRight(n7, null);
            n26.SetLeftRight(n25, null);
            return n17;
        }

        [Theory]
        [InlineData(6, 24, 17)]
        [InlineData(3, 7, 6)]
        [InlineData(6, 7, 6)]
        [InlineData(2, 25, 17)]
        [InlineData(20, 25, 24)]
        [InlineData(2, 17, 17)]
        [InlineData(20, 26, 24)]
        public void Should_Correctly_Find_Lca(int value1, int value2, int expectedValue)
        {
            // Arrange
            var comparision = Comparisions.StandardComparision;
            var root = BuildLCATestTree();

            // Act
            var lca = root.FindLcaSortedTree(value1, value2, comparision);
            
            // Assert
            lca.Data.Should().Be(expectedValue);
        }
    }
}
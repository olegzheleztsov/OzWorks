using FluentAssertions;
using Oz.Algorithms.Rod.Trees;
using Xunit;

namespace Oz.Algorithms.Tests.Rod.Trees
{
    public class BinaryNodeParentedExtensionsTests
    {
        private BinaryNodeParented<int> BuildLCATestTree()
        {
            var n17 = new BinaryNodeParented<int>(17);
            var n6 = new BinaryNodeParented<int>(6);
            var n24 = new BinaryNodeParented<int>(24);
            var n3 = new BinaryNodeParented<int>(3);
            var n12 = new BinaryNodeParented<int>(12);
            var n20 = new BinaryNodeParented<int>(20);
            var n26 = new BinaryNodeParented<int>(26);
            var n2 = new BinaryNodeParented<int>(2);
            var n5 = new BinaryNodeParented<int>(5);
            var n7 = new BinaryNodeParented<int>(7);
            var n25 = new BinaryNodeParented<int>(25);
            
            n17.SetLeftRightParent(n6, n24, null);
            n6.SetLeftRightParent(n3, n12, n17);
            n24.SetLeftRightParent(n20, n26, n17);
            n3.SetLeftRightParent(n2, n5, n6);
            n12.SetLeftRightParent(n7, null, n6);
            n20.SetLeftRightParent(null, null, n24);
            n26.SetLeftRightParent(n25, null, n24);
            n2.Parent = n3;
            n5.Parent = n3;
            n7.Parent = n12;
            n25.Parent = n26;
            return n17;
        }

        [Theory]
        [InlineData(6, 24, 17)]
        [InlineData(2, 7, 6)]
        [InlineData(6, 5, 6)]
        [InlineData(25, 2, 17)]

        public void Should_Correctly_Find_Lca(int node1Value, int node2Value, int expectedLca)
        {
            var tree = BuildLCATestTree();

            var node1 = tree.FindNode(node1Value, Comparisions.StandardComparision);
            var node2 = tree.FindNode(node2Value, Comparisions.StandardComparision);
            var result = BinaryNodeParentedExtensions.FindLcaParentPointers(node1, node2);

            result.Data.Should().Be(expectedLca);
        }
    }
}
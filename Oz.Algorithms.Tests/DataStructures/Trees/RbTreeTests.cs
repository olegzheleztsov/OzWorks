using System.Linq;
using FluentAssertions;
using Oz.Algorithms.DataStructures.Trees;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures.Trees
{
    public class RbTreeTests
    {
        private static RbTree<int> CreateTestTree()
        {
            var tree = new RbTree<int>(val => val);
            var n2 = tree.CreateNode(2);
            var n3 = tree.CreateNode(3);
            var n4 = tree.CreateNode(4);
            var n6 = tree.CreateNode(6);
            var n7 = tree.CreateNode(7);
            var n11 = tree.CreateNode(11);
            var n9 = tree.CreateNode(9);
            var n18 = tree.CreateNode(18);
            var n12 = tree.CreateNode(12);
            var n14 = tree.CreateNode(14);
            var n17 = tree.CreateNode(17);
            var n19 = tree.CreateNode(19);
            var n22 = tree.CreateNode(22);
            var n20 = tree.CreateNode(20);

            n3.SetLeft(n2);
            n4.SetLeft(n3);
            n4.SetRight(n6);
            n7.SetLeft(n4);

            n22.SetLeft(n20);
            n19.SetRight(n22);
            n14.SetLeft(n12);
            n14.SetRight(n17);
            n18.SetLeft(n14);
            n18.SetRight(n19);
            n11.SetLeft(n9);
            n11.SetRight(n18);
            n7.SetRight(n11);
            tree.SetRoot(n7);
            return tree;
        }

        private RbTree<int> CreateTestTreeForInsert()
        {
            var tree = new RbTree<int>(val => val);
            var n11 = tree.CreateNode(11);
            var n2 = tree.CreateNode(2, TreeNodeColor.Red);
            var n1 = tree.CreateNode(1);
            var n7 = tree.CreateNode(7);
            var n5 = tree.CreateNode(5, TreeNodeColor.Red);
            var n8 = tree.CreateNode(8, TreeNodeColor.Red);
            var n14 = tree.CreateNode(14);
            var n15 = tree.CreateNode(15, TreeNodeColor.Red);

            n7.SetLeft(n5);
            n7.SetRight(n8);
            n2.SetLeft(n1);
            n2.SetRight(n7);
            n14.SetRight(n15);
            n11.SetLeft(n2);
            n11.SetRight(n14);
            tree.SetRoot(n11);
            return tree;
        }

        [Fact]
        public void Should_Correctly_Find_Successor_In_RbTree()
        {
            var tree = CreateTestTreeForInsert();
            var searcher = BinaryTreeSearcherFactory
                .Create(tree, n => (n as RbTreeNode<int>).Data, SearchMethod.Iterative);
            var node = searcher.Search(2);
            node.Should().NotBeNull();
            var successor = tree.FindSuccessor(node);
            successor.Should().NotBeNull();
            (successor as RbTreeNode<int>).Data.Should().Be(5);

            var n15 = searcher.Search(15);
            tree.IsNull(tree.FindSuccessor(n15)).Should().BeTrue();
        }

        [Fact]
        public void Should_Correctly_Find_Predecessor_In_RbTree()
        {
            var tree = CreateTestTreeForInsert();
            var searcher = BinaryTreeSearcherFactory
                .Create(tree, n => (n as RbTreeNode<int>).Data, SearchMethod.Iterative);
            var node = searcher.Search(2);
            node.Should().NotBeNull();
            tree.FindPredecessor(node).Data.Should().Be(1);
            tree.IsNull(tree.FindPredecessor(searcher.Search(1))).Should().BeTrue();
        }

        [Fact]
        public void Should_Correctly_Rotate_Left()
        {
            var tree = CreateTestTree();

            static int KeySelector(ITreeNode node)
            {
                return ((RbTreeNode<int>) node).Data;
            }

            var searcher = BinaryTreeSearcherFactory.Create(tree, KeySelector, SearchMethod.Iterative);

            var x = searcher.Search(11);
            var y = searcher.Search(18);
            tree.TreeLeftRotate(x as RbTreeNode<int>);

            Assert.True(y.LeftChild == x);
            Assert.True(y.RightChild == searcher.Search(19));
            Assert.True(((RbTreeNode<int>) y).ParentNode == searcher.Search(7));
            Assert.True(searcher.Search(7).RightChild == y);
            Assert.True(((RbTreeNode<int>) x).ParentNode == y);
            Assert.True(((BinaryTreeNode<int>) searcher.Search(19)).ParentNode == y);
        }

        [Fact]
        public void Should_Correctly_Rotate_Right()
        {
            var tree = CreateTestTree();

            static int KeySelector(ITreeNode node)
            {
                return ((RbTreeNode<int>) node).Data;
            }

            var searcher = BinaryTreeSearcherFactory.Create(tree, KeySelector, SearchMethod.Iterative);

            var x = searcher.Search(11);
            var y = searcher.Search(18);
            tree.TreeLeftRotate(x as RbTreeNode<int>);
            tree.TreeRightRotate(y as RbTreeNode<int>);

            Assert.True(x.RightChild == y);
            Assert.True(y.ParentNode == x);
            Assert.True(x.LeftChild == searcher.Search(9));
            Assert.True(searcher.Search(9).ParentNode == x);
            Assert.True(y.RightChild == searcher.Search(19));
            Assert.True(searcher.Search(19).ParentNode == y);
            Assert.True(y.LeftChild == searcher.Search(14));
            Assert.True(searcher.Search(14).ParentNode == y);
            Assert.True(x.ParentNode == searcher.Search(7));
            Assert.True(searcher.Search(7).RightChild == x);
        }

        [Fact]
        public void Should_Insert_Correctly()
        {
            var tree = CreateTestTreeForInsert();
            var z = tree.CreateNode(4, TreeNodeColor.Red);
            tree.Insert(z);

            var searcher =
                BinaryTreeSearcherFactory.Create<RbTreeNode<int>>(tree, node => node.Data, SearchMethod.Recursive);

            Assert.True(searcher.Search(4).Color == TreeNodeColor.Red);
            Assert.True(searcher.Search(4).ParentNode == searcher.Search(5));
            Assert.True(searcher.Search(5).LeftChild == searcher.Search(4));
            Assert.True(searcher.Search(5).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(1).ParentNode == searcher.Search(2));
            Assert.True(searcher.Search(1).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(2).LeftChild == searcher.Search(1));
            Assert.True(searcher.Search(2).RightChild == searcher.Search(5));
            Assert.True(searcher.Search(2).Color == TreeNodeColor.Red);
            Assert.True(searcher.Search(7).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(7).LeftChild == searcher.Search(2));
            Assert.True(searcher.Search(7).RightChild == searcher.Search(11));
            Assert.True(searcher.Search(11).Color == TreeNodeColor.Red);
            Assert.True(searcher.Search(11).ParentNode == searcher.Search(7));
            Assert.True(searcher.Search(11).LeftChild == searcher.Search(8));
            Assert.True(searcher.Search(8).ParentNode == searcher.Search(11));
            Assert.True(searcher.Search(8).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(11).RightChild == searcher.Search(14));
            Assert.True(searcher.Search(14).ParentNode == searcher.Search(11));
            Assert.True(searcher.Search(14).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(14).RightChild == searcher.Search(15));
            Assert.True(searcher.Search(15).ParentNode == searcher.Search(14));
            Assert.True(searcher.Search(15).Color == TreeNodeColor.Red);
        }

        [Fact]
        public void Shoud_Correctly_Enumerate_Tree()
        {
            var tree = CreateTestTree();
            var output = tree.Enumerate(7, 17).ToList();
            Assert.True(output.All(node => tree.KeySelector(node.Data) >= 7 && tree.KeySelector(node.Data) <= 17));
            Assert.True(output.Count() == 6);
        }

        [Fact]
        public void Should_Correctly_Enumerate_On_Non_Root_Subtree()
        {
            var tree = CreateTestTree();
            var searcher =
                BinaryTreeSearcherFactory.Create(tree, node => tree.KeySelector(node.Value), SearchMethod.Recursive);
            var n4 = searcher.Search(4);
            var output = tree.Enumerate(4, 7, n4 as RbTreeNode<int>).ToList();
            Assert.True(output.All(node => tree.KeySelector(node.Data) >= 4 && tree.KeySelector(node.Data) <= 7));
            Assert.True(output.Count() == 2);
        }
    }
}
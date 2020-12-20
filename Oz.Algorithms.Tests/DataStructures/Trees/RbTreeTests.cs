using System;
using Oz.Algorithms.DataStructures.Trees;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures.Trees
{
    public class RbTreeTests
    {
        private RbTree<int> CreateTestTree()
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
            var n11 = tree.CreateNode(11, TreeNodeColor.Black);
            var n2 = tree.CreateNode(2, TreeNodeColor.Red);
            var n1 = tree.CreateNode(1, TreeNodeColor.Black);
            var n7 = tree.CreateNode(7, TreeNodeColor.Black);
            var n5 = tree.CreateNode(5, TreeNodeColor.Red);
            var n8 = tree.CreateNode(8, TreeNodeColor.Red);
            var n14 = tree.CreateNode(14, TreeNodeColor.Black);
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
        public void Should_Correctly_Rotate_Left()
        {
            var tree = CreateTestTree();
            static int KeySelector(ITreeNode node) => ((RbTreeNode<int>) node).Data;
            var searcher = BinaryTreeSearcherFactory.Create(tree, KeySelector, SearchMethod.Iterative);

            var x = searcher.Search(11);
            var y = searcher.Search(18);
            tree.TreeLeftRotate(x as RbTreeNode<int>);
            
            Assert.True(y.LeftChild == x);
            Assert.True(y.RightChild == searcher.Search(19));
            Assert.True(((RbTreeNode<int>) y).RbParent == searcher.Search(7));
            Assert.True(searcher.Search(7).RightChild == y);
            Assert.True(((RbTreeNode<int>) x).Parent == y);
            Assert.True(((BinaryTreeNode<int>) searcher.Search(19)).Parent == y);
        }

        [Fact]
        public void Should_Correctly_Rotate_Right()
        {
            var tree = CreateTestTree();
            static int KeySelector(ITreeNode node) => ((RbTreeNode<int>) node).Data;
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
            Assert.True(searcher.Search(4).RbParent == searcher.Search(5));
            Assert.True(searcher.Search(5).RbLeft == searcher.Search(4));
            Assert.True(searcher.Search(5).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(1).RbParent == searcher.Search(2));
            Assert.True(searcher.Search(1).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(2).RbLeft == searcher.Search(1));
            Assert.True(searcher.Search(2).RbRight == searcher.Search(5));
            Assert.True(searcher.Search(2).Color == TreeNodeColor.Red);
            Assert.True(searcher.Search(7).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(7).RbLeft == searcher.Search(2));
            Assert.True(searcher.Search(7).RbRight == searcher.Search(11));
            Assert.True(searcher.Search(11).Color == TreeNodeColor.Red);
            Assert.True(searcher.Search(11).RbParent == searcher.Search(7));
            Assert.True(searcher.Search(11).RbLeft == searcher.Search(8));
            Assert.True(searcher.Search(8).RbParent == searcher.Search(11));
            Assert.True(searcher.Search(8).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(11).RbRight == searcher.Search(14));
            Assert.True(searcher.Search(14).RbParent == searcher.Search(11));
            Assert.True(searcher.Search(14).Color == TreeNodeColor.Black);
            Assert.True(searcher.Search(14).RbRight == searcher.Search(15));
            Assert.True(searcher.Search(15).RbParent == searcher.Search(14));
            Assert.True(searcher.Search(15).Color == TreeNodeColor.Red);
        }
    }
}
using System.Linq;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.DataStructures.Trees;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures.Trees
{
    public class OrderStatTreeTests
    {
        private OrderStatTree<int> CreateTestTree()
        {
            var tree = new OrderStatTree<int>(key => key);
            var n3 = tree.CreateNode(3, TreeNodeColor.Red);
            var n7 = tree.CreateNode(7);
            var n12 = tree.CreateNode(12);
            var n10 = tree.CreateNode(10, TreeNodeColor.Red);
            var n14 = tree.CreateNode(14);
            var n14_1 = tree.CreateNode(14, TreeNodeColor.Red);
            var n16 = tree.CreateNode(16);
            var n17 = tree.CreateNode(17, TreeNodeColor.Red);
            var n20 = tree.CreateNode(20, TreeNodeColor.Red);
            var n19 = tree.CreateNode(19);
            var n21 = tree.CreateNode(21);
            var n21_1 = tree.CreateNode(21);

            var n26 = tree.CreateNode(26);

            var n35 = tree.CreateNode(35, TreeNodeColor.Red);
            var n39 = tree.CreateNode(39, TreeNodeColor.Red);
            var n38 = tree.CreateNode(38);
            var n28 = tree.CreateNode(28);
            var n30 = tree.CreateNode(30, TreeNodeColor.Red);
            var n47 = tree.CreateNode(47);
            var n41 = tree.CreateNode(41);

            n7.SetLeft(n3);
            n10.SetLeft(n7).SetRight(n12);
            n16.SetLeft(n14_1);
            n14.SetLeft(n10).SetRight(n16);

            n19.SetRight(n20);
            n21.SetLeft(n19).SetRight(n21_1);
            n17.SetLeft(n14).SetRight(n21);


            n38.SetLeft(n35).SetRight(n39);
            n30.SetLeft(n28).SetRight(n38);
            n41.SetLeft(n30).SetRight(n47);

            n26.SetLeft(n17).SetRight(n41);
            tree.SetRoot(n26);
            return tree;
        }

        [Fact]
        public void Size_Of_Nodes_Should_Be_Computed_Correctly()
        {
            var tree = CreateTestTree();
            var searcher =
                BinaryTreeSearcherFactory.Create<OrderStatTreeNode<int>>(tree, node => node.Data,
                    SearchMethod.Recursive);
            Assert.Equal(1, searcher.Search(3).Size);
            Assert.Equal(2, searcher.Search(7).Size);
            Assert.Equal(1, searcher.Search(12).Size);
            Assert.Equal(4, searcher.Search(10).Size);
            Assert.True(7 == searcher.Search(14).Size || 1 == searcher.Search(14).Size);
            Assert.Equal(2, searcher.Search(16).Size);
            Assert.Equal(1, searcher.Search(20).Size);
            Assert.Equal(2, searcher.Search(19).Size);
            Assert.True(4 == searcher.Search(21).Size || 1 == searcher.Search(21).Size);
            Assert.Equal(12, searcher.Search(17).Size);
            Assert.Equal(20, searcher.Search(26).Size);

            Assert.Equal(1, searcher.Search(35).Size);
            Assert.Equal(1, searcher.Search(39).Size);
            Assert.Equal(3, searcher.Search(38).Size);
            Assert.Equal(1, searcher.Search(28).Size);
            Assert.Equal(5, searcher.Search(30).Size);
            Assert.Equal(1, searcher.Search(47).Size);
            Assert.Equal(7, searcher.Search(41).Size);
        }

        [Fact]
        public void Selection_Should_Work_Correctly()
        {
            var tree = CreateTestTree();
            var walker = BinaryTreeWalkerFactory.Create(tree, TreeWalkStrategy.Inorder);

            for (var targetRank = 1; targetRank <= tree.Count(); targetRank++)
            {
                var index = 0;
                var target = targetRank;
                ITreeNode targetNode = null;
                walker.Walk(node =>
                {
                    index++;
                    if (index == target)
                    {
                        targetNode = node;
                    }
                });
                Assert.Equal(targetNode, tree.SelectByRank(tree.Root as OrderStatTreeNode<int>, targetRank));
            }
        }

        [Fact]
        public void Rank_Should_Be_Computed_Correctly()
        {
            var tree = CreateTestTree();
            var walker = BinaryTreeWalkerFactory.Create(tree, TreeWalkStrategy.Inorder);

            foreach (var testNode in tree)
            {
                var tn = testNode;
                var currentRank = 0;
                var realRank = -1;

                walker.Walk(node =>
                {
                    currentRank++;
                    if (tn == node)
                    {
                        realRank = currentRank;
                    }
                });
                Assert.Equal(realRank, tree.Rank(tn));
            }
        }
    }
}
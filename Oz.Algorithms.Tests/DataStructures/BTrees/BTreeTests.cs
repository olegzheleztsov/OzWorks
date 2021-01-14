using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Oz.Algorithms.DataStructures.BTrees;
using Xunit;

namespace Oz.Algorithms.Tests.DataStructures.BTrees
{
    public class BTreeTests
    {
        [Fact]
        public void Should_Split_Works_Correctly()
        {
            BTree<string> tree = new BTree<string>((treeDegree) =>
            {
                var n = new BTreeNode<string>(true, default) {KeyCount = treeDegree - 1};
                return n;
            }, node =>
            {

            },(t, parent, index) => null,  4);

            BTreeNode<string> x = tree.CreateNode(false, "X");
            x.KeyCount = 3;
            x.Keys[0] = 100;
            x.Keys[1] = 200;
            x.Keys[2] = 300;

            x.Children[0] = tree.CreateNode(true, "A");
            x.Children[1] = tree.CreateNode(true, "B");
            
            x.Children[2] = tree.CreateNode(false, "C");
            x.Children[2].KeyCount = 7;
            x.Children[2].Keys[0] = 110;
            x.Children[2].Keys[1] = 120;
            x.Children[2].Keys[2] = 130;
            x.Children[2].Keys[3] = 140;
            x.Children[2].Keys[4] = 150;
            x.Children[2].Keys[5] = 160;
            x.Children[2].Keys[6] = 170;
            x.Children[2].Children[0] = tree.CreateNode(true, "AA");
            x.Children[2].Children[1] = tree.CreateNode(true, "BB");
            x.Children[2].Children[2] = tree.CreateNode(true, "CC");
            x.Children[2].Children[3] = tree.CreateNode(true, "DD");
            x.Children[2].Children[4] = tree.CreateNode(true, "EE");
            x.Children[2].Children[5] = tree.CreateNode(true, "FF");
            x.Children[2].Children[6] = tree.CreateNode(true, "GG");
            x.Children[2].Children[7] = tree.CreateNode(true, "HH");
            
            x.Children[3] = tree.CreateNode(true, "D");
            
            tree.SplitChild(x, 2);
            
            Assert.Equal(4, x.KeyCount);
            Assert.Equal(5, x.Children.Length);
            Assert.Equal("C", x.Children[2].Data);
            Assert.Equal(3, x.Children[2].KeyCount);
            Assert.Equal(3, x.Children[3].KeyCount);
            Assert.Equal(new string[]{"AA", "BB", "CC", "DD"}, x.Children[2].Children.Select(c => c.Data).ToArray());
            Assert.Equal(new int[] {110, 120, 130}, x.Children[2].Keys);
            Assert.Equal(new string[]{"EE", "FF", "GG", "HH"}, x.Children[3].Children
                .Select(c => c.Data).ToArray());
            Assert.Equal(new int[]{150, 160, 170}, x.Children[3].Keys);
            Assert.Equal(new int[]{100, 200, 140, 300}, x.Keys);
            
        }
    }
}
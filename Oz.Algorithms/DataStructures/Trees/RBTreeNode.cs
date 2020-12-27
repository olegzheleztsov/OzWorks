namespace Oz.Algorithms.DataStructures.Trees
{
    public class RbTreeNode<T> : BinaryTreeNode<T>, IColoredTreeNode
    {
        public RbTreeNode(T data, TreeNodeColor color = TreeNodeColor.Black) : base(data)
        {
            Color = color;
        }

        public TreeNodeColor Color { get; set; }
        
    }
}
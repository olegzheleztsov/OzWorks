namespace Oz.Algorithms.DataStructures.Trees
{
    public class RbTreeNode<T> : BinaryTreeNode<T>, IColoredTreeNode
    {
        public RbTreeNode(T data, TreeNodeColor color = TreeNodeColor.Black) : base(data)
        {
            Color = color;
        }

        public TreeNodeColor Color { get; set; }

        public RbTreeNode<T> RbLeft
        {
            get => Left as RbTreeNode<T>;
            set => Left = value;
        }

        public RbTreeNode<T> RbRight
        {
            get => Right as RbTreeNode<T>;
            set => Right = value;
        }

        public RbTreeNode<T> RbParent
        {
            get => Parent as RbTreeNode<T>;
            set => Parent = value;
        }
    }
}
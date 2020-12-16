namespace Oz.Algorithms.DataStructures.Trees
{
    public class BinaryTreeNode<T> : ITreeNode
    {
        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public T Data { get; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
        public BinaryTreeNode<T> Parent { get; set; }

        public bool IsRoot => Parent == null;

        public bool HasLeft => Left != null;

        public bool HasRight => Right != null;

        public ITreeNode LeftChild => Left;

        public ITreeNode RightChild => Right;
        public ITreeNode ParentNode => Parent;

        public override string ToString()
        {
            return $"Data: {Data?.ToString()}, Left: {Left != null}, Right: {Right != null}, Parent: {Parent != null}";
        }

        public void SetLeft(BinaryTreeNode<T> other)
        {
            Left = other;
            other.Parent = this;
        }

        public void SetRight(BinaryTreeNode<T> other)
        {
            Right = other;
            other.Parent = this;
        }
    }
}
namespace Oz.Algorithms.DataStructures
{
    public class BinaryTreeNode<T>
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

        public override string ToString()
        {
            return $"Data: {Data?.ToString()}, Left: {Left != null}, Right: {Right != null}, Parent: {Parent != null}";
        }
    }
}
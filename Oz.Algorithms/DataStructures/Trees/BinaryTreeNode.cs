namespace Oz.Algorithms.DataStructures.Trees
{
    public class BinaryTreeNode<T> : ITreeNode
    {
        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public T Data { get; }
        public bool IsRoot => ParentNode == null;

        public bool HasLeft => LeftChild != null;

        public bool HasRight => RightChild != null;

        public ITreeNode LeftChild
        {
            get;
            set;
        }

        public ITreeNode RightChild
        {
            get;
            set;
        }

        public ITreeNode ParentNode
        {
            get;
            private set;
        }

        public virtual void SetParent(ITreeNode parentNode)
        {
            ParentNode = parentNode;
        }

        public object Value => Data;

        public override string ToString()
        {
            return $"Data: {Data?.ToString()}, Left: {LeftChild != null}, Right: {RightChild != null}, Parent: {ParentNode != null}";
        }

        public BinaryTreeNode<T> SetLeft(BinaryTreeNode<T> other)
        {
            LeftChild = other;
            other.SetParent(this);
            return this;
        }

        public BinaryTreeNode<T> SetRight(BinaryTreeNode<T> other)
        {
            RightChild = other;
            other.SetParent(this);
            return this;
        }
    }
}
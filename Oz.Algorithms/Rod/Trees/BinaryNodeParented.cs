namespace Oz.Algorithms.Rod.Trees
{
    public class BinaryNodeParented<T> : IBinaryNode<BinaryNodeParented<T>, T>
    {
        public BinaryNodeParented(T data)
            => Data = data;
        
        public T Data { get; }
        
        public BinaryNodeParented<T> LeftChild { get; set; }
        public BinaryNodeParented<T> RightChild { get; set; }
        public BinaryNodeParented<T> Parent { get; set; }
        public bool Marked { get; set; }

        public void SetLeftRightParent(BinaryNodeParented<T> left, BinaryNodeParented<T> right,
            BinaryNodeParented<T> parent)
        {
            LeftChild = left;
            RightChild = right;
            Parent = parent;
        }
    }
}
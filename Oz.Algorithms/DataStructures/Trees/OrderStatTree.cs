using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class OrderStatTree<T> : IBinaryTree
    {
        private readonly Func<T, int> _keySelector;
        private OrderStatTreeNode<T> _root;
        private OrderStatTreeNode<T> Nil { get; }
        
        public OrderStatTree(Func<T, int> keySelector)
        {
            _keySelector = keySelector;
            Nil = new OrderStatTreeNode<T>(default, TreeNodeColor.Black, true);
            Nil.Left = Nil.Right = Nil;
        }

        public void SetRoot(OrderStatTreeNode<T> root)
        {
            _root = root;
            _root.Parent = Nil;
        }

        public ITreeNode Root
        {
            get => _root;
            set => _root = value as OrderStatTreeNode<T>;
        }

        public ITreeNode NullNode => Nil;
        public Func<object, int> KeySelector => obj => _keySelector((T) obj);

        public bool IsNull(ITreeNode node)
        {
            return node == Nil;
        }
    }
}
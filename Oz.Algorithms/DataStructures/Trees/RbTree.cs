using System;

namespace Oz.Algorithms.DataStructures.Trees
{
    public sealed class RbTree<T> : IBinaryTree
    {
        private readonly Func<T, int> _keySelector;
        private RbTreeNode<T> _root;

        public RbTree(Func<T, int> keySelector)
        {
            Nil = new RbTreeNode<T>(default);
            Nil.RbLeft = Nil.RbRight = Nil;
            _root = Nil;
            _keySelector = keySelector;
        }

        private RbTreeNode<T> Nil { get; }

        public ITreeNode NullNode => Nil;
        public Func<object, int> KeySelector => obj => _keySelector((T) obj);

        public ITreeNode Root
        {
            get => _root;
            set => _root = value as RbTreeNode<T>;
        }


        public bool IsNull(ITreeNode node)
        {
            return node == Nil;
        }

        public RbTreeNode<T> CreateNode(T data, TreeNodeColor color = TreeNodeColor.Black)
        {
            var node = new RbTreeNode<T>(data, color);
            node.RbLeft = node.RbRight = Nil;
            return node;
        }

        public override string ToString()
        {
            return this.GetColoredTreeString(Root as IColoredTreeNode);
        }

        public void SetRoot(RbTreeNode<T> root)
        {
            _root = root;
            _root.RbParent = Nil;
        }
    }
}
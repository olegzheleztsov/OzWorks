using System;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures.Trees
{
    public sealed class RbTree<T> : IBinaryTree
    {
        private readonly Func<T, int> _keySelector;
        private RbTreeNode<T> _root;
        private readonly RbTreeNode<T> _nil;
        
        public RbTree(Func<T, int> keySelector)
        {
            _nil = new RbTreeNode<T>(default);
            _nil.LeftChild = _nil.RightChild = _nil;
            _root = _nil;
            _keySelector = keySelector;
        }
        
        public ITreeNode NullNode => _nil;
        public Func<object, int> KeySelector => obj => _keySelector((T) obj);

        public ITreeNode Root
        {
            get => _root;
            set => _root = value as RbTreeNode<T>;
        }


        public bool IsNull(ITreeNode node)
        {
            return node == _nil;
        }

        public RbTreeNode<T> CreateNode(T data, TreeNodeColor color = TreeNodeColor.Black)
        {
            var node = new RbTreeNode<T>(data, color);
            node.LeftChild = node.RightChild = _nil;
            return node;
        }

        public override string ToString()
        {
            return this.GetColoredTreeString();
        }

        public void SetRoot(RbTreeNode<T> root)
        {
            _root = root;
            _root.SetParent(_nil);
        }

        /// <summary>
        ///     Enumerates nodes which keys between minKey and maxKey
        /// </summary>
        /// <param name="minKey">Min key to enumerate</param>
        /// <param name="maxKey">Max key to enumerate</param>
        /// <param name="rootNode">
        ///     Root node of the subtree where enumeration is started. If the parameter is missed than used
        ///     whole tree root
        /// </param>
        /// <returns>List of nodes that has keys in range [minKey, maxKey]</returns>
        /// <exception cref="ArgumentException">Throws exception when maxKey is less or equal than minKey</exception>
        /// <exception cref="InvalidOperationException">
        ///     Throws when we don't pass rootNode parameter and current tree's Root is
        ///     null
        /// </exception>
        public IEnumerable<RbTreeNode<T>> Enumerate(int minKey, int maxKey, RbTreeNode<T> rootNode = null)
        {
            if (minKey >= maxKey)
            {
                throw new ArgumentException($"minKey should be less than maxKey, minKey: {minKey}, maxKey: {maxKey}");
            }

            rootNode ??= Root as RbTreeNode<T>;

            if (rootNode == null)
            {
                throw new InvalidOperationException("root is null or it is not RbTreeNode<T>");
            }

            var outputList = new List<RbTreeNode<T>>();
            var currentKey = KeySelector(rootNode.Data);
            if (minKey <= currentKey && currentKey <= maxKey)
            {
                outputList.Add(rootNode);
            }

            if (minKey <= currentKey && !IsNull(rootNode.LeftChild))
            {
                outputList.AddRange(Enumerate(minKey, maxKey, rootNode.LeftChild as RbTreeNode<T>));
            }

            if (currentKey <= maxKey && !IsNull(rootNode.RightChild))
            {
                outputList.AddRange(Enumerate(minKey, maxKey, rootNode.RightChild as RbTreeNode<T>));
            }

            return outputList;
        }

        public RbTreeNode<T> FindSuccessor(ITreeNode node)
            => this.Successor(node) as RbTreeNode<T>;

        public RbTreeNode<T> FindPredecessor(ITreeNode node)
            => this.Predecessor(node) as RbTreeNode<T>;

        public ITreeSearcher<RbTreeNode<T>> CreateSearcher()
            => BinaryTreeSearcherFactory.Create<RbTreeNode<T>>(this, node => KeySelector(node.Data), SearchMethod.Recursive);
        
        public ITreeMaximumSearcher<RbTreeNode<T>> CreateMaximumSearcher() 
            => TreeMaximumSearcherFactory.Create<RbTreeNode<T>>(this);
        
        public ITreeMinimumSearcher<RbTreeNode<T>> CreateMinimumSearcher()
            => TreeMinimumSearcherFactory.Create<RbTreeNode<T>>(this, SearchMethod.Recursive);
    }
}
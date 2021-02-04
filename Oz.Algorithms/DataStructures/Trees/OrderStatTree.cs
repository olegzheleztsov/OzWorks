using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class OrderStatTree<T> : IBinaryTree, IEnumerable<OrderStatTreeNode<T>>
    {
        private readonly Func<T, int> _keySelector;
        private OrderStatTreeNode<T> _root;

        public OrderStatTree(Func<T, int> keySelector)
        {
            _keySelector = keySelector;
            Nil = new OrderStatTreeNode<T>(default, TreeNodeColor.Black, true);
            Nil.LeftChild = Nil.RightChild = Nil;
        }

        private OrderStatTreeNode<T> Nil { get; }

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

        public IEnumerator<OrderStatTreeNode<T>> GetEnumerator()
        {
            var nodes = new List<OrderStatTreeNode<T>>();
            var walker = BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Preorder);
            walker.Walk(node => { nodes.Add(node as OrderStatTreeNode<T>); });
            return nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void SetRoot(OrderStatTreeNode<T> root)
        {
            _root = root;
            _root.SetParent(Nil);
        }

        public void UpdateSize()
        {
            (Root as OrderStatTreeNode<T>).UpdateSize();
        }

        public OrderStatTreeNode<T> SelectByRank(ITreeNode parentNode, int index)
        {
            var r = ((OrderStatTreeNode<T>) parentNode.LeftChild).Size + 1;
            if (index == r)
            {
                return parentNode as OrderStatTreeNode<T>;
            }

            if (index < r)
            {
                return SelectByRank((OrderStatTreeNode<T>) parentNode.LeftChild, index);
            }

            return SelectByRank(parentNode.RightChild as OrderStatTreeNode<T>, index - r);
        }

        /// <summary>
        ///     Returns order of node in inorder traveling
        /// </summary>
        /// <param name="node">OzSingleLinkedListNode from tree</param>
        /// <returns>Rank of this node</returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public int Rank(OrderStatTreeNode<T> node)
        {
            var r = ((OrderStatTreeNode<T>) node.LeftChild).Size + 1;
            var y = node;
            while (y != Root)
            {
                if (y == y.ParentNode.RightChild)
                {
                    r += (y.ParentNode.LeftChild as OrderStatTreeNode<T>).Size + 1;
                }

                y = y.ParentNode as OrderStatTreeNode<T>;
            }

            return r;
        }

        public OrderStatTreeNode<T> CreateNode(T data, TreeNodeColor color = TreeNodeColor.Black, bool isNull = false)
        {
            var node = new OrderStatTreeNode<T>(data, color, isNull);
            node.LeftChild = node.RightChild = NullNode as OrderStatTreeNode<T>;
            return node;
        }
    }
}
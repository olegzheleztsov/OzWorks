using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Algorithms.DataStructures
{
    public class SearchBinaryTree<T> : IEnumerable<BinaryTreeNode<T>>
    {
        private readonly Func<T, int> _keySelector;
        private BinaryTreeNode<T> _root;

        public SearchBinaryTree() : this(null, null)
        {
        }

        public SearchBinaryTree(Func<T, int> keySelector) : this(null, keySelector)
        {
        }

        public SearchBinaryTree(BinaryTreeNode<T> root, Func<T, int> keySelector)
        {
            _root = root;
            _keySelector = keySelector;
        }

        public IEnumerator<BinaryTreeNode<T>> GetEnumerator()
        {
            var list = new List<BinaryTreeNode<T>>();
            InorderTreeWalk(data => list.Add(data));
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void InorderTreeWalk(Action<BinaryTreeNode<T>> visitor)
        {
            _InorderTreeWalk(_root, visitor);
        }

        public async Task InorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await _InorderTreeWalkAsync(_root, visitor).ConfigureAwait(false);
        }

        public void PreorderTreeWalk(Action<BinaryTreeNode<T>> visitor)
        {
            _PreorderTreeWalk(_root, visitor);
        }

        public async Task PreorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await _PreorderTreeWalkAsync(_root, visitor).ConfigureAwait(false);
        }

        public void PostorderTreeWalk(Action<BinaryTreeNode<T>> visitor)
        {
            _PostorderTreeWalk(_root, visitor);
        }

        public async Task PostorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await _PostorderTreeWalkAsync(_root, visitor).ConfigureAwait(false);
        }

        public async Task<BinaryTreeNode<T>> SearchAsync(int key, SearchMethod searchMethod = SearchMethod.Recursive)
        {
            return await Task.Run(() => Search(key, searchMethod)).ConfigureAwait(false);
        }

        public BinaryTreeNode<T> Search(int key, SearchMethod searchMethod = SearchMethod.Recursive)
        {
            return searchMethod == SearchMethod.Recursive ? _Search(_root, key) : _SearchIterative(_root, key);
        }

        public BinaryTreeNode<T> Minimum(SearchMethod searchMethod = SearchMethod.Iterative)
        {
            return searchMethod == SearchMethod.Iterative ? _Minimum(_root) : _MinimumRecursive(_root);
        }
        

        public BinaryTreeNode<T> Maximum(SearchMethod searchMethod = SearchMethod.Iterative)
        {
            return searchMethod == SearchMethod.Iterative ? _Maximum(_root) : _MaximumRecursive(_root);
        }

        public BinaryTreeNode<T> Successor(BinaryTreeNode<T> node)
        {
            if (node.Right != null)
            {
                return _Minimum(node.Right);
            }

            var parent = node.Parent;
            while (parent != null && node == parent.Right)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent;
        }

        public BinaryTreeNode<T> Predecessor(BinaryTreeNode<T> node)
        {
            if (node.Left != null)
            {
                return _Maximum(node.Left);
            }

            var parent = node.Parent;
            while (parent != null && node == parent.Left)
            {
                node = parent;
                parent = parent.Parent;
            }

            return parent;
        }

        public void Insert(T data)
        {
            var newNode = new BinaryTreeNode<T>(data);
            BinaryTreeNode<T> changedElement = null;
            var currentElement = _root;
            while (currentElement != null)
            {
                changedElement = currentElement;
                currentElement = _keySelector(newNode.Data) < _keySelector(currentElement.Data)
                    ? currentElement.Left
                    : currentElement.Right;
            }

            newNode.Parent = changedElement;
            if (changedElement == null)
            {
                _root = newNode;
            }
            else if (_keySelector(newNode.Data) < _keySelector(changedElement.Data))
            {
                changedElement.Left = newNode;
            }
            else
            {
                changedElement.Right = newNode;
            }
        }

        public async Task InsertAsync(T data)
        {
            await Task.Run(() => Insert(data)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(BinaryTreeNode<T> nodeToDelete)
        {
            await Task.Run(() => Delete(nodeToDelete)).ConfigureAwait(false);
        }

        public void Delete(BinaryTreeNode<T> nodeToDelete)
        {
            if (nodeToDelete.Left == null)
            {
                Transplant(nodeToDelete, nodeToDelete.Right);
            }
            else if (nodeToDelete.Right == null)
            {
                Transplant(nodeToDelete, nodeToDelete.Left);
            }
            else
            {
                var newNode = _Minimum(nodeToDelete.Right);
                if (newNode.Parent != nodeToDelete)
                {
                    Transplant(newNode, newNode.Right);
                    newNode.Right = nodeToDelete.Right;
                    newNode.Right.Parent = newNode;
                }

                Transplant(nodeToDelete, newNode);
                newNode.Left = nodeToDelete.Left;
                newNode.Left.Parent = newNode;
            }
        }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Root: {_root?.Data?.ToString() ?? "<NONE>"}");
            InorderTreeWalk(data => stringBuilder.Append($"{data.Data?.ToString() ?? "<NONE>"}, "));
            return stringBuilder.ToString();
        }

        private void Transplant(BinaryTreeNode<T> oldNode, BinaryTreeNode<T> newNode)
        {
            if (oldNode.Parent == null)
            {
                _root = newNode;
            }
            else if (oldNode == oldNode.Parent.Left)
            {
                oldNode.Parent.Left = newNode;
            }
            else
            {
                oldNode.Parent.Right = newNode;
            }

            if (newNode != null)
            {
                newNode.Parent = oldNode.Parent;
            }
        }

        private BinaryTreeNode<T> _Maximum(BinaryTreeNode<T> node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        private BinaryTreeNode<T> _MaximumRecursive(BinaryTreeNode<T> node)
        {
            return node.Right == null ? node : _MaximumRecursive(node.Right);
        }

        private BinaryTreeNode<T> _Minimum(BinaryTreeNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        private BinaryTreeNode<T> _MinimumRecursive(BinaryTreeNode<T> node)
        {
            return node.Left == null ? node : _MinimumRecursive(node.Left);
        }

        private BinaryTreeNode<T> _SearchIterative(BinaryTreeNode<T> node, int key)
        {
            while (node != null && key != _keySelector(node.Data))
            {
                node = key < _keySelector(node.Data) ? node.Left : node.Right;
            }

            return node;
        }

        private BinaryTreeNode<T> _Search(BinaryTreeNode<T> node, int key)
        {
            if (node == null || key == _keySelector(node.Data))
            {
                return node;
            }

            return _Search(key < _keySelector(node.Data) ? node.Left : node.Right, key);
        }


        private static void _InorderTreeWalk(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                _InorderTreeWalk(node.Left, visitor);
                visitor?.Invoke(node);
                _InorderTreeWalk(node.Right, visitor);
            }
        }

        private static async Task _InorderTreeWalkAsync(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                await _InorderTreeWalkAsync(node.Left, visitor).ConfigureAwait(false);
                visitor?.Invoke(node);
                await _InorderTreeWalkAsync(node.Right, visitor).ConfigureAwait(false);
            }
        }

        private static void _PreorderTreeWalk(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                visitor?.Invoke(node);
                _PreorderTreeWalk(node.Left, visitor);
                _PreorderTreeWalk(node.Right, visitor);
            }
        }

        private static async Task _PreorderTreeWalkAsync(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                visitor?.Invoke(node);
                await _PreorderTreeWalkAsync(node.Left, visitor).ConfigureAwait(false);
                await _PreorderTreeWalkAsync(node.Right, visitor).ConfigureAwait(false);
            }
        }

        private static void _PostorderTreeWalk(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                _PostorderTreeWalk(node.Left, visitor);
                _PostorderTreeWalk(node.Right, visitor);
                visitor?.Invoke(node);
            }
        }

        private static async Task _PostorderTreeWalkAsync(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> visitor)
        {
            if (node != null)
            {
                await _PostorderTreeWalkAsync(node.Left, visitor).ConfigureAwait(false);
                await _PostorderTreeWalkAsync(node.Right, visitor).ConfigureAwait(false);
                visitor?.Invoke(node);
            }
        }
    }
}
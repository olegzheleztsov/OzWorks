using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Algorithms.DataStructures.Trees
{
    public class SearchBinaryTree<T> : IEnumerable<BinaryTreeNode<T>>, IBinaryTree
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


        public ITreeNode Root
        {
            get => _root;
            set => _root = value as BinaryTreeNode<T>;
        }

        public bool IsNull(ITreeNode node)
        {
            return node == null;
        }

        public ITreeNode NullNode => null;
        public Func<object, int> KeySelector => obj => _keySelector((T) obj);

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
            BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Inorder)
                .Walk(element => visitor?.Invoke(element as BinaryTreeNode<T>));
        }

        public async Task InorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Inorder).WalkAsync(async node =>
            {
                await Task.Run(() => { visitor?.Invoke(node as BinaryTreeNode<T>); });
            }).ConfigureAwait(false);
        }

        public void PreorderTreeWalk(Action<BinaryTreeNode<T>> visitor)
        {
            BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Preorder)
                .Walk(element => visitor?.Invoke(element as BinaryTreeNode<T>));
        }

        public async Task PreorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Preorder).WalkAsync(async element =>
            {
                await Task.Run(() => { visitor?.Invoke(element as BinaryTreeNode<T>); }).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        public void PostorderTreeWalk(Action<BinaryTreeNode<T>> visitor)
        {
            BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Postorder)
                .Walk(element => visitor?.Invoke(element as BinaryTreeNode<T>));
        }

        public async Task PostorderTreeWalkAsync(Action<BinaryTreeNode<T>> visitor)
        {
            await BinaryTreeWalkerFactory.Create(this, TreeWalkStrategy.Postorder).WalkAsync(async element =>
            {
                await Task.Run(() => { visitor?.Invoke(element as BinaryTreeNode<T>); }).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        public async Task<BinaryTreeNode<T>> SearchAsync(int key, SearchMethod searchMethod = SearchMethod.Recursive)
        {
            return await Task.Run(() => Search(key, searchMethod)).ConfigureAwait(false);
        }

        public BinaryTreeNode<T> Search(int key, SearchMethod searchMethod = SearchMethod.Recursive)
        {
            return BinaryTreeSearcherFactory
                    .Create(this, node => _keySelector(((BinaryTreeNode<T>) node).Data), searchMethod).Search(key)
                as BinaryTreeNode<T>;
        }

        public BinaryTreeNode<T> Minimum(SearchMethod searchMethod = SearchMethod.Iterative)
        {
            var searcher = TreeMinimumSearcherFactory.Create<BinaryTreeNode<T>>(this, searchMethod);
            return searcher.Minimum(_root);
        }


        public BinaryTreeNode<T> Maximum(SearchMethod searchMethod = SearchMethod.Iterative)
        {
            return searchMethod == SearchMethod.Iterative ? _Maximum(_root) : _MaximumRecursive(_root);
        }

        public BinaryTreeNode<T> Successor(BinaryTreeNode<T> node)
        {
            if (node.RightChild != null)
            {
                var minimumSearcher =
                    TreeMinimumSearcherFactory.Create<BinaryTreeNode<T>>(this, SearchMethod.Recursive);
                return minimumSearcher.Minimum(node.RightChild as BinaryTreeNode<T>);
            }

            var parent = node.ParentNode;
            while (parent != null && node == parent.RightChild)
            {
                node = parent as BinaryTreeNode<T>;
                parent = parent.ParentNode;
            }

            return parent as BinaryTreeNode<T>;
        }

        public BinaryTreeNode<T> Predecessor(BinaryTreeNode<T> node)
        {
            if (node.LeftChild != null)
            {
                return _Maximum(node.LeftChild as BinaryTreeNode<T>);
            }

            var parent = node.ParentNode;
            while (parent != null && node == parent.LeftChild)
            {
                node = parent as BinaryTreeNode<T>;
                parent = parent.ParentNode;
            }

            return parent as BinaryTreeNode<T>;
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
                    ? currentElement.LeftChild as BinaryTreeNode<T>
                    : currentElement.RightChild as BinaryTreeNode<T>;
            }

            newNode.SetParent(changedElement);
            if (changedElement == null)
            {
                _root = newNode;
            }
            else if (_keySelector(newNode.Data) < _keySelector(changedElement.Data))
            {
                changedElement.LeftChild = newNode;
            }
            else
            {
                changedElement.RightChild = newNode;
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
            if (nodeToDelete.LeftChild == null)
            {
                Transplant(nodeToDelete, nodeToDelete.RightChild as BinaryTreeNode<T>);
            }
            else if (nodeToDelete.RightChild == null)
            {
                Transplant(nodeToDelete, nodeToDelete.LeftChild as BinaryTreeNode<T>);
            }
            else
            {
                var minimumSearcher =
                    TreeMinimumSearcherFactory.Create<BinaryTreeNode<T>>(this, SearchMethod.Recursive);
                var newNode = minimumSearcher.Minimum(nodeToDelete.RightChild as BinaryTreeNode<T>);
                if (newNode.ParentNode != nodeToDelete)
                {
                    Transplant(newNode, newNode.RightChild as BinaryTreeNode<T>);
                    newNode.RightChild = nodeToDelete.RightChild;
                    newNode.RightChild.SetParent(newNode);
                }

                Transplant(nodeToDelete, newNode);
                newNode.LeftChild = nodeToDelete.LeftChild;
                newNode.LeftChild.SetParent(newNode);
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
            if (oldNode.ParentNode == null)
            {
                _root = newNode;
            }
            else if (oldNode == oldNode.ParentNode.LeftChild)
            {
                oldNode.ParentNode.LeftChild = newNode;
            }
            else
            {
                oldNode.ParentNode.RightChild = newNode;
            }

            newNode?.SetParent(oldNode.ParentNode);
        }

        private BinaryTreeNode<T> _Maximum(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }
            while (node?.RightChild != null)
            {
                node = node.RightChild as BinaryTreeNode<T>;
            }

            return node;
        }

        private BinaryTreeNode<T> _MaximumRecursive(BinaryTreeNode<T> node)
        {
            return node.RightChild == null ? node : _MaximumRecursive(node.RightChild as BinaryTreeNode<T>);
        }
    }
}
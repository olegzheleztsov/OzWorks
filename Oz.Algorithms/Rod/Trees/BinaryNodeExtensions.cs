using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Rod.Trees
{
    public static class BinaryNodeExtensions
    {

        public static T FindNode<T, TU>(this IBinaryNode<T, TU> root, TU value, Comparison<TU> comparison) where T : IBinaryNode<T, TU>
        {
            if (comparison(root.Data, value) == 0)
            {
                return (T)root;
            }

            if (root.LeftChild != null)
            {
                var leftSearchResult = root.LeftChild.FindNode(value, comparison);
                if (leftSearchResult != null)
                {
                    return leftSearchResult;
                }
            }

            if (root.RightChild != null)
            {
                var rightSearchResult = root.RightChild.FindNode(value, comparison);
                if (rightSearchResult != null)
                {
                    return rightSearchResult;
                }
            }

            return default;
        }
        public static void TraversePreorder<T>(this BinaryNode<T> node, Action<BinaryNode<T>> visitor)
        {
            visitor?.Invoke(node);
            node.LeftChild?.TraversePreorder(visitor);
            node.RightChild?.TraversePreorder(visitor);
        }

        public static void TraverseInorder<T>(this BinaryNode<T> node, Action<BinaryNode<T>> visitor)
        {
            node.LeftChild?.TraverseInorder(visitor);
            visitor?.Invoke(node);
            node.RightChild?.TraverseInorder(visitor);
        }

        public static void TraversePostorder<T>(this BinaryNode<T> node, Action<BinaryNode<T>> visitor)
        {
            node.LeftChild?.TraversePostorder(visitor);
            node.RightChild?.TraversePostorder(visitor);
            visitor?.Invoke(node);
        }

        public static void TraverseDepthFirst<T>(this BinaryNode<T> node, Action<BinaryNode<T>> visitor)
        {
            var children = new Queue<BinaryNode<T>>();
            children.Enqueue(node);

            while (children.Count > 0)
            {
                var n = children.Dequeue();
                visitor?.Invoke(n);
                if (n.LeftChild != null)
                {
                    children.Enqueue(n.LeftChild);
                }

                if (n.RightChild != null)
                {
                    children.Enqueue(n.RightChild);
                }
            }
        }

        public static void AddSortedTreeNode<T>(this BinaryNode<T> node, T newValue, Comparison<T> comparison)
        {
            if (comparison(newValue, node.Data) < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new BinaryNode<T>(newValue);
                }
                else
                {
                    node.LeftChild.AddSortedTreeNode(newValue, comparison);
                }
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new BinaryNode<T>(newValue);
                }
                else
                {
                    node.RightChild.AddSortedTreeNode(newValue, comparison);
                }
            }
        }

        public static BinaryNode<T> FindSortedTreeNode<T>(this BinaryNode<T> node, T target, Comparison<T> comparison)
        {
            if (comparison(target, node.Data) == 0)
            {
                return node;
            }

            return comparison(target, node.Data) < 0 
                ? node.LeftChild?.FindSortedTreeNode(target, comparison) 
                : node.RightChild?.FindSortedTreeNode(target, comparison);
        }

        public static BinaryNodePair<T> FindSortedTreeNodeWithParent<T>(
            this BinaryNode<T> node, BinaryNode<T> parent, BinaryNode<T> target, Comparison<T> comparison)
        {
            if (target == node)
            {
                return new BinaryNodePair<T>() {Parent = parent, Child = node};
            }

            return comparison(target.Data, node.Data) < 0
                ? node.LeftChild?.FindSortedTreeNodeWithParent(node, target, comparison)
                : node.RightChild?.FindSortedTreeNodeWithParent(node, target, comparison);
        }
        
        public class BinaryNodePair<T>
        {
            public BinaryNode<T> Parent { get; set; }
            public BinaryNode<T> Child { get; set; }
        }

        public static BinaryNode<T> DeleteSortedTreeNode<T>(this BinaryNode<T> node, BinaryNode<T> toDeleteNode, Comparison<T> comparison)
        {
            var pair = node.FindSortedTreeNodeWithParent(null, toDeleteNode, comparison);
            if (pair.Parent == null)
            {
                return toDeleteNode;
            }

            if (toDeleteNode.LeftChild == null && toDeleteNode.RightChild == null)
            {
                if (pair.Parent.LeftChild == toDeleteNode)
                {
                    pair.Parent.LeftChild = null;
                } else if (pair.Parent.RightChild == toDeleteNode)
                {
                    pair.Parent.RightChild = null;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            } else if (toDeleteNode.LeftChild != null && toDeleteNode.RightChild == null)
            {
                if (pair.Parent.LeftChild == toDeleteNode)
                {
                    pair.Parent.LeftChild = toDeleteNode.LeftChild;
                } else if (pair.Parent.RightChild == toDeleteNode)
                {
                    pair.Parent.RightChild = toDeleteNode.LeftChild;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            } else if (toDeleteNode.LeftChild == null && toDeleteNode.RightChild != null)
            {
                if (pair.Parent.LeftChild == toDeleteNode)
                {
                    pair.Parent.LeftChild = toDeleteNode.RightChild;
                } else if (pair.Parent.RightChild == toDeleteNode)
                {
                    pair.Parent.RightChild = toDeleteNode.RightChild;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            } else if (toDeleteNode.LeftChild != null && toDeleteNode.RightChild != null)
            {
                var leftSubtreeParent = toDeleteNode;
                var leftSubtree = toDeleteNode.LeftChild;

                if (leftSubtree.RightChild == null)
                {
                    if (pair.Parent.LeftChild == toDeleteNode)
                    {
                        pair.Parent.LeftChild = leftSubtree;
                        leftSubtree.RightChild = toDeleteNode.RightChild;
                    } else if (pair.Parent.RightChild == toDeleteNode)
                    {
                        pair.Parent.RightChild = leftSubtree;
                        leftSubtree.RightChild = toDeleteNode.RightChild;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    return toDeleteNode;
                }
                
                while (leftSubtree.RightChild != null)
                {
                    leftSubtreeParent = leftSubtree;
                    leftSubtree = leftSubtree.RightChild;
                }

                if (leftSubtree.LeftChild != null)
                {
                    leftSubtreeParent.RightChild = leftSubtree.LeftChild;
                    leftSubtree.LeftChild = null;
                }

                if (pair.Parent.LeftChild == toDeleteNode)
                {
                    pair.Parent.LeftChild = leftSubtree;
                    leftSubtree.LeftChild ??= toDeleteNode.LeftChild;
                    leftSubtree.RightChild = toDeleteNode.RightChild;
                    
                } else if (pair.Parent.RightChild == toDeleteNode)
                {
                    pair.Parent.RightChild = leftSubtree;
                    leftSubtree.LeftChild ??= toDeleteNode.LeftChild;
                    leftSubtree.RightChild = toDeleteNode.RightChild;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return toDeleteNode;
        }


        /// <summary>
        /// Find least common ancestor in sorted tree by top down traverse algorithm
        /// </summary>
        /// <param name="root">Tree root</param>
        /// <param name="value1">First value</param>
        /// <param name="value2">Second value</param>
        /// <param name="comparison">Comparision func</param>
        /// <typeparam name="T">Type of root node data</typeparam>
        /// <returns>Returns LCA or null if not found</returns>
        public static BinaryNode<T> FindLcaSortedTree<T>(this BinaryNode<T> root, T value1, T value2, Comparison<T> comparison)
        {
            if (comparison(value1, root.Data) < 0 && comparison(value2, root.Data) < 0)
            {
                return root.LeftChild?.FindLcaSortedTree(value1, value2, comparison);
            }

            if (comparison(value1, root.Data) > 0 && comparison(value2, root.Data) > 0)
            {
                return root.RightChild?.FindLcaSortedTree(value1, value2, comparison);
            }

            return root;
        }
    }
}
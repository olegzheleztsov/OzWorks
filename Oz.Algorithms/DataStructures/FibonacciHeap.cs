using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     Structure like priority queue with fast amortized operations
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public sealed class FibonacciHeap<T> : IEnumerable<T> where T : IKey
    {
        private OzDoubleCyclicLinkedList<T> _rootList;

        public FibonacciHeap()
        {
            Count = 0;
            Min = null;
        }

        /// <summary>
        ///     Count of the elements in the heap
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Whether the heap is empty
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        ///     Min element of the heap
        /// </summary>
        public FibonacciHeapNode<T> Min { get; private set; }


        /// <summary>
        ///     Auxiliary node degree upper boundary
        /// </summary>
        private int DegreeBoundary
        {
            get
            {
                if (Count > 0)
                {
                    return (int) Math.Floor(Math.Log(Count, 1.61803));
                }

                return 0;
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            if (_rootList != null)
            {
                var dataList = new List<T>();
                foreach (var n in _rootList.EnumerateNodes())
                {
                    var currentNode = (FibonacciHeapNode<T>) n;
                    currentNode.Preorder(node => { dataList.Add(node.Data); }, currentNode);
                }

                foreach (var element in dataList)
                {
                    yield return element;
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Convenient method that allocates fibonacci node for the heap
        /// </summary>
        /// <param name="data">Node data</param>
        /// <returns>New node for the heap</returns>
        private IDoubleLinkedNode<T> FibonacciNodeAllocator(T data)
        {
            var node = new FibonacciHeapNode<T>(data);
            return node;
        }

        /// <summary>
        ///     Insert node to the heap. Works with amortized time O(1)
        /// </summary>
        /// <param name="node">Node to be inserted</param>
        public void Insert(FibonacciHeapNode<T> node)
        {
            node.Degree = 0;
            node.Parent = null;
            node.Child = null;
            node.IsMark = false;
            if (Min == null)
            {
                _rootList = new OzDoubleCyclicLinkedList<T>(FibonacciNodeAllocator);
                _rootList.Insert(node);
                Min = node;
            }
            else
            {
                _rootList.Insert(node);
                if (node.Data.Key < Min.Data.Key)
                {
                    Min = node;
                }
            }

            Count++;
        }


        /// <summary>
        ///     Union two heap and create new one. After this operation old heaps unusable
        /// </summary>
        /// <param name="first">First heap for union</param>
        /// <param name="second">Second heap for union</param>
        /// <returns>New heap that is union of two heaps</returns>
        public static FibonacciHeap<T> Union(FibonacciHeap<T> first, FibonacciHeap<T> second)
        {
            var newHeap = new FibonacciHeap<T>
            {
                Min = first.Min, _rootList = OzDoubleCyclicLinkedList<T>.Concatenate(first._rootList, second._rootList)
            };

            if (first.Min == null || second.Min != null &&
                second.Min.Data.Key < first.Min.Data.Key)
            {
                newHeap.Min = second.Min;
            }

            newHeap.Count = first.Count + second.Count;
            return newHeap;
        }

        /// <summary>
        ///     Returns and delete from heap minimum element. This operation takes O(lg(n)) amortized time
        /// </summary>
        /// <returns>Minimum element of the heap</returns>
        public FibonacciHeapNode<T> ExtractMin()
        {
            var currentNode = Min;
            if (currentNode != null)
            {
                foreach (var child in GetChildren(currentNode))
                {
                    _rootList.Insert(child);
                    child.Parent = null;
                }

                _rootList.Delete(currentNode);
                if (currentNode == currentNode.Next)
                {
                    Min = null;
                    _rootList = null;
                }
                else
                {
                    Min = (FibonacciHeapNode<T>) currentNode.Next;
                    Consolidate();
                }

                Count--;
            }

            return currentNode;
        }

        /// <summary>
        ///     Auxiliary method that change heap structure after min element extracting
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void Consolidate()
        {
            var heapNodes = new List<FibonacciHeapNode<T>>(DegreeBoundary + 1);

            for (var i = 0; i < DegreeBoundary + 1; i++)
            {
                heapNodes.Add(null);
            }

            foreach (var rootNode in _rootList.EnumerateNodes().ToList())
            {
                var currentNode = (FibonacciHeapNode<T>) rootNode;
                var degree = currentNode.Degree;
                EnsureIndexExists(heapNodes, degree);
                while (heapNodes[degree] != null)
                {
                    var candidateNode = heapNodes[degree];
                    if (currentNode.Data.Key > candidateNode.Data.Key)
                    {
                        var temp = candidateNode;
                        candidateNode = currentNode;
                        currentNode = temp;
                    }

                    HeapLink(candidateNode, currentNode);
                    heapNodes[degree] = null;
                    degree++;
                    EnsureIndexExists(heapNodes, degree);
                }

                heapNodes[degree] = currentNode;
            }

            Min = null;
            _rootList = null;
            foreach (var node in heapNodes.Where(node => node != null))
            {
                if (Min == null)
                {
                    _rootList = new OzDoubleCyclicLinkedList<T>(FibonacciNodeAllocator);
                    _rootList.Insert(node);
                    Min = node;
                }
                else
                {
                    if (_rootList == null)
                    {
                        throw new InvalidOperationException();
                    }

                    _rootList.Insert(node);
                    if (node.Data.Key < Min.Data.Key)
                    {
                        Min = node;
                    }
                }
            }
        }

        /// <summary>
        ///     Auxiliary method that change node links
        /// </summary>
        /// <param name="childNode">Child node</param>
        /// <param name="parentNode">Parent node</param>
        private void HeapLink(FibonacciHeapNode<T> childNode, FibonacciHeapNode<T> parentNode)
        {
            _rootList.Delete(childNode);
            childNode.Parent = parentNode;
            parentNode.Child ??= new OzDoubleCyclicLinkedList<T>(FibonacciNodeAllocator);
            parentNode.Child.Insert(childNode);
            childNode.Parent = parentNode;
            parentNode.Degree++;
            childNode.IsMark = false;
        }


        /// <summary>
        ///     Auxiliary method that ensures list always has valid index range
        /// </summary>
        /// <param name="list">List to be checked</param>
        /// <param name="index">Index that should be present in the list</param>
        private static void EnsureIndexExists(ICollection<FibonacciHeapNode<T>> list, int index)
        {
            if (list.Count < index + 1)
            {
                while (list.Count < index + 1)
                {
                    list.Add(null);
                }
            }
        }

        /// <summary>
        ///     Enumerates direct children for the node
        /// </summary>
        /// <param name="parent">Parent node</param>
        /// <returns>Children of the node</returns>
        private static IEnumerable<FibonacciHeapNode<T>> GetChildren(FibonacciHeapNode<T> parent)
        {
            var children = new List<FibonacciHeapNode<T>>();
            if (parent.Child != null && !parent.Child.IsEmpty)
            {
                children.AddRange(parent.Child.EnumerateNodes().Select(n => (FibonacciHeapNode<T>) n));
            }

            return children;
        }

        /// <summary>
        ///     Enumerates root list nodes
        /// </summary>
        /// <returns>Root list nodes</returns>
        public IEnumerable<FibonacciHeapNode<T>> EnumerateRootNodes()
        {
            if (Min == null || _rootList == null || _rootList.IsEmpty)
            {
                yield break;
            }

            foreach (var rootNode in _rootList.EnumerateNodes())
            {
                yield return (FibonacciHeapNode<T>) rootNode;
            }
        }

        /// <summary>
        ///     Return heap as string
        /// </summary>
        /// <returns>String representation of the heap</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"IS_EMPTY: {IsEmpty}, ROOT_NODE_COUNT: {_rootList?.Count ?? 0}");
            if (!IsEmpty)
            {
                if (_rootList != null)
                {
                    foreach (var rootNode in _rootList?.EnumerateNodes())
                    {
                        stringBuilder.AppendLine(rootNode.ToString());
                    }
                }
            }

            stringBuilder.AppendLine("--------------------------------------------------------------");
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Decrease key of one node in the heap. This operation takes O(1) amortized time
        /// </summary>
        /// <param name="targetNode">Target node</param>
        /// <param name="newKey">New key for node</param>
        /// <exception cref="ArgumentException"></exception>
        public void DecreaseKey(FibonacciHeapNode<T> targetNode, int newKey)
        {
            if (newKey > targetNode.Data.Key)
            {
                throw new ArgumentException($"New key: {newKey} i greater than current key: {targetNode.Data.Key}");
            }

            targetNode.Data.SetKey(newKey);
            var parentNode = targetNode.Parent;
            if (parentNode != null && targetNode.Data.Key < parentNode.Data.Key)
            {
                Cut(targetNode, parentNode);
                CascadingCut(parentNode);
            }

            if (targetNode.Data.Key < Min.Data.Key)
            {
                Min = targetNode;
            }
        }

        /// <summary>
        ///     Cut element from its parent
        /// </summary>
        /// <param name="cutNode">Node to be cut</param>
        /// <param name="parentNode">Parent Node</param>
        private void Cut(FibonacciHeapNode<T> cutNode, FibonacciHeapNode<T> parentNode)
        {
            parentNode.Child.Delete(cutNode);
            parentNode.Degree--;
            _rootList.Insert(cutNode);
            cutNode.Parent = null;
            cutNode.IsMark = false;
        }

        /// <summary>
        ///     Cascading cut the node
        /// </summary>
        /// <param name="targetNode">Node to be cutted</param>
        private void CascadingCut(FibonacciHeapNode<T> targetNode)
        {
            var z = targetNode.Parent;
            if (z != null)
            {
                if (targetNode.IsMark == false)
                {
                    targetNode.IsMark = true;
                }
                else
                {
                    Cut(targetNode, z);
                    CascadingCut(z);
                }
            }
        }

        /// <summary>
        ///     Delete node from the heap. This operation takes O(lg(n))
        /// </summary>
        /// <param name="deleteNode"></param>
        /// <returns></returns>
        public FibonacciHeapNode<T> Delete(FibonacciHeapNode<T> deleteNode)
        {
            DecreaseKey(deleteNode, int.MinValue);
            return ExtractMin();
        }

        /// <summary>
        ///     Found nodes in the heap that satisfy to predicate
        /// </summary>
        /// <param name="predicate">Predicate for nodes</param>
        /// <returns>Collection of the satisfied nodes in the heap</returns>
        public IEnumerable<FibonacciHeapNode<T>> Find(Func<T, bool> predicate)
        {
            if (_rootList != null)
            {
                var nodes = new List<FibonacciHeapNode<T>>();
                foreach (var n in _rootList.EnumerateNodes())
                {
                    var currentNode = n;
                    ((FibonacciHeapNode<T>) currentNode).Preorder(node =>
                        {
                            if (predicate(node.Data))
                            {
                                nodes.Add(node);
                            }
                        }, currentNode as FibonacciHeapNode<T>
                    );
                }

                return nodes;
            }

            return new List<FibonacciHeapNode<T>>();
        }
    }
}
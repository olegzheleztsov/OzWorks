using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    public class FibonacciHeap<T>
    {
        private readonly Func<T, int> _keySelector;

        private OzDoubleCyclicLinkedList<T> _rootList;

        public FibonacciHeap(Func<T, int> keySelector)
        {
            _keySelector = keySelector;
            Count = 0;
            Min = null;
        }

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public FibonacciHeapNode<T> Min { get; private set; }

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

        private IDoubleLinkedNode<T> FibonacciNodeAllocator(T data)
        {
            var node = new FibonacciHeapNode<T>(data);
            return node;
        }

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
                if (_keySelector(node.Data) < _keySelector(Min.Data))
                {
                    Min = node;
                }
            }

            Count++;
        }


        public static FibonacciHeap<T> Union(FibonacciHeap<T> first, FibonacciHeap<T> second)
        {
            var newHeap = new FibonacciHeap<T>(first._keySelector)
            {
                Min = first.Min,
                _rootList = OzDoubleCyclicLinkedList<T>.Concatenate(first._rootList, second._rootList)
            };

            if (first.Min == null || second.Min != null &&
                second._keySelector(second.Min.Data) < first._keySelector(first.Min.Data))
            {
                newHeap.Min = second.Min;
            }

            newHeap.Count = first.Count + second.Count;
            return newHeap;
        }

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
                    if (_keySelector(currentNode.Data) > _keySelector(candidateNode.Data))
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
                    if (_keySelector(node.Data) < _keySelector(Min.Data))
                    {
                        Min = node;
                    }
                }
            }
        }

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


        private static void EnsureIndexExists(List<FibonacciHeapNode<T>> list, int index)
        {
            if (list.Count < index + 1)
            {
                while (list.Count < index + 1)
                {
                    list.Add(null);
                }
            }
        }

        private static IEnumerable<FibonacciHeapNode<T>> GetChildren(FibonacciHeapNode<T> parent)
        {
            var children = new List<FibonacciHeapNode<T>>();
            if (parent.Child != null && !parent.Child.IsEmpty)
            {
                children.AddRange(parent.Child.EnumerateNodes().Select(n => (FibonacciHeapNode<T>) n));
            }

            return children;
        }

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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"IS_EMPTY: {IsEmpty}, ROOT_NODE_COUNT: {_rootList?.Count ?? 0}");
            if (!IsEmpty)
            {
                foreach (var rootNode in _rootList.EnumerateNodes())
                {
                    stringBuilder.AppendLine(rootNode.ToString());
                }
            }

            stringBuilder.AppendLine("--------------------------------------------------------------");
            return stringBuilder.ToString();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.Algorithms.Rod
{
    public class BinomialHeap<T> : IEnumerable<T>
    {
        private Comparison<T> Comparison { get; }
        private BinomialNode<T> Sentinel { get; set; } = new(default);

        public BinomialHeap(Comparison<T> comparison)
        {
            Comparison = comparison;
        }

        public bool IsEmpty => Sentinel.NextSibling == null;

        public void Enqueue(T value)
        {
            if (Sentinel.NextSibling == null)
            {
                Sentinel.NextSibling = new BinomialNode<T>(value);
            }
            else
            {
                var newHeap = new BinomialHeap<T>(Comparison);
                newHeap.Enqueue(value);
                Sentinel = MergeHeapLists(this, newHeap).Sentinel;
                MergeRootsWithSameOrder(this);
            }
        }

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("empty");
            }
            var prev = FindRootBeforeSmallestValue();
            var root = prev.NextSibling;
            prev.NextSibling = root.NextSibling;

            var newHeap = new BinomialHeap<T>(Comparison);
            var subTree = root.FirstChild;
            while (subTree != null)
            {
                var next = subTree.NextSibling;
                subTree.NextSibling = newHeap.Sentinel.NextSibling;
                newHeap.Sentinel.NextSibling = subTree;
                subTree = next;
            }

            Sentinel = MergeHeapLists(this, newHeap).Sentinel;
            MergeRootsWithSameOrder(this);
            return root.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty)
            {
                yield break;
            }
            
            var list = new List<T>();
            Sentinel.NextSibling.Visit((data, level) =>
            {
                list.Add(data);
            });
            foreach (var element in list)
            {
                yield return element;
            }
        }

        public override string ToString()
        {
            if (IsEmpty)
            {
                return "{}";
            }

            var leveledNodes = new Dictionary<int, List<BinomialNode<T>>>();
            Sentinel.NextSibling.Visit((BinomialNode<T> node, int level) =>
            {
                if (leveledNodes.ContainsKey(level))
                {
                    leveledNodes[level].Add(node);
                }
                else
                {
                    leveledNodes.Add(level, new List<BinomialNode<T>> {node});
                }
            });
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("{");
            foreach (var (key, value) in leveledNodes.OrderBy(p => p.Key))
            {
                stringBuilder.AppendLine($"\t{key}: [{string.Join(", ", value.Select(v => v.ToString()))}]");
            }

            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private BinomialNode<T> FindRootBeforeSmallestValue()
        {
            if (IsEmpty)
            {
                return null;
            }

            BinomialNode<T> beforeSmallest = Sentinel;
            var smallestNode = Sentinel.NextSibling;
            var prevCurrent = smallestNode;
            var current = smallestNode.NextSibling;
            while (current != null)
            {
                if (Comparison(current.Data, smallestNode.Data) < 0)
                {
                    beforeSmallest = prevCurrent;
                    smallestNode = current;
                }

                prevCurrent = current;
                current = current.NextSibling;
            }
            return beforeSmallest;
        }
        
        

        private static  BinomialHeap<T> MergeHeapLists(BinomialHeap<T> heap1, BinomialHeap<T> heap2)
        {
            var resultHeap = new BinomialHeap<T>(heap1.Comparison);
            var mergedListSentinel = resultHeap.Sentinel;
            var mergedListBottom = mergedListSentinel;

            heap1.Sentinel = heap1.Sentinel.NextSibling;
            heap2.Sentinel = heap2.Sentinel.NextSibling;

            while (heap1.Sentinel != null && heap2.Sentinel != null)
            {
                BinomialHeap<T> moveHeap = null;
                moveHeap = heap1.Sentinel.Order <= heap2.Sentinel.Order ? heap1 : heap2;

                var moveRoot = moveHeap.Sentinel;
                moveHeap.Sentinel = moveRoot.NextSibling;
                mergedListBottom.NextSibling = moveRoot;
                mergedListBottom = moveRoot;
                mergedListBottom.NextSibling = null;
            }

            if (heap1.Sentinel != null)
            {
                mergedListBottom.NextSibling = heap1.Sentinel;
                heap1.Sentinel = null;
            }
            else
            {
                mergedListBottom.NextSibling = heap2.Sentinel;
                heap2.Sentinel = null;
            }

            return resultHeap;
        }

        private static void MergeRootsWithSameOrder(BinomialHeap<T> heap)
        {
            var prev = heap.Sentinel;
            var node = prev.NextSibling;
            var next = node?.NextSibling;

            while (next != null)
            {
                if (node.Order != next.Order)
                {
                    prev = node;
                    node = next;
                    next = next.NextSibling;
                }
                else
                {
                    prev.NextSibling = next.NextSibling;
                    node = BinomialNodeExtensions.MergeTrees(node, next, heap.Comparison);
                    next = prev.NextSibling;
                    node.NextSibling = next;
                    prev.NextSibling = node;
                    if (next != null && node.Order == next.Order && next.NextSibling != null &&
                        node.Order == next.NextSibling.Order)
                    {
                        prev = node;
                        node = next;
                        next = next.NextSibling;
                    }
                }
            }
        }
    }
}
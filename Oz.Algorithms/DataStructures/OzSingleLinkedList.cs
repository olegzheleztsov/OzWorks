#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     Single linked list with pointers to head and tail
    /// </summary>
    /// <typeparam name="T">Type of node's data</typeparam>
    public class OzSingleLinkedList<T> : IOzLinkedList<T>, IEnumerable<T>
    {
        /// <summary>
        ///     Head node value (T default if list is empty)
        /// </summary>
        public T HeadValue => HeadNode != null ? HeadNode.Data : default;

        public bool HasCircle
            => GetStartCircleNode() != null;


        /// <summary>
        ///     Enumerates list elements
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = HeadNode;
            while (current != TailNode)
            {
                yield return current.Data;
                current = current.Next;
            }

            yield return current.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Is list empty?
        /// </summary>
        public bool IsEmpty => HeadNode == null;

        /// <summary>
        ///     Count of elements in the list
        /// </summary>
        public int Count
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }

                var current = HeadNode;
                var count = 0;

                while (current != TailNode)
                {
                    count++;
                    current = current.Next;
                }

                count++;


                return count;
            }
        }

        public IListNode<T> HeadNode { get; protected set; }

        public IListNode<T> TailNode { get; protected set; }

        public bool IsLast(OzSingleLinkedListNode<T> node)
        {
            return node == TailNode;
        }

        /// <summary>
        ///     Search first node that conform to condition
        /// </summary>
        /// <param name="condition">Search condition</param>
        /// <returns>List node</returns>
        public OzSingleLinkedListNode<T> Search(Func<T, bool> condition)
        {
            if (HeadNode == null)
            {
                return null;
            }

            if (condition(TailNode.Data))
            {
                return (OzSingleLinkedListNode<T>) TailNode;
            }

            var found = false;
            var current = HeadNode;
            while (current != TailNode)
            {
                if (!condition(current.Data))
                {
                    current = current.Next;
                }
                else
                {
                    found = true;
                    break;
                }
            }

            return found ? (OzSingleLinkedListNode<T>) current : null;
        }

        protected (OzSingleLinkedListNode<T> previousNode, OzSingleLinkedListNode<T> foundNode) SearchNode(
            Func<T, bool> condition)
        {
            if (HeadNode == null)
            {
                return (null, null);
            }

            OzSingleLinkedListNode<T> previousNode;
            if (condition(TailNode.Data))
            {
                previousNode = GetCellBefore((OzSingleLinkedListNode<T>) TailNode);
                return (previousNode, (OzSingleLinkedListNode<T>) TailNode);
            }

            var found = false;
            previousNode = null;
            var current = HeadNode;
            while (current != TailNode)
            {
                if (!condition(current.Data))
                {
                    previousNode = (OzSingleLinkedListNode<T>) current;
                    current = current.Next;
                }
                else
                {
                    found = true;
                    break;
                }
            }

            return found ? (previousNode, (OzSingleLinkedListNode<T>) current) : (null, null);
        }

        public bool Contains(Func<T, bool> condition)
        {
            return Search(condition) != null;
        }

        /// <summary>
        ///     Delete first node that conforms to condition
        /// </summary>
        /// <param name="condition"></param>
        public void Delete(Func<T, bool> condition)
        {
            var node = Search(condition);
            if (node != null)
            {
                Delete(node);
            }
        }

        public void Clear()
        {
            while (!IsEmpty)
            {
                Delete(_ => true);
            }
        }

        /// <summary>
        ///     Delete head of the list
        /// </summary>
        public void DeleteHead()
        {
            Delete((OzSingleLinkedListNode<T>) HeadNode);
        }

        /// <summary>
        ///     Insert new node into head position (from list start)
        /// </summary>
        /// <param name="data">OzDoubleLinkedListNode's data</param>
        public void InsertFirst(T data)
        {
            var newNode = new OzSingleLinkedListNode<T>(data) {Next = HeadNode};
            HeadNode = newNode;

            if (newNode.Next == null)
            {
                TailNode = newNode;
            }
        }

        /// <summary>
        ///     Insert node into list end
        /// </summary>
        /// <param name="data">OzDoubleLinkedListNode's data</param>
        public void InsertLast(T data)
        {
            if (TailNode == null)
            {
                InsertFirst(data);
            }
            else
            {
                var newNode = new OzSingleLinkedListNode<T>(data);
                TailNode.Next = newNode;
                TailNode = newNode;
            }
        }

        public void InsertLastRange(IEnumerable<T> datas)
        {
            foreach (var data in datas)
            {
                InsertLast(data);
            }
        }


        public void InsertFirstRange(IEnumerable<T> datas)
        {
            foreach (var data in datas)
            {
                InsertFirst(data);
            }
        }

        /// <summary>
        ///     Delete node from list by node reference
        /// </summary>
        /// <param name="node">OzDoubleLinkedListNode to delete</param>
        public void Delete(OzSingleLinkedListNode<T> node)
        {
            if (TailNode.Next != null)
            {
                throw new InvalidOperationException("Deletion on cycle lists is not implemented");
            }

            if (node == HeadNode)
            {
                if (HeadNode == TailNode)
                {
                    HeadNode = null;
                    TailNode = null;
                }
                else
                {
                    HeadNode = HeadNode.Next;
                }
            }
            else
            {
                var current = HeadNode;
                OzSingleLinkedListNode<T> previous = null;
                while (current != null && current != node)
                {
                    previous = (OzSingleLinkedListNode<T>) current;
                    current = current.Next;
                }

                if (previous != null && current != null)
                {
                    previous.Next = current.Next;
                    current.Next = null;
                    if (current == TailNode)
                    {
                        TailNode = previous;
                    }
                }
            }
        }

        public IEnumerable<OzSingleLinkedListNode<T>> EnumerateNodes()
        {
            var current = HeadNode;
            while (current != TailNode)
            {
                yield return (OzSingleLinkedListNode<T>) current;
                current = current.Next;
            }

            yield return (OzSingleLinkedListNode<T>) current;
        }

        public OzSingleLinkedListNode<T> GetStartCircleNode()
        {
            return (HeadNode as OzSingleLinkedListNode<T>)?.GetCircleStart();
        }

        protected OzSingleLinkedListNode<T> GetCellBefore(OzSingleLinkedListNode<T> targetCell)
        {
            var centinel = new OzSingleLinkedListNode<T> {Next = HeadNode};
            var pointer = centinel;

            while (pointer.Next != null)
            {
                if (pointer.Next == targetCell)
                {
                    centinel.Next = null;
                    return pointer != centinel ? pointer : null;
                }

                pointer = (OzSingleLinkedListNode<T>) pointer.Next;
            }

            return null;
        }

        public IListNode<T> GetAtIndex(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            var pointer = HeadNode;
            var currentIndex = 0;
            while (currentIndex < index)
            {
                pointer = pointer.Next;
                currentIndex++;
            }

            return pointer;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", this)}]";
        }

        /// <summary>
        ///     Find max value in list. Works only for integral data values in the list
        /// </summary>
        /// <returns>Max value node</returns>
        public OzSingleLinkedListNode<T> Max()
        {
            if (IsEmpty)
            {
                return null;
            }

            if (HeadNode == TailNode)
            {
                return (OzSingleLinkedListNode<T>) HeadNode;
            }

            var pointer = HeadNode;
            var maxNode = pointer;
            dynamic maxValue = pointer.Data;
            pointer = pointer.Next;

            while (pointer != TailNode)
            {
                dynamic testValue = pointer.Data;
                if (testValue > maxValue)
                {
                    maxValue = testValue;
                    maxNode = pointer;
                }

                pointer = pointer.Next;
            }

            dynamic tailValue = pointer.Data;
            if (tailValue > maxValue)
            {
                maxNode = pointer;
            }

            return (OzSingleLinkedListNode<T>) maxNode;
        }

        public void InsertionSort(Comparison<T> comparison)
        {
            var oldSentinel = new OzSingleLinkedListNode<T> {Next = HeadNode};

            var newSentinel = new OzSingleLinkedListNode<T> {Next = null};

            oldSentinel = (OzSingleLinkedListNode<T>) oldSentinel.Next;

            while (oldSentinel != null)
            {
                var nextCell = oldSentinel;
                oldSentinel = (OzSingleLinkedListNode<T>) oldSentinel.Next;
                var nodeAfterMe = newSentinel;
                while (nodeAfterMe.Next != null && comparison(nodeAfterMe.Next.Data, nextCell.Data) < 0)
                {
                    nodeAfterMe = (OzSingleLinkedListNode<T>) nodeAfterMe.Next;
                }

                nextCell.Next = nodeAfterMe.Next;
                nodeAfterMe.Next = nextCell;
            }

            HeadNode = newSentinel.Next;
            var pointer = HeadNode;
            if (pointer == null)
            {
                TailNode = null;
            }
            else
            {
                while (pointer.Next != null)
                {
                    pointer = pointer.Next;
                }

                TailNode = pointer;
            }

            newSentinel.Next = null;
        }

        public void SelectionSort(Comparison<T> comparison)
        {
            var oldSentinel = new OzSingleLinkedListNode<T> {Next = HeadNode};

            var newSentinel = new OzSingleLinkedListNode<T> {Next = null};

            while (oldSentinel.Next != null)
            {
                var nodeBestAfterMe = oldSentinel;
                var bestValue = nodeBestAfterMe.Next.Data;

                var nodeAfterMe = oldSentinel.Next;
                while (nodeAfterMe.Next != null)
                {
                    if (comparison(nodeAfterMe.Next.Data, bestValue) > 0)
                    {
                        nodeBestAfterMe = (OzSingleLinkedListNode<T>) nodeAfterMe;
                        bestValue = nodeAfterMe.Next.Data;
                    }

                    nodeAfterMe = nodeAfterMe.Next;
                }

                var nodeBest = nodeBestAfterMe.Next;
                nodeBestAfterMe.Next = nodeBest.Next;

                nodeBest.Next = newSentinel.Next;
                newSentinel.Next = nodeBest;
            }

            HeadNode = newSentinel.Next;
            var pointer = HeadNode;
            if (pointer == null)
            {
                TailNode = null;
            }
            else
            {
                while (pointer.Next != null)
                {
                    pointer = pointer.Next;
                }

                TailNode = pointer;
            }

            newSentinel.Next = null;
        }
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleCyclicLinkedList<T> : IEnumerable<T>
    {
        private readonly IDoubleLinkedNode<T> _nil;
        private readonly Func<T, IDoubleLinkedNode<T>> _allocate;

        public OzDoubleCyclicLinkedList(Func<T, IDoubleLinkedNode<T>> allocate)
        {
            _allocate = allocate;
            _nil = allocate(default);
            _nil.Next = _nil;
            _nil.Prev = _nil;
        }

        public bool IsEmpty => _nil.Next == _nil.Prev && _nil.Next == _nil;

        public int Count
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }

                var count = 0;
                var n = _nil;
                while (n.Next != _nil)
                {
                    count++;
                    n = n.Next;
                }

                return count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _nil.Next;
            while (current != _nil)
            {
                if (!IsNull(current))
                {
                    yield return current.Data;
                }

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IDoubleLinkedNode<T>> EnumerateNodes()
        {
            var current = _nil.Next;
            while (current != _nil)
            {
                yield return current;
                current = current.Next;
            }
        }

        public IDoubleLinkedNode<T> Search(Func<T, bool> condition)
        {
            var current = _nil.Next;
            while (current != _nil && !condition(current.Data))
            {
                current = current.Next;
            }

            return current;
        }

        public void Delete(Func<T, bool> condition)
        {
            var toDelete = Search(condition);
            if (toDelete != _nil)
            {
                Delete(toDelete);
            }
        }

        public OzDoubleCyclicLinkedList<T> Insert(IDoubleLinkedNode<T> newNode)
        {
            newNode.Next = _nil.Next;
            _nil.Next.Prev = newNode;
            _nil.Next = newNode;
            newNode.Prev = _nil;
            return this;
        }

        public OzDoubleCyclicLinkedList<T> Insert(params T[] dataArray)
        {
            if (dataArray != null)
            {
                foreach (var data in dataArray)
                {
                    Insert(data);
                }
            }

            return this;
        }

        public OzDoubleCyclicLinkedList<T> Insert(T data)
        {
            var allocatedNode = _allocate(data);
            var newNode = allocatedNode;
            return Insert(newNode);
        }

        public void Delete(IDoubleLinkedNode<T> node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }

        public bool IsNull(IDoubleLinkedNode<T> node)
        {
            return node == _nil;
        }

        public IDoubleLinkedNode<T> Next(IDoubleLinkedNode<T> node)
        {
            if (IsEmpty)
            {
                return null;
            }

            var nextNode = node.Next;
            if (IsNull(nextNode))
            {
                return nextNode.Next;
            }

            return nextNode;
        }

        public IDoubleLinkedNode<T> Prev(IDoubleLinkedNode<T> node)
        {
            if (IsEmpty)
            {
                return null;
            }

            var prevNode = node.Prev;
            return IsNull(prevNode) ? prevNode.Prev : prevNode;
        }


        public static OzDoubleCyclicLinkedList<T> Concatenate(OzDoubleCyclicLinkedList<T> first,
            OzDoubleCyclicLinkedList<T> second)
        {
            if (first == null && second == null)
            {
                return null;
            }

            Func<T, IDoubleLinkedNode<T>> allocate = null;
            allocate = first != null ? first._allocate : second._allocate;

            var newList = new OzDoubleCyclicLinkedList<T>(allocate);
            if (first == null)
            {
                MoveList(second, newList);
                return newList;
            }

            if (second == null)
            {
                MoveList(first, newList);
                return newList;
            }

            
            MoveList(first, newList);
            MoveList(second, newList);
            return newList;
        }

        private static void MoveList(OzDoubleCyclicLinkedList<T> source, OzDoubleCyclicLinkedList<T> target)
        {
            while (!source.IsEmpty)
            {
                var element = source.Search(_ => true);
                MoveNode(source, target, element);
            }
        }
        private static void MoveNode(OzDoubleCyclicLinkedList<T> source, OzDoubleCyclicLinkedList<T> target,
            IDoubleLinkedNode<T> node)
        {
            source.Delete(node);
            target.Insert(node);
        }
    }
}
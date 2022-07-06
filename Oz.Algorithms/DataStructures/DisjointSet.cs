using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Oz.Algorithms.DataStructures
{
    public class DisjointSet<T> : IEnumerable<DisjointSetElement<T>>
    {
        private readonly Func<T, T, bool> _comparer;
        private DisjointSetElement<T> _head;

        private DisjointSetElement<T> _tail;

        private DisjointSet(Func<T, T, bool> comparer)
        {
            _comparer = comparer;
            Count = 0;
        }

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public IEnumerator<DisjointSetElement<T>> GetEnumerator()
        {
            if (IsEmpty)
            {
                yield break;
            }

            var current = _head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void UnionWith(DisjointSet<T> secondSet)
        {
            var secondCount = secondSet.Count;
            if(secondCount == 0)
            {
                return;
            }

            var current = secondSet._head;

            while (current != null)
            {
                current.OwnerSet = this;
                current = current.Next;
            }

            if (IsEmpty)
            {
                _head = secondSet._head;
                _tail = secondSet._tail;
            }
            else
            {
                _tail.Next = secondSet._head;
                _tail = secondSet._tail;
            }

            secondSet.Clear();
            Count += secondCount;
        }

        private void Clear()
        {
            _head = _tail = null;
            Count = 0;
        }

        private void Add(T data)
        {
            var newElement = new DisjointSetElement<T>(data, this);
            if (IsEmpty)
            {
                _head = newElement;
            }
            else
            {
                _tail.Next = newElement;
                newElement.Next = null;
            }

            _tail = newElement;
            Count++;
        }

        public DisjointSetElement<T> Find(Func<T, bool> condition)
        {
            if (IsEmpty)
            {
                return null;
            }

            var current = _head;
            while (current != null)
            {
                if (condition(current.Data))
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }

        public DisjointSetElement<T> Find(T data)
        {
            return Find((element) => element.Equals(data));
        }

        public bool Delete(DisjointSetElement<T> element)
        {
            if (IsEmpty)
            {
                return false;
            }

            if (_head == _tail)
            {
                if (_tail == element)
                {
                    _head = _tail = null;
                    Count = 0;
                    return true;
                }

                return false;
            }

            if (_head == element)
            {
                _head = _head.Next;
                Count--;
                return true;
            }


            var previousElement = _head;
            var currentElement = _head.Next;

            while (currentElement != null)
            {
                if (currentElement == element)
                {
                    previousElement.Next = currentElement.Next;
                    if (currentElement.Next == null)
                    {
                        _tail = previousElement;
                    }

                    Count--;
                    return true;
                }

                previousElement = currentElement;
                currentElement = currentElement.Next;
            }

            return false;
        }

        public bool Delete(T data)
        {
            if (IsEmpty)
            {
                return false;
            }

            if (_head == _tail)
            {
                if (_comparer(_head.Data, data))
                {
                    _head = _tail = null;
                    Count = 0;
                    return true;
                }

                return false;
            }

            if (_comparer(_head.Data, data))
            {
                var prevHead = _head;
                _head = _head.Next;
                prevHead.Next = null;
                Count--;
                return true;
            }

            var previousElement = _head;
            var currentElement = _head.Next;
            while (currentElement != null)
            {
                if (_comparer(currentElement.Data, data))
                {
                    previousElement.Next = currentElement.Next;
                    if (currentElement.Next == null)
                    {
                        _tail = previousElement;
                    }

                    Count--;
                    return true;
                }

                previousElement = currentElement;
                currentElement = currentElement.Next;
            }

            return false;
        }

        public IEnumerable<T> Enumerate() =>
            this.Select(element => element.Data);

        public static DisjointSet<T> Empty(Func<T, T, bool> comparer) =>
            new DisjointSet<T>(comparer);

        public static DisjointSet<T> MakeSet(T data, Func<T, T, bool> comparer)
        {
            var emptySet = Empty(comparer);
            emptySet.Add(data);
            return emptySet;
        }

        public static DisjointSet<T> MakeSet(IEnumerable<T> dataCollection, Func<T, T, bool> comparer)
        {
            var result = Empty(comparer);
            foreach (var data in dataCollection)
            {
                result.Add(data);
            }

            return result;
        }

        public static DisjointSet<T> Union(DisjointSet<T> firstSet, DisjointSet<T> secondSet)
        {
            if (firstSet.Count > secondSet.Count)
            {
                firstSet.UnionWith(secondSet);
                return firstSet;
            }

            secondSet.UnionWith(firstSet);
            return secondSet;
        }

        public static DisjointSet<T> FindSet(DisjointSetElement<T> element) =>
            element.OwnerSet;
    }
}
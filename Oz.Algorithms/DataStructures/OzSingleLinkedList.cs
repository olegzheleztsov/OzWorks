using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    /// <summary>
    ///     Single linked list with pointers to head and tail
    /// </summary>
    /// <typeparam name="T">Type of node's data</typeparam>
    public class OzSingleLinkedList<T> : IEnumerable<T>
    {
        private OzSingleLinkedListNode<T> _head;
        private OzSingleLinkedListNode<T> _last;

        /// <summary>
        ///     Head node value (T default if list is empty)
        /// </summary>
        public T HeadValue => _head != null ? _head.Data : default;

        /// <summary>
        ///     Is list empty?
        /// </summary>
        public bool IsEmpty => _head == null;

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

                var current = _head;
                var count = 1;

                while (current.Next != null)
                {
                    count++;
                    current = current.Next;
                }

                return count;
            }
        }


        /// <summary>
        ///     Enumerates list elements
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsLast(OzSingleLinkedListNode<T> node)
        {
            return node == _last;
        }

        /// <summary>
        ///     Search first node that conform to condition
        /// </summary>
        /// <param name="condition">Search condition</param>
        /// <returns>List node</returns>
        public OzSingleLinkedListNode<T> Search(Func<T, bool> condition)
        {
            var found = false;
            var current = _head;
            while (current != null)
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

            return found ? current : null;
        }

        public bool Contains(Func<T, bool> condition)
        {
            return Search(condition) != null;
        }

        /// <summary>
        ///     Delete's first node that conforms to condition
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
                Delete(obj => true);
            }
        }

        /// <summary>
        ///     Delete head of the list
        /// </summary>
        public void DeleteHead()
        {
            Delete(_head);
        }

        /// <summary>
        ///     Insert new node into head position (from list start)
        /// </summary>
        /// <param name="data">Node's data</param>
        public void InsertFirst(T data)
        {
            var newNode = new OzSingleLinkedListNode<T>(data) {Next = _head};
            _head = newNode;

            if (newNode.Next == null)
            {
                _last = newNode;
            }
        }

        /// <summary>
        ///     Insert node into list end
        /// </summary>
        /// <param name="data">Node's data</param>
        public void InsertLast(T data)
        {
            if (_last == null)
            {
                InsertFirst(data);
            }
            else
            {
                var newNode = new OzSingleLinkedListNode<T>(data);
                _last.Next = newNode;
                _last = newNode;
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
        ///     Delete's node from list by node reference
        /// </summary>
        /// <param name="node">Node to delete</param>
        public void Delete(OzSingleLinkedListNode<T> node)
        {
            if (node == _head)
            {
                if (_head == _last)
                {
                    _head = null;
                    _last = null;
                }
                else
                {
                    _head = _head.Next;
                }
            }
            else
            {
                var current = _head;
                OzSingleLinkedListNode<T> previous = null;
                while (current != null && current != node)
                {
                    previous = current;
                    current = current.Next;
                }

                if (previous != null && current != null)
                {
                    previous.Next = current.Next;
                    current.Next = null;
                }
            }
        }

        public IEnumerable<OzSingleLinkedListNode<T>> EnumerateNodes()
        {
            var current = _head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
    }
}
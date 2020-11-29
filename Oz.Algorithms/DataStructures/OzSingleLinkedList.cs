using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzSingleLinkedList<T> : IEnumerable<T>
    {
        private Node _head;
        private Node _last;
        
        public Node Search(Func<T, bool> condition)
        {
            var current = _head;
            while (current != null && !condition(current.Data))
            {
                current = current.Next;
            }

            return current;
        }

        public void Delete(Func<T, bool> condition)
        {
            var node = Search(condition);
            if (node != null)
            {
                Delete(node);
            }
        }

        public void DeleteHead()
        {
            Delete(_head);
        }

        public T HeadValue => _head != null ? _head.Data : default;

        public bool IsEmpty => _head == null;

        public void Insert(T data)
        {
            var newNode = new Node(data) {Next = _head};
            _head = newNode;
            
            if (newNode.Next == null)
            {
                _last = newNode;
            }
        }

        public void InsertLast(T data)
        {
            
            if (_last == null)
            {
                Insert(data);
            }
            else
            {
                var newNode = new Node(data);
                _last.Next = newNode;
                _last = newNode;
            }
        }

        public void Delete(Node node)
        {
            if (node == _head)
            {
                _head = _head.Next;
            }
            else
            {
                var current = _head;
                Node previous = null;
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

        public class Node
        {
            public Node() : this(default)
            {
            }

            public Node(T data)
            {
                Data = data;
            }

            public Node Next { get; set; }
            public T Data { get; }
        }

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
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleLinkedList<T> : IEnumerable<T>
    {
        private Node _head;

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

        public void Insert(T data)
        {
            var newNode = new Node(data) {Next = _head};
            if (_head != null)
            {
                _head.Prev = newNode;
            }

            _head = newNode;
            newNode.Prev = null;
        }

        public void Delete(Node node)
        {
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                _head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
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
            public Node Prev { get; set; }
            public T Data { get; }
        }
    }
}
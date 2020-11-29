using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleCyclicLinkedList<T> : IEnumerable<T>
    {
        private readonly Node _nil = new Node(default);

        public OzDoubleCyclicLinkedList()
        {
            _nil.Next = _nil;
            _nil.Prev = _nil;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _nil.Next;
            while (current != _nil)
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

        public void Insert(T data)
        {
            var newNode = new Node(data) {Next = _nil.Next};
            _nil.Next.Prev = newNode;
            _nil.Next = newNode;
            newNode.Prev = _nil;
        }

        public void Delete(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
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
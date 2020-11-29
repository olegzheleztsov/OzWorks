using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzSingleLinkedListBasedQueue<T> : IEnumerable<T>
    {
        private OzSingleLinkedList<T> _linkedList = new OzSingleLinkedList<T>();

        public void Enqueue(T data)
        {
            _linkedList.InsertLast(data);
        }

        public T Dequeue()
        {
            if (_linkedList.IsEmpty)
            {
                throw new IndexOutOfRangeException();
            }

            var headValue = _linkedList.HeadValue;
            _linkedList.DeleteHead();
            return headValue;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return this.GetStringRepresentation();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzLinkedListBasedStack<T> : IEnumerable<T>
    {
        private readonly OzSingleLinkedList<T> _linkedList = new OzSingleLinkedList<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _linkedList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(T value)
        {
            _linkedList.InsertFirst(value);
        }

        public bool IsEmpty => _linkedList.IsEmpty;
        public bool NonEmpty => !IsEmpty;

        public T Pop()
        {
            if (_linkedList.IsEmpty)
            {
                throw new IndexOutOfRangeException();
            }

            var value = _linkedList.HeadValue;
            _linkedList.DeleteHead();
            return value;
        }

        public override string ToString()
        {
            return this.GetStringRepresentation();
        }
    }
}
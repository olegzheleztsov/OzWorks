#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    public class OzStack<T> : IStack<T>
    {
        private readonly T[] _array;
        private int _top;

        public OzStack(int capacity)
        {
            _array = new T[capacity];
            _top = -1;
        }

        public bool IsEmpty => _top == -1;

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i <= _top; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(T value)
        {
            _top++;
            if (_top >= _array.Length)
            {
                throw new IndexOutOfRangeException(nameof(_top));
            }

            _array[_top] = value;
        }

        public T Peek() 
            => _array[_top];

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException(nameof(_top));
            }

            _top--;
            return _array[_top + 1];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[ ");
            foreach (var element in this)
            {
                stringBuilder.Append($"{element}, ");
            }

            if (!IsEmpty)
            {
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append(" ]");
            return stringBuilder.ToString();
        }
    }
}
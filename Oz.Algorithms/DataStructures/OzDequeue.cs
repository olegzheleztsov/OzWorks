using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Oz.Algorithms.DataStructures
{
    public class OzDequeue<T> : IEnumerable<T>
    {
        private readonly T[] _array;
        private int _head;
        private int _tail;

        public OzDequeue(int capacity)
        {
            _array = new T[capacity + 1];
            _head = _tail = 0;
        }

        public bool IsFull => _head == _tail + 1 || (_tail == _array.Length - 1 && _head == 0);
        public bool IsEmpty => _head == _tail;

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty)
            {
                yield break;
            }

            if (_tail > _head)
            {
                for (var index = _head; index < _tail; index++)
                {
                    yield return _array[index];
                }
            }
            else
            {
                for (var index = _head; index < _array.Length; index++)
                {
                    yield return _array[index];
                }

                for (var index = 0; index < _tail; index++)
                {
                    yield return _array[index];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ThrowsWhenFull(string propertyName)
        {
            if (IsFull)
            {
                throw new IndexOutOfRangeException(propertyName);
            }
        }

        private void ThrowsWhenEmpty(string propertyName)
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException(propertyName);
            }
        }

        public void EnqueueRight(T element)
        {
            ThrowsWhenFull(nameof(_tail));
            
            _array[_tail] = element;
            if (_tail == _array.Length - 1)
            {
                _tail = 0;
            }
            else
            {
                _tail++;
            }
        }

        public void EnqueueLeft(T element)
        {
            ThrowsWhenFull(nameof(_head));
            if (_head == 0)
            {
                _head = _array.Length - 1;
            }
            else
            {
                _head--;
            }

            _array[_head] = element;
        }

        public T DequeueLeft()
        {
            ThrowsWhenEmpty(nameof(_head));
            var element = _array[_head];
            if (_head == _array.Length - 1)
            {
                _head = 0;
            }
            else
            {
                _head++;
            }

            return element;
        }

        public T DequeueRight()
        {
            ThrowsWhenEmpty(nameof(_tail));

            if (_tail == 0)
            {
                _tail = _array.Length - 1;
            }
            else
            {
                _tail--;
            }

            return _array[_tail];
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
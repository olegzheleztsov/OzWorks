using System;
using System.Collections;
using System.Collections.Generic;

namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleStack<T> : IEnumerable<T>
    {
        private readonly T[] _array;
        private int _topLeft;
        private int _topRight;

        public OzDoubleStack(int capacity)
        {
            _array = new T[capacity];
            _topLeft = -1;
            _topRight = capacity;
        }

        public bool IsLeftEmpty => _topLeft == -1;
        public bool IsRightEmpty => _topRight == _array.Length;

        public bool IsFull => _topLeft >= (_topRight - 1);

        private bool IsAllowInsertLeft
            => !IsFull && _topLeft < _array.Length - 1;

        private bool IsAllowInsertRight
            => !IsFull && _topRight > 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i <= _topLeft; i++)
            {
                yield return _array[i];
            }

            for (var i = _array.Length - 1; i >= _topRight; i--)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void PushLeft(T value)
        {
            if (IsAllowInsertLeft)
            {
                _topLeft++;
                _array[_topLeft] = value;
            }
            else
            {
                throw new IndexOutOfRangeException(
                    $"Not allow insert left, top left: {_topLeft}, top right: {_topRight}, array length: {_array.Length}");
            }
        }

        public void PushRight(T value)
        {
            if (IsAllowInsertRight)
            {
                _topRight--;
                _array[_topRight] = value;
            }
            else
            {
                throw new IndexOutOfRangeException(
                    $"Not allow insert right, top left: {_topLeft}, top right: {_topRight}, array length: {_array.Length}");
            }
        }

        public T PopLeft()
        {
            if (IsLeftEmpty)
            {
                throw new IndexOutOfRangeException(nameof(_topLeft));
            }

            _topLeft--;
            return _array[_topLeft + 1];
        }

        public T PopRight()
        {
            if (IsRightEmpty)
            {
                throw new IndexOutOfRangeException(nameof(_topRight));
            }

            _topRight++;
            return _array[_topRight - 1];
        }
    }
}
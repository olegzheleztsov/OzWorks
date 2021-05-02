#nullable enable

#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Oz.Algorithms.DataStructures
{
    public class OzLinkedListBasedStack<T> : IStack<T>
    {
        private readonly OzSingleLinkedList<T> _linkedList = new();
        public bool NonEmpty => !IsEmpty;

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

        public T Peek()
        {
            if (_linkedList.IsEmpty)
            {
                throw new InvalidOperationException("empty");
            }

            return _linkedList.GetAtIndex(0).Data;
        }

        public bool IsEmpty 
            => _linkedList.IsEmpty;

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

        public void InsertionSort(Comparison<T> comparison)
        {
            var sortedCount = 0;
            OzLinkedListBasedStack<T> auxStack = new();
            var stackCount = _linkedList.Count;
            
            while (!IsEmpty)
            {
                auxStack.Push(Pop());
            }

            while (sortedCount < stackCount)
            {
                var val = auxStack.Pop();
                var poppedCount = 0;
                while (!IsEmpty && comparison(Peek(), val) < 0)
                {
                    auxStack.Push(Pop());
                    poppedCount++;
                }
                Push(val);
                while (poppedCount > 0)
                {
                    Push(auxStack.Pop());
                    poppedCount--;
                }

                sortedCount++;
            }
        }

        public void SelectionSort(Comparison<T> comparision)
        {
            var sortedCount = 0;
            var stackCount = _linkedList.Count;
            

            if (stackCount <= 1)
            {
                return;
            }
            
            var auxStack = new OzLinkedListBasedStack<T>();
            while (sortedCount < stackCount)
            {
                T maxValue = Pop();
                int count = stackCount - 1;
                while (count > sortedCount)
                {
                    if (comparision(Peek(), maxValue) > 0)
                    {
                        auxStack.Push(maxValue);
                        maxValue = Pop();
                    }
                    else
                    {
                        auxStack.Push(Pop());
                    }
                    count--;
                }
                Push(maxValue);
                while (!auxStack.IsEmpty)
                {
                    Push(auxStack.Pop());
                }

                sortedCount++;
            }
        }
    }
}
#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Oz.Algorithms.DataStructures
{
    public class OzDoubleLinkedList<T> : IOzLinkedList<T>, IEnumerable<T>
    {
        public bool IsEmpty => HeadNode == null;

        public int Count
        {
            get
            {
                var count = 0;
                if (IsEmpty)
                {
                    return count;
                }

                var pointer = HeadNode;
                while (pointer != TailNode)
                {
                    count++;
                    pointer = pointer.Next;
                }

                if (TailNode != null)
                {
                    count++;
                }

                return count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = HeadNode;
            while (current != TailNode)
            {
                yield return current.Data;
                current = current.Next;
            }

            if (TailNode != null)
            {
                yield return TailNode.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IListNode<T> HeadNode { get; protected set; }
        public IListNode<T> TailNode { get; protected set; }

        public OzDoubleLinkedListNode<T> Search(Func<T, bool> condition)
        {
            var current = HeadNode;
            while (current != null && !condition(current.Data))
            {
                current = current.Next;
            }

            return current as OzDoubleLinkedListNode<T>;
        }

        public void Delete(Func<T, bool> condition)
        {
            var node = Search(condition);
            if (node != null)
            {
                Delete(node);
            }
        }

        public void InsertFirst(T data)
        {
            var newNode = new OzDoubleLinkedListNode<T>(data) {Next = HeadNode};
            if (HeadNode != null)
            {
                ((OzDoubleLinkedListNode<T>) HeadNode).Prev = newNode;
            }

            if (HeadNode == null)
            {
                TailNode = newNode;
            }

            HeadNode = newNode;
            newNode.Prev = null;
        }

        public void InsertFirstRange(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                InsertFirst(element);
            }
        }

        public void InsertSorted(T data, Func<T, T, int> comparision)
        {
            if (IsEmpty)
            {
                var node = new OzDoubleLinkedListNode<T>(data);
                HeadNode = node;
                TailNode = node;
            }
            else
            {
                var pointer = HeadNode;
                while (pointer != null && comparision(data, pointer.Data) > 0)
                {
                    pointer = pointer.Next;
                }

                var newNode = new OzDoubleLinkedListNode<T>(data);
                if (pointer == null)
                {
                    TailNode.Next = newNode;
                    newNode.Prev = TailNode;
                    TailNode = newNode;
                }
                else
                {
                    if (!(pointer is OzDoubleLinkedListNode<T> typedPointer))
                    {
                        throw new InvalidOperationException();
                    }

                    if (typedPointer.Prev != null)
                    {
                        typedPointer.Prev.Next = newNode;
                        newNode.Prev = typedPointer.Prev;
                        newNode.Next = pointer;
                        typedPointer.Prev = newNode;
                    }
                    else
                    {
                        typedPointer.Prev = newNode;
                        newNode.Next = pointer;
                        if (pointer == HeadNode)
                        {
                            HeadNode = newNode;
                        }
                    }
                }
            }
        }

        public void InsertLast(T data)
        {
            if (HeadNode == null)
            {
                HeadNode = new OzDoubleLinkedListNode<T>(data);
                TailNode = HeadNode;
            }
            else
            {
                var newNode = new OzDoubleLinkedListNode<T>(data) {Prev = TailNode, Next = null};
                TailNode.Next = newNode;
                TailNode = newNode;
            }
        }

        public void InsertLastRange(IEnumerable<T> elements)
        {
            foreach (var element in elements)
            {
                InsertLast(element);
            }
        }

        public void Clear()
        {
            while (!IsEmpty)
            {
                Delete(_ => true);
            }
        }


        public void Delete(OzDoubleLinkedListNode<T> ozDoubleLinkedListNode)
        {
            if (ozDoubleLinkedListNode == TailNode)
            {
                TailNode = ozDoubleLinkedListNode.Prev;
            }

            if (ozDoubleLinkedListNode.Prev != null)
            {
                ozDoubleLinkedListNode.Prev.Next = ozDoubleLinkedListNode.Next;
            }
            else
            {
                HeadNode = ozDoubleLinkedListNode.Next;
            }

            if (ozDoubleLinkedListNode.Next != null)
            {
                ((OzDoubleLinkedListNode<T>) ozDoubleLinkedListNode.Next).Prev = ozDoubleLinkedListNode.Prev;
            }
        }

        public override string ToString()
        {
            return $"[ {string.Join(", ", this)} ]";
        }
    }
}
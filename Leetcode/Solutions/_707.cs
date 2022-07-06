// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _707
{
    public class MyLinkedList
    {
        private int _count;

        private Node _head;

        public int Get(int index)
        {
            if (_count == 0)
            {
                return -1;
            }

            if (index < 0 || index >= _count)
            {
                return -1;
            }

            var ind = 0;
            var p = _head;
            while (ind < index)
            {
                p = p.Next;
                ind++;
            }

            return p.Val;
        }

        public void AddAtHead(int val)
        {
            if (_count == 0)
            {
                _head = new Node
                {
                    Val = val
                };
            }
            else
            {
                var newNode = new Node
                {
                    Val = val,
                    Next = _head
                };
                _head = newNode;
            }

            _count++;
        }

        public void AddAtTail(int val)
        {
            if (_count == 0)
            {
                _head = new Node
                {
                    Val = val
                };
            }
            else
            {
                var p = _head;
                while (p.Next != null)
                {
                    p = p.Next;
                }

                p.Next = new Node
                {
                    Val = val
                };
            }

            _count++;
        }

        public void AddAtIndex(int index, int val)
        {
            if (_count == 0 && index == 0)
            {
                AddAtTail(val);
                return;
            }

            if (index < 0 || index > _count)
            {
                return;
            }

            if (index == 0)
            {
                AddAtHead(val);
                return;
            }

            if (index == _count)
            {
                AddAtTail(val);
                return;
            }

            var p = _head;
            var ind = 0;
            while (ind < index)
            {
                if (p == null)
                {
                    p = _head;
                }
                else
                {
                    p = p.Next;
                }

                ind++;
            }

            var pnext = p.Next;
            var n = new Node
            {
                Val = val
            };
            p.Next = n;
            n.Next = pnext;
            _count++;
        }

        public void DeleteAtIndex(int index)
        {
            if (_count == 0)
            {
                return;
            }

            if (index < 0 || index >= _count)
            {
                return;
            }

            var p = _head;
            var ind = index;
            Node pPrev = null;
            while (ind < index)
            {
                pPrev = p;
                p = p.Next;
                ind++;
            }

            if (pPrev == null)
            {
                _head = _head.Next;
            }
            else
            {
                pPrev.Next = p.Next;
            }

            _count--;
        }

        public class Node
        {
            public Node Next;
            public int Val;
        }
    }
}
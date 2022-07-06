// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _622
{
    public class MyCircularQueue
    {

        private int[] _values;
        private int _count = 0;
        private int _next = 0;
        private int _last = 0;
        
        public MyCircularQueue(int k)
        {
            _values = new int[k];
            
        }
    
        public bool EnQueue(int value) {
            if (_count == _values.Length)
            {
                return false;
            }

            _values[_next] = value;
            _next = (_next + 1) % _values.Length;
            _count++;
            return true;
        }
    
        public bool DeQueue() {
            if (_count == 0)
            {
                return false;
            }

            var value = _values[_last];
            _last = (_last + 1) % _values.Length;
            _count--;
            return true;
        }
    
        public int Front() {
            if (_count == 0)
            {
                return -1;
            }

            return _values[_last];
        }
    
        public int Rear() {
            if (_count == 0)
            {
                return -1;
            }

            var prev = _next - 1;
            if (prev < 0)
            {
                prev += _values.Length;
            }

            return _values[prev];
        }
    
        public bool IsEmpty()
        {
            return _count == 0;
        }
    
        public bool IsFull()
        {
            return _count == _values.Length;
        }
    }
}
using System.Collections.Generic;

namespace Oz.LeetCode.QueueStacks
{
    public class MyQueue
    {

        private readonly Stack<int> _backTop = new Stack<int>();
        private readonly Stack<int> _frontTop = new Stack<int>();
        
        /** Initialize your data structure here. */
        public MyQueue() {
        
        }
    
        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            _backTop.Push(x);
        }
    
        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            if (_frontTop.Count > 0)
            {
                return _frontTop.Pop();
            }

            while (_backTop.Count > 0)
            {
                _frontTop.Push(_backTop.Pop());
            }

            return _frontTop.Pop();
        }
    
        /** Get the front element. */
        public int Peek() {
            if (_frontTop.Count > 0)
            {
                return _frontTop.Peek();
            }

            while (_backTop.Count > 0)
            {
                _frontTop.Push(_backTop.Pop());
            }

            return _frontTop.Peek();
        }
    
        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            return _backTop.Count + _frontTop.Count == 0;
        }
    }
    
    
}
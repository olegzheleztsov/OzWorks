using System.Collections.Generic;

namespace Oz.LeetCode.QueueStacks
{
    public class MyStack
    {

        private readonly Queue<int> _firstQueue;
        private readonly Queue<int> _secondQueue;
        private Queue<int> _endQueue;
        
        /** Initialize your data structure here. */
        public MyStack() {
            _firstQueue = new Queue<int>(); 
            _secondQueue = new Queue<int>();
            _endQueue = _firstQueue;
        }
    
        /** Push element x onto stack. */
        public void Push(int x) {
            if (_endQueue.Count == 0)
            {
                _endQueue.Enqueue(x);
            }
            else
            {
                while (_endQueue.Count > 0)
                {
                    NonEndQueue.Enqueue(_endQueue.Dequeue());
                }
                _endQueue.Enqueue(x);
            }
        }
    
        /** Removes the element on top of the stack and returns that element. */
        public int Pop()
        {
            int x = _endQueue.Dequeue();
            while (NonEndQueue.Count > 1)
            {
                _endQueue.Enqueue(NonEndQueue.Dequeue());                
            }

            _endQueue = NonEndQueue;
            return x;
        }

        private Queue<int> NonEndQueue
            => _endQueue == _firstQueue ? _secondQueue : _firstQueue;

        /** Get the top element. */
        public int Top()
        {
            return _endQueue.Peek();
        }
    
        /** Returns whether the stack is empty. */
        public bool Empty()
        {
            return _endQueue.Count == 0 && NonEndQueue.Count == 0;
        }
    }
}
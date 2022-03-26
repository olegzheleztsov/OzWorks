// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet232
{
    public class MyQueue
    {

        private readonly Stack<int> _pushStack = new Stack<int>();
        private readonly Stack<int> _popStack = new Stack<int>();
        
        public void Push(int x) {
            _pushStack.Push(x);
        }
    
        public int Pop() {
            if (_popStack.Count == 0)
            {
                while (_pushStack.Count > 0)
                {
                    _popStack.Push(_pushStack.Pop());
                }
            }

            return _popStack.Pop();
        }
    
        public int Peek() {
            if (_popStack.Count == 0)
            {
                while (_pushStack.Count > 0)
                {
                    _popStack.Push(_pushStack.Pop());
                }
            }

            return _popStack.Peek();
        }
    
        public bool Empty()
        {
            return (_pushStack.Count + _popStack.Count) == 0;
        }
    }
}
// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _155
{
    public class MinStack
    {
        private readonly Stack<(int Top, int Min)> _stack = new();

        public void Push(int val) =>
            _stack.Push((val,
                _stack.Count == 0 ? val : _stack.Peek().Min > val ? val : _stack.Peek().Min));

        public void Pop() =>
            _stack.Pop();

        public int Top() =>
            _stack.Peek().Top;

        public int GetMin() =>
            _stack.Peek().Min;
    }
}
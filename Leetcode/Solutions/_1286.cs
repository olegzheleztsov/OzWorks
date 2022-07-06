// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1286
{
    public class CombinationIterator
    {
        private readonly Queue<string> _queue;
        private readonly string _characters;

        public CombinationIterator(string characters, int combinationLength)
        {
            _characters = characters;
            _queue = new Queue<string>();

            Find("", 0, combinationLength);
        }

        public string Next()
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            return string.Empty;
        }

        public bool HasNext() => _queue.Count > 0;

        private void Find(string str, int index, int length)
        {
            if (length == 0)
            {
                _queue.Enqueue(str);
                return;
            }

            for (int i = index; i < _characters.Length; i++)
            {
                Find(str + _characters[i], i + 1, length - 1);
            }
        }
    }
}
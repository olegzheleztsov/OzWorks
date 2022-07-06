// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Leetcode.Models;

namespace Leetcode.Solutions;

public class _173
{
    public class BSTIterator {
        
        private readonly TreeNode _root;

        private Stack<TreeNode> _stack = new Stack<TreeNode>();

        private TreeNode _current;

        private bool _started = false;

        public BSTIterator(TreeNode root)
        {
            _root = root;
            _current = root;
        }
    
        public int Next()
        {
            _started = true;

            while (_current != null)
            {
                while (_current != null)
                {
                    _stack.Push(_current);
                    _current = _current.left;
                }
            }
            _current = _stack.Pop();
            int result = _current.val;
            _current = _current.right;
            while (_current != null)
            {
                while (_current != null)
                {
                    _stack.Push(_current);
                    _current = _current.left;
                }
            }
            return result;
        }
    
        public bool HasNext()
        {
            return _stack.Count > 0 || !_started;
        }
    }

}
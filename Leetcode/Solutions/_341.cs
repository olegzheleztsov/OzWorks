// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _341
{
    public class TestNestedInteger : NestedInteger
    {
        private List<NestedInteger> _list;
        private int? _value;

        public TestNestedInteger(int? value, List<NestedInteger> list)
        {
            _value = value;
            _list = list;
        }

        public bool IsInteger() =>
            _value != null;

        public int GetInteger() =>
            _value.Value;

        public IList<NestedInteger> GetList() =>
            _list;
    }
    public interface NestedInteger
    {
        bool IsInteger();
        int GetInteger();
        IList<NestedInteger> GetList();
    }
    
    public class NestedIterator
    {

        private List<int> _flattenedList;
        private int _current = -1;
        
        public NestedIterator(IList<NestedInteger> nestedList)
        {
            _flattenedList = new List<int>();
            var tempList = new List<int>();
            foreach (var root in nestedList)
            {
                AggregateValues(root, tempList);
                if (tempList.Count > 0)
                {
                    _flattenedList.AddRange(tempList);
                    tempList.Clear();
                }
            }
        }

        private void AggregateValues(NestedInteger root, List<int> valuesList)
        {
            if (root.IsInteger())
            {
                valuesList.Add(root.GetInteger());
            }
            else
            {
                foreach (var child in root.GetList())
                {
                    AggregateValues(child, valuesList);
                }
            }
        }

        public bool HasNext()
        {
            return _current + 1 < _flattenedList.Count;
        }

        public int Next()
        {
            _current++;
            return _flattenedList[_current];
        }
    }
    
}
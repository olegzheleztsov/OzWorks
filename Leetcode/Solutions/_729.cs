// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _729
{
    public class MyCalendar
    {

        private List<Interval> _intervals = new List<Interval>();

        public MyCalendar() {
        
        }
    
        public bool Book(int start, int end)
        {
            if (_intervals.Count == 0)
            {
                _intervals.Add(new Interval(){ Start = start, End = end});
                return true;
            }
            
            int index = -1;
            for (int i = 0; i < _intervals.Count; i++)
            {
                if (_intervals[i].Start >= start)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                if (_intervals[_intervals.Count - 1].End <= start)
                {
                    _intervals.Add(new Interval(){Start = start, End = end});
                    return true;
                }

                return false;
            }

            if (index == 0)
            {
                if (end <= _intervals[0].Start)
                {
                    _intervals.Insert(0, new Interval(){Start = start, End = end});
                    return true;
                }
            }

            if (end <= _intervals[index].Start && start >= _intervals[index-1].End)
            {
                _intervals.Insert(index, new Interval(){Start = start, End = end});
                return true;
            }

            return false;
        }
        
        public struct Interval
        {
            public int Start;
            public int End;
        }
    }
}
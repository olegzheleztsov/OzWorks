// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _1825
{
    public class MKAverage
    {
        private int counter;
        private readonly int k;

        private readonly List<int> left;
        private readonly int m;
        private readonly List<int> middle;
        private readonly Queue<int> range;
        private readonly List<int> right;

        private int sum;
        private readonly int sz;

        public MKAverage(int m, int k)
        {
            this.m = m;
            this.k = k;
            sz = m - (2 * k);
            counter = 0;

            left = new List<int>();
            middle = new List<int>();
            right = new List<int>();
            range = new Queue<int>();
        }

        private void Insert(List<int> list, int item)
        {
            var index = list.BinarySearch(item);

            if (index < 0)
            {
                index = ~index;
            }

            list.Insert(index, item);
        }

        private void Add(int num)
        {
            int mov;

            Remove();

            Insert(left, num);

            if (left.Count > k)
            {
                mov = left[^1];

                Insert(middle, mov);
                sum += mov;
                TakeOut(left, mov);
            }

            if (middle.Count > sz)
            {
                mov = middle[^1];

                Insert(right, mov);
                sum -= mov;
                TakeOut(middle, mov);
            }

            range.Enqueue(num);
        }

        private bool TakeOut(List<int> list, int item)
        {
            var index = list.BinarySearch(item);

            if (index >= 0)
            {
                list.RemoveAt(index);

                return true;
            }

            return false;
        }

        private void Remove()
        {
            int mov;

            while (range.Count >= m)
            {
                mov = range.Dequeue();

                if (TakeOut(left, mov))
                {
                    mov = middle[0];
                    sum -= mov;
                    Insert(left, mov);
                    TakeOut(middle, mov);

                    mov = right[0];
                    sum += mov;
                    Insert(middle, mov);
                    TakeOut(right, mov);
                }
                else if (TakeOut(middle, mov))
                {
                    sum -= mov;

                    mov = right[0];
                    sum += mov;
                    Insert(middle, mov);
                    TakeOut(right, mov);
                }
                else
                {
                    TakeOut(right, mov);
                }
            }
        }

        public void AddElement(int num) =>
            Add(num);

        public int CalculateMKAverage()
        {
            if (range.Count < m || m < 1)
            {
                return -1;
            }

            var avg = 0;

            if (sz > 0)
            {
                avg = sum / sz;
            }

            return avg;
        }
    }
}
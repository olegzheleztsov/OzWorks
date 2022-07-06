using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

var leet1855 = new Leet1855();
leet1855.Test();

public class Leet187
{
    public IList<string> FindRepeatedDnaSequences(string s)
    {
        HashSet<string> discoveredSeqs = new HashSet<string>();

        HashSet<string> result = new HashSet<string>();
        for (int i = 0; i < s.Length - 9; i++)
        {
            string sub = s.Substring(i, 10);
            if (discoveredSeqs.Contains(sub) && !result.Contains(sub))
            {
                result.Add(sub); 
            }
            else
            {
                discoveredSeqs.Add(sub);
            }
        }

        return result.ToList();
    }
}

public class Leet43
{
    public string Multiply(string num1, string num2)
    {
        if (num1 == null || num1 == string.Empty || num2 == null || num2 == string.Empty)
            return string.Empty;
            
        if (num1 == "0" || num2 == "0")
            return "0";

        char[,] temp = new char[num2.Length, num1.Length + num2.Length];
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < temp.GetLength(0); i++)
        for (int j = 0; j < temp.GetLength(1); j++)
            temp[i, j] = '0';

        int x = 0,
            y = 0,
            offset = 0;

        for (int i = num2.Length - 1; i > -1; i--)
        {
            int k = 0;
            y = temp.GetLength(1) - 1 - offset;

            for (int j = num1.Length - 1; j > -1; j--)
            {
                int l = (num2[i] - '0') * (num1[j] - '0');

                l += k;
                k = l / 10;
                l %= 10;

                temp[x, y--] = (char)(l + '0');
            }

            if (k != 0)
                temp[x, y] = (char)(k + '0');

            x++;
            offset++;
        }

        int p = 0;

        for (int i = temp.GetLength(1) - 1; i > -1; i--)
        {
            int l = 0;

            for (int j = 0; j < temp.GetLength(0); j++)
                l += temp[j, i] - '0';

            l += p;
            p = l / 10;
            l %= 10;

            sb.Append(l.ToString());
        }

        int len = sb.Length - 1;

        while (sb[len] == '0')
        {
            sb.Remove(len, 1);
            len--;
        }

        return new string(sb.ToString().Reverse().ToArray());
    }
}
public class Leet49
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        List<IList<string>> results = new List<IList<string>>();
        Dictionary<string, IList<string>> groups = new Dictionary<string, IList<string>>();

        foreach (string s in strs)
        {
            var orderedStr = new string(s.OrderBy(c => c).ToArray());
            if (groups.ContainsKey(orderedStr))
            {
                groups[orderedStr].Add(s);
            }
            else
            {
                groups.Add(orderedStr, new List<string>(){s});
            }
        }

        return groups.Values.ToList();
    }
}
public class Leet409
{
    public int LongestPalindrome(string s)
    {

        Dictionary<char, int> freq = new Dictionary<char, int>();
        bool wasOdd = false;

        foreach (char c in s)
        {
            if (freq.ContainsKey(c))
            {
                freq[c]++;
            }
            else
            {
                freq.Add(c, 1);
            }
        }

        int len = 0;
        foreach (var (c, cnt) in freq)
        {
            if (cnt % 2 != 0)
            {
                wasOdd = true;
            }

            if (cnt > 1)
            {
                len += (cnt % 2 == 0) ? cnt : (cnt - 1);
            }
        }

        if (wasOdd)
        {
            len++;
        }

        return len;
    }
}
public class Leet415
{
    public string AddStrings(string num1, string num2)
    {
        char[] num1Arr = num1.ToCharArray();
        char[] num2Arr = num2.ToCharArray();

        var (first, second) = num1Arr.Length > num2.Length ? (num1Arr, num2Arr) : (num2Arr, num1Arr);

        int firstIndex = first.Length - 1;
        int secondIndex = second.Length - 1;
        StringBuilder result = new StringBuilder();

        int memo = 0;
        
        while (secondIndex > -1 || firstIndex > -1)
        {
            int cur = Char2Num(secondIndex > -1 ? second[secondIndex] : '0') + Char2Num(first[firstIndex]) + memo;
            memo = 0;
            if (cur < 10)
            {
                result.Insert(0, cur.ToString());
            }
            else
            {
                memo = 1;
                result.Insert(0, (cur % 10).ToString());
            }

            secondIndex--;
            firstIndex--;
        }
        
        if (memo > 0)
        {
            result.Insert(0, memo.ToString());
        }

        return result.ToString();

        int Char2Num(char c) => (int)(c - '0');
    }
}

public class Leet560
{
    public int SubarraySum(int[] nums, int k)
    {
        int rightSum = 0;
        int result = 0;
        var leftSumFreq = new Dictionary<int, int>() {[0] = 1};

        foreach (var num in nums)
        {
            rightSum += num;
            if (leftSumFreq.TryGetValue(rightSum - k, out var freq))
            {
                result += freq;
            }

            leftSumFreq[rightSum] = leftSumFreq.GetValueOrDefault(rightSum, 0) + 1;
        }

        return result;
    }
}


public class Leet238
{
    public int[] ProductExceptSelf(int[] nums) 
    {
        if (nums == null || nums.Length == 0)
        {
            return null;
        }

        int[] result = new int[nums.Length];
        int temp = 1;

        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = temp;
            temp *= nums[i];
        }

        temp = 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            result[i] *= temp;
            temp *= nums[i];
        }

        return result;
    }
}
public class Leet334
{
    public bool IncreasingTriplet(int[] nums)
    {
        if (nums.Length < 3)
        {
            return false;
        }

        int? minIndex = null;
        int? midIndex = null;

        for (int i = 0; i < nums.Length; i++)
        {
            var currentNumber = nums[i];
            if (midIndex != null && currentNumber > nums[midIndex.Value])
            {
                return true;
            }

            if (minIndex == null || currentNumber < nums[minIndex.Value])
            {
                minIndex = i;
            }

            if (currentNumber > nums[minIndex.Value])
            {
                midIndex = i;
            }
        }

        return false;
    }
}
public class Leet435
{
    public int EraseOverlapIntervals(int[][] intervals) 
    {
        if (intervals.Length == 0)
        {
            return 0;
        }    
        Array.Sort(intervals, (i1, i2) =>
        {
            var cmpEnd = i1[1].CompareTo(i2[1]);
            if (cmpEnd != 0)
            {
                return cmpEnd;
            }

            return i1[0].CompareTo(i2[0]);
        });

        int i = 0;
        int j = 1;
        int res = 0;
        while (i < intervals.Length)
        {
            int to = intervals[i][1];
            while (j < intervals.Length && intervals[j][0] < to)
            {
                j++;
                res++;
            }

            i = j;
            j = i + 1;
        }

        return res;
    }
}
public class Leet240
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        int currentRow = 0;
        int currentCol = matrix[0].Length - 1;

        while (currentRow <= matrix.Length - 1 && currentCol >= 0)
        {
            if (matrix[currentRow][currentCol] == target)
            {
                return true;
            } else if (matrix[currentRow][currentCol] < target)
            {
                currentRow++;
            } else if (matrix[currentRow][currentCol] > target)
            {
                currentCol--;
            }
        }

        return false;
    }
}

public class Leet59
{
    public int[][] GenerateMatrix(int n)
    {

        int rMin = 0;
        int rMax = n - 1;
        int cMin = 0;
        int cMax = n - 1;

        int counter = 1;
        Direction dir = Direction.Right;
        int[][] matrix = new int[n][];
        for (int i = 0; i < n; i++)
        {
            matrix[i] = new int[n];
        }
        
        while (counter <= n * n)
        {
            switch (dir)
            {
                case Direction.Right:
                {
                    for (int i = cMin; i <= cMax; i++)
                    {
                        matrix[rMin][i] = counter++;
                    }

                    rMin++;
                    dir = Direction.Down;
                }
                    break;
                case Direction.Down:
                {
                    for (int i = rMin; i <= rMax; i++)
                    {
                        matrix[i][cMax] = counter++;
                    }

                    cMax--;
                    dir = Direction.Left;
                }
                    break;
                case Direction.Left:
                {
                    for (int i = cMax; i >= cMin; i--)
                    {
                        matrix[rMax][i] = counter++;
                    }

                    rMax--;
                    dir = Direction.Up;
                }
                    break;
                case Direction.Up:
                {
                    for (int i = rMax; i >= rMin; i--)
                    {
                        matrix[i][cMin] = counter++;
                    }

                    cMin++;
                    dir = Direction.Right;
                }
                    break;
            }
            
        }

        return matrix;
    }
    public enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
}

public class Leet48
{
    public void Rotate(int[][] matrix) {

        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = i + 1; j < matrix.Length; j++)
            {
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }

        int c1 = 0;
        int c2 = matrix.Length - 1;
        while (c1 < c2)
        {
            SwapColumns(matrix, c1, c2);
            c1++;
            c2--;
        }
    }

    private void SwapColumns(int[][] matrix, int c1, int c2)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            (matrix[i][c1], matrix[i][c2]) = (matrix[i][c2], matrix[i][c1]);
        }
    }
}

public class MyHashMap
{
    private int _currentSize = 8;
    private HashMapNode[] _nodes;
    private int _usedBuckets;

    public MyHashMap() =>
        _nodes = new HashMapNode[_currentSize];

    public void Put(int key, int value)
    {
        if (_usedBuckets == _currentSize)
        {
            DoubleAndRehash();
        }

        var index = GetHash(key, _currentSize);
        var prevValue = _nodes[index];

        if (prevValue == null)
        {
            _usedBuckets++;
        }

        var curr = prevValue;
        while (curr != null)
        {
            if (curr.Key == key)
            {
                curr.Value = value;
                return;
            }

            curr = curr.Next;
        }

        var newValue = new HashMapNode {Key = key, Value = value};
        _nodes[index] = newValue;
        newValue.Next = prevValue;
    }

    public int Get(int key)
    {
        var index = GetHash(key, _currentSize);
        if (_nodes[index] == null)
        {
            return -1;
        }

        var curr = _nodes[index];
        while (curr != null)
        {
            if (curr.Key == key)
            {
                return curr.Value;
            }

            curr = curr.Next;
        }

        return -1;
    }

    public void Remove(int key)
    {
        var index = GetHash(key, _currentSize);
        var curr = _nodes[index];
        if (curr == null)
        {
            return;
        }


        if (curr.Key == key)
        {
            _nodes[index] = curr.Next;
        }

        var prev = curr;
        curr = prev.Next;
        while (curr != null)
        {
            if (curr.Key == key)
            {
                prev.Next = curr.Next;
                break;
            }

            prev = curr;
            curr = curr.Next;
        }

        if (_nodes[index] == null)
        {
            _usedBuckets--;
        }
    }

    private void DoubleAndRehash()
    {
        _currentSize *= 2;
        var newNodes = new HashMapNode[_currentSize];

        for (var i = 0; i < _nodes.Length; i++)
        {
            var list = _nodes[i];
            while (list != null)
            {
                var nextList = list.Next;
                var newIndex = GetHash(list.Key, _currentSize);
                var oldList = newNodes[newIndex];
                newNodes[newIndex] = list;
                list.Next = oldList;
                list = nextList;
            }
        }


        _nodes = newNodes;

        var newUsedBuckets = 0;
        foreach (var n in _nodes)
        {
            if (n != null)
            {
                newUsedBuckets++;
            }
        }

        _usedBuckets = newUsedBuckets;
    }

    private int GetHash(int key, int size) => key % size;

    private class HashMapNode
    {
        public int Key { get; set; }
        public int Value { get; set; }
        public HashMapNode Next { get; set; }
    }
}

public class Leet56
{
    public enum PointType
    {
        Start = 0,
        End = 1
    }

    public int[][] Merge(int[][] intervals)
    {
        var points = new List<Point>();
        foreach (var pt in intervals)
        {
            points.Add(new Point(pt[0], PointType.Start));
            points.Add(new Point(pt[1], PointType.End));
        }

        points.Sort((pt1, pt2) =>
        {
            var firstCompare = pt1.Value.CompareTo(pt2.Value);
            if (firstCompare == 0)
            {
                return pt1.PointType.CompareTo(pt2.PointType);
            }

            return firstCompare;
        });

        var result = new List<int[]>();

        var index = 0;
        var countOfOpen = 0;
        var startIndex = 0;
        var endIndex = 0;

        while (index < points.Count)
        {
            if (points[index].PointType == PointType.Start)
            {
                if (countOfOpen == 0)
                {
                    startIndex = index;
                }

                countOfOpen++;
            }

            if (points[index].PointType == PointType.End)
            {
                if (!IsDoubleEndStartPoint(index, points))
                {
                    countOfOpen--;
                    if (countOfOpen == 0)
                    {
                        result.Add(new[] {points[startIndex].Value, points[index].Value});
                    }
                }
            }

            index++;
        }

        return result.ToArray();
    }

    private bool IsDoubleEndStartPoint(int index, List<Point> points)
    {
        if (index + 1 >= points.Count)
        {
            return false;
        }

        var nextPoint = points[index + 1];

        if (nextPoint.PointType == PointType.Start && nextPoint.Value == points[index].Value)
        {
            return true;
        }

        return false;
    }

    public struct Point
    {
        public Point(int value, PointType pointType)
        {
            Value = value;
            PointType = pointType;
        }

        public int Value { get; }
        public PointType PointType { get; }
    }
}

public class Leet75
{
    public void SortColors(int[] nums)
    {
        var freq = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (freq.ContainsKey(num))
            {
                freq[num]++;
            }
            else
            {
                freq.Add(num, 1);
            }
        }

        var countOfZero = freq.ContainsKey(0) ? freq[0] : 0;
        var countOfOne = freq.ContainsKey(1) ? freq[1] : 0;
        var countOfTwo = freq.ContainsKey(2) ? freq[2] : 0;
        var counter = 0;

        for (var i = 0; i < countOfZero; i++)
        {
            nums[counter++] = 0;
        }

        for (var i = 0; i < countOfOne; i++)
        {
            nums[counter++] = 1;
        }

        for (var i = 0; i < countOfTwo; i++)
        {
            nums[counter++] = 2;
        }
    }
}

public class Leet169
{
    public int MajorityElement(int[] nums)
    {
        var freq = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            if (freq.ContainsKey(num))
            {
                freq[num]++;
            }
            else
            {
                freq.Add(num, 1);
            }
        }

        foreach (var (num, count) in freq)
        {
            if (count > nums.Length / 2)
            {
                return num;
            }
        }

        throw new InvalidOperationException();
    }
}

public class Leet1855
{
    public void Test()
    {
        //[9819,9508,7398,7347,6337,5756,5493,5446,5123,3215,1597,774,368,313]
        //[9933,9813,9770,9697,9514,9490,9441,9439,8939,8754,8665,8560]
        int[] nums1 = {9819, 9508, 7398, 7347, 6337, 5756, 5493, 5446, 5123, 3215, 1597, 774, 368, 313};
        int[] nums2 = {9933, 9813, 9770, 9697, 9514, 9490, 9441, 9439, 8939, 8754, 8665, 8560};
        var max = MaxDistance(nums1, nums2);
    }

    public int MaxDistance(int[] nums1, int[] nums2)
    {
        var maxDistance = 0;
        for (var i = 0; i < nums1.Length; i++)
        {
            maxDistance = Math.Max(maxDistance, DistanceFor(nums1, nums2, i));
        }

        return maxDistance;
    }

    private int DistanceFor(int[] nums1, int[] nums2, int firstIndex)
    {
        var start = firstIndex;
        var end = nums2.Length - 1;
        var target = nums1[firstIndex];

        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (nums2[mid] >= target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid - 1;
            }
        }

        if (start >= nums2.Length)
        {
            start = nums2.Length - 1;
        }

        if (nums2[start] >= target)
        {
            return start - firstIndex;
        }

        var prevStart = start - 1;
        if (prevStart >= firstIndex && nums2[prevStart] >= target)
        {
            return prevStart - firstIndex;
        }

        return 0;
    }
}

public class Leet633
{
    private bool IsPerfectSquare(int num)
    {
        checked
        {
            var left = 1;
            var right = num;

            while (left < right)
            {
                var mid = left + ((right - left) / 2);
                long lMid = mid;
                var sqr = lMid * lMid;

                if (sqr == num)
                {
                    return true;
                }

                if (sqr < num)
                {
                    left = mid + 1;
                    continue;
                }

                right = mid;
            }

            long lLeft = left;
            return lLeft * lLeft == num;
        }
    }

    public bool JudgeSquareSum(int c)
    {
        for (var a = 0; a * a <= c / 2; a++)
        {
            var a2 = a * a;
            var b2 = c - a2;

            if (b2 == 1 || b2 == 0 || IsPerfectSquare(b2))
            {
                return true;
            }
        }

        return false;
    }
}

public class Leet1346
{
    public bool CheckIfExist(int[] arr)
    {
        var hashSet = new HashSet<int>();

        foreach (var num in arr)
        {
            var dbl = num * 2;
            if (hashSet.Contains(dbl))
            {
                return true;
            }

            if (num % 2 == 0)
            {
                var half = num / 2;
                if (hashSet.Contains(half))
                {
                    return true;
                }
            }

            hashSet.Add(num);
        }

        return false;
    }
}

public class Leet1337
{
    public int[] KWeakestRows(int[][] mat, int k)
    {
        var result = new int[k];
        var list = new List<Battlion>();
        for (var index = 0; index < mat.Length; index++)
        {
            var currentRow = mat[index];
            var numberSoldiers = BinarySearch(currentRow);

            list.Add(new Battlion(index, numberSoldiers));
        }

        list = list.OrderBy(x => x.soldierCount).ToList();
        var count = 0;
        foreach (var item in list)
        {
            if (count < k)
            {
                result[count++] = item.rowNumber;
            }
        }

        return result;
    }


    public int BinarySearch(int[] currentRow)
    {
        var low = 0;
        var high = currentRow.Length;

        while (low < high)
        {
            var mid = low + ((high - low) / 2);

            if (currentRow[mid] == 1)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return low;
    }


    public class Battlion
    {
        public int rowNumber;
        public int soldierCount;

        public Battlion(int rowNumber, int soldierCount)
        {
            this.rowNumber = rowNumber;
            this.soldierCount = soldierCount;
        }
    }
}
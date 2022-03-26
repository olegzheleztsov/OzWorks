using Oz.Algorithms.Sedgewick.SearchTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new Stack<char>();
s.Push('a');
s.Push('b');
var arr = s.ToArray();
Console.WriteLine(string.Join(' ', arr));


public class Leet844
{
    public bool BackspaceCompare(string s, string t)
    {
        var s1 = GetBackspacedString(s);
        var t1 = GetBackspacedString(t);
        return s1 == t1;
    }

    private string GetBackspacedString(string s)
    {
        var stack = new Stack<char>();
        foreach (var c in s)
        {
            if (c == '#')
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                stack.Push(c);
            }
        }

        var arr = stack.ToArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}

public class Leet986
{
    public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
    {
        var res = new List<int[]>();
        var i = 0;
        var j = 0;

        while (i < firstList.Length && j < secondList.Length)
        {
            var first = firstList[i];
            var second = secondList[j];

            if (second[0] > first[1])
            {
                i++;
                continue;
            }

            if (second[1] < first[0])
            {
                j++;
                continue;
            }

            res.Add(new[] {Math.Max(first[0], second[0]), Math.Min(first[1], second[1])});
            if (first[1] < second[1])
            {
                i++;
            }
            else
            {
                j++;
            }
        }

        return res.ToArray();
    }
}

public class Leet11
{
    public int MaxArea(int[] height)
    {
        if (height.Length == 2)
        {
            return Math.Min(height[0], height[1]) * 1;
        }

        var leftIndex = 0;
        var rightIndex = height.Length - 1;
        var maxArea = Math.Min(height[leftIndex], height[rightIndex]) * (rightIndex - leftIndex);
        while (leftIndex < rightIndex)
        {
            var suggestedArea = Math.Min(height[leftIndex], height[rightIndex]) * (rightIndex - leftIndex);
            maxArea = Math.Max(maxArea, suggestedArea);
            if (height[leftIndex] < height[rightIndex])
            {
                leftIndex++;
            }
            else
            {
                rightIndex--;
            }
        }

        return maxArea;
    }
}

public class Leet15
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var res = new List<IList<int>>();
        if (nums == null || nums.Length < 3)
        {
            return res;
        }

        Array.Sort(nums);

        for (var i = 0; i < nums.Length - 2; i++)
        {
            if (nums[i] > 0 || (i > 0 && nums[i] == nums[i - 1]))
            {
                continue;
            }

            var left = i + 1;
            var right = nums.Length - 1;

            while (left < right)
            {
                if (nums[i] + nums[left] + nums[right] == 0)
                {
                    res.Add(new List<int> {nums[i], nums[left], nums[right]});
                    left++;
                    right--;

                    while (left < right && nums[left] == nums[left - 1])
                    {
                        left++;
                    }

                    while (left < right && nums[right] == nums[right + 1])
                    {
                        right--;
                    }
                }
                else if (nums[i] + nums[left] + nums[right] > 0)
                {
                    right--;
                }
                else
                {
                    left++;
                }
            }
        }

        return res;
    }
}

public class Leet82
{
    public void Test()
    {
        //[1,2,3,3,4,4,5]
        var n1 = new ListNode(1);
        var n2 = new ListNode(2);
        var n3_1 = new ListNode(3);
        var n3_2 = new ListNode(3);
        var n4_1 = new ListNode(4);
        var n4_2 = new ListNode(4);
        var n5 = new ListNode(5);
        n1.next = n2;
        n2.next = n3_1;
        n3_1.next = n3_2;
        n3_2.next = n4_1;
        n4_1.next = n4_2;
        n4_2.next = n5;
        var result = DeleteDuplicates(n1);
    }


    public ListNode DeleteDuplicates(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return head;
        }

        var prev = head;
        var cur = head.next;
        var deleteVal = new HashSet<int>();
        while (cur != null)
        {
            if (prev.val == cur.val)
            {
                prev.next = cur.next;
                if (!deleteVal.Contains(prev.val))
                {
                    deleteVal.Add(prev.val);
                }
            }
            else
            {
                prev = cur;
            }

            cur = cur.next;
        }

        while (head != null && deleteVal.Contains(head.val))
        {
            head = head.next;
        }

        if (head == null)
        {
            return head;
        }

        prev = head;
        cur = head.next;
        while (cur != null)
        {
            if (deleteVal.Contains(cur.val))
            {
                prev.next = cur.next;
            }
            else
            {
                prev = cur;
            }

            cur = cur.next;
        }


        return head;
    }

    public class ListNode
    {
        public ListNode next;
        public int val;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}

public class Leet162
{
    public int FindPeakElement(int[] nums)
    {
        var left = 0;
        var right = nums.Length - 1;
        if (nums.Length == 1)
        {
            return 0;
        }

        while (left < right)
        {
            var mid = (left + right) / 2;
            if (IsPeek(nums, mid))
            {
                return mid;
            }

            if (mid == 0)
            {
                left = mid + 1;
            }
            else if (mid == nums.Length - 1)
            {
                right = mid - 1;
            }
            else
            {
                if (nums[mid] < nums[mid + 1])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            if (Math.Abs(right - left) == 1)
            {
                if (IsPeek(nums, left))
                {
                    return left;
                }

                if (IsPeek(nums, right))
                {
                    return right;
                }

                break;
            }
        }

        if (IsPeek(nums, left))
        {
            return left;
        }

        if (IsPeek(nums, right))
        {
            return right;
        }

        return -1;
    }

    public bool IsPeek(int[] nums, int index)
    {
        if (index == 0)
        {
            return nums[0] > nums[1];
        }

        if (index == nums.Length - 1)
        {
            return nums[nums.Length - 2] < nums[nums.Length - 1];
        }

        return nums[index - 1] < nums[index] && nums[index] > nums[index + 1];
    }
}

public class Leet74_2
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var rows = matrix.Length;

        var targetRow = SearchInColumn(matrix, 0, target);
        if (targetRow >= rows)
        {
            targetRow = rows - 1;
        }

        var result = SearchInRow(matrix, targetRow, target);
        if (result != null)
        {
            return true;
        }


        targetRow--;
        if (targetRow >= 0)
        {
            result = SearchInRow(matrix, targetRow, target);
            if (result != null)
            {
                return true;
            }
        }

        targetRow += 2;
        if (targetRow < matrix.Length)
        {
            result = SearchInRow(matrix, targetRow, target);
            if (result != null)
            {
                return true;
            }
        }

        return false;
    }

    private int? SearchInRow(int[][] matrix, int row, int target)
    {
        var minIndex = 0;
        var maxIndex = matrix[0].Length - 1;
        while (minIndex <= maxIndex)
        {
            var midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[row][midIndex])
            {
                return midIndex;
            }

            if (target < matrix[row][midIndex])
            {
                maxIndex = midIndex - 1;
            }
            else
            {
                minIndex = midIndex + 1;
            }
        }

        return null;
    }

    private int SearchInColumn(int[][] matrix, int col, int target)
    {
        var minIndex = 0;
        var maxIndex = matrix.Length - 1;

        while (minIndex <= maxIndex)
        {
            var midIndex = (minIndex + maxIndex) / 2;
            if (target == matrix[midIndex][col])
            {
                return midIndex;
            }

            if (target < matrix[midIndex][col])
            {
                maxIndex = midIndex - 1;
            }
            else
            {
                minIndex = midIndex + 1;
            }
        }

        return minIndex;
    }
}

public class Leet33
{
    public void Test()
    {
        int[] nums = {5, 1, 3};
        var result = Search(nums, 5);
    }

    public int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0)
        {
            return -1;
        }

        var offset = FindMinIndex(nums);
        return BinarySearch(nums, target, offset);
    }

    private int FindMinIndex(int[] nums)
    {
        var n = nums.Length;
        var start = 0;
        var end = n - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (nums[mid] > nums[end])
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        return start;
    }

    private int BinarySearch(int[] nums, int target, int offset)
    {
        var n = nums.Length;
        var start = 0;
        var end = n - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            var realMid = (mid + offset) % n;
            if (nums[realMid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        var index = (start + offset) % n;
        return nums[index] == target ? index : -1;
    }
}


public class Leet34
{
    public int[] SearchRange(int[] nums, int target) =>
        new[] {FindFirstIndex(nums, target), FindLastIndex(nums, target)};

    private int FindFirstIndex(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int start = 0, end = nums.Length - 1;
        while (start < end)
        {
            var mid = start + ((end - start) / 2);
            if (nums[mid] < target)
            {
                start = mid + 1;
            }
            else
            {
                end = mid;
            }
        }

        return nums[start] == target ? start : -1;
    }

    private int FindLastIndex(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return -1;
        }

        int start = 0, end = nums.Length - 1;
        while (start < end)
        {
            var mid = start + ((end - start + 1) / 2);
            if (nums[mid] > target)
            {
                end = mid - 1;
            }
            else
            {
                start = mid;
            }
        }

        return nums[start] == target ? start : -1;
    }
}


public class FrequencyCounter
{
    public void Run()
    {
        var fileName = @"C:\development\tale.txt";
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"File {fileName} doesn't exist");
            return;
        }

        using var reader = new StreamReader(@"C:\development\tale.txt");
        ArraySt<string, Integer> counterSt = new();

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var tokens = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                var loweredToken = token.ToLowerInvariant();
                if (counterSt.Contains(loweredToken))
                {
                    counterSt.Get(loweredToken).Value++;
                }
                else
                {
                    counterSt.Put(loweredToken, new Integer {Value = 1});
                }
            }
        }

        foreach (var key in counterSt.Keys)
        {
            Console.WriteLine($"{key}: {counterSt.Get(key).Value}");
        }

        Console.WriteLine("Deleting...");

        var keys = counterSt.Keys;
        foreach (var key in keys)
        {
            counterSt.Delete(key);
            Console.WriteLine($"Deleted: {key}, size: {counterSt.Size}");
        }

        Console.WriteLine($"Size after deleting: {counterSt.Size}");
    }
}

public class Integer
{
    public int Value { get; set; }
}

public class Leet785
{
    public bool IsBipartite(int[][] graph)
    {
        var nodeCounts = graph.Length;

        var codes = new bool?[nodeCounts];

        for (var i = 0; i < nodeCounts; i++)
        {
            if (codes[i] == null && !Dfs(i, graph, false, codes))
            {
                return false;
            }
        }

        return true;
    }

    private bool Dfs(int node, int[][] graph, bool currentCode, bool?[] codes)
    {
        if (codes[node] != null)
        {
            return codes[node] == currentCode;
        }

        codes[node] = currentCode;

        for (var i = 0; i < graph[node].Length; i++)
        {
            if (!Dfs(graph[node][i], graph, !currentCode, codes))
            {
                return false;
            }
        }

        return true;
    }
}


public class Leet886
{
    public bool PossibleBipartition(int n, int[][] dislikes)
    {
        var graph = new List<int>[n + 1];

        for (var i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

        foreach (var dislike in dislikes)
        {
            graph[dislike[0]].Add(dislike[1]);
            graph[dislike[1]].Add(dislike[0]);
        }

        var locations = new int[n + 1];
        for (var i = 1; i <= n; i++)
        {
            if (!Partition(graph, i, locations[i], locations))
            {
                return false;
            }
        }

        return true;
    }

    private bool Partition(List<int>[] graph, int i, int value, int[] locations)
    {
        if (value == 0)
        {
            value = 1;
        }

        if (locations[i] == value)
        {
            return true;
        }

        if (locations[i] == -value)
        {
            return false;
        }

        locations[i] = value;

        foreach (var j in graph[i])
        {
            if (!Partition(graph, j, -value, locations))
            {
                return false;
            }
        }

        return true;
    }
}

public class Leet1615
{
    public int MaximalNetworkRank(int n, int[][] roads)
    {
        var res = 0;
        var inDegree = new Dictionary<int, int>();
        var graph = new Dictionary<int, HashSet<int>>();

        foreach (var road in roads)
        {
            if (!inDegree.ContainsKey(road[0]))
            {
                inDegree.Add(road[0], 0);
            }

            if (!inDegree.ContainsKey(road[1]))
            {
                inDegree.Add(road[1], 0);
            }

            inDegree[road[0]]++;
            inDegree[road[1]]++;

            if (!graph.ContainsKey(road[0]))
            {
                graph.Add(road[0], new HashSet<int>());
            }

            if (!graph.ContainsKey(road[1]))
            {
                graph.Add(road[1], new HashSet<int>());
            }

            graph[road[0]].Add(road[1]);
            graph[road[1]].Add(road[0]);
        }

        var ranks = inDegree.OrderByDescending(x => x.Value).Select(x => new[] {x.Key, x.Value}).ToArray();

        for (var i = 0; i < ranks.Length - 1; i++)
        {
            for (var j = i + 1; j < ranks.Length; j++)
            {
                res = Math.Max(res,
                    ranks[i][1] + (graph[ranks[i][0]].Contains(ranks[j][0]) ? ranks[j][1] - 1 : ranks[j][1]));
            }
        }

        return res;
    }
}

public class Leet433
{
    public int MinMutation(string start, string end, string[] bank)
    {
        const string GeneBase = "ACGT";
        var bankSet = new HashSet<string>(bank);
        var genes = new Queue<string>();
        genes.Enqueue(start);
        bankSet.Remove(start);
        var level = 0;

        while (genes.Count > 0)
        {
            var count = genes.Count;
            for (var i = 0; i < count; i++)
            {
                var current = genes.Dequeue();
                if (current == end)
                {
                    return level;
                }

                foreach (var g in GeneBase)
                {
                    var currentGenes = current.ToCharArray();
                    for (var j = 0; j < currentGenes.Length; j++)
                    {
                        var t = currentGenes[j];
                        currentGenes[j] = g;
                        var next = new string(currentGenes);
                        if (bankSet.Contains(next))
                        {
                            bankSet.Remove(next);
                            genes.Enqueue(next);
                        }

                        currentGenes[j] = t;
                    }
                }
            }

            level++;
        }

        return -1;
    }
}

public class Leet127
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        var queue = new Queue<string>();
        var visited = new HashSet<string>();
        var bank = new HashSet<string>();
        queue.Enqueue(beginWord);
        var moves = 0;

        while (queue.Count > 0)
        {
            var cnt = queue.Count;
            for (var i = 0; i < cnt; i++)
            {
                var current = queue.Dequeue();
                if (current == endWord)
                {
                    return moves;
                }

                var candidates = wordList.Where(w => !visited.Contains(w) && Difference(current, w) == 1);

                foreach (var candidate in candidates)
                {
                    visited.Add(candidate);
                    queue.Enqueue(candidate);
                }
            }

            moves++;
        }

        return 0;
    }

    private int Difference(string word1, string word2)
    {
        var cnt = 0;
        for (var i = 0; i < word1.Length; i++)
        {
            if (word1[i] != word2[i])
            {
                cnt++;
            }
        }

        return cnt;
    }
}

public class Leet752
{
    public static void Test()
    {
        string[] deadends = {"0201", "0101", "0102", "1212", "2002"};
        var target = "0202";
        var obj = new Leet752();
        var count = obj.OpenLock(deadends, target);
        Console.WriteLine(count);
    }

    public int OpenLock(string[] deadends, string target)
    {
        var visited = new HashSet<string>();
        var deadSet = new HashSet<string>(deadends);
        var queue = new Queue<string>();

        if (deadSet.Contains("0000"))
        {
            return -1;
        }

        queue.Enqueue("0000");

        var moves = 0;

        while (queue.Count > 0)
        {
            var cnt = queue.Count;

            for (var i = 0; i < cnt; i++)
            {
                var current = queue.Dequeue();
                if (current == target)
                {
                    return moves;
                }

                for (var j = 0; j < 4; j++)
                {
                    var candidateRight = Move(current, j, true);
                    var candidateLeft = Move(current, j, false);

                    if (!visited.Contains(candidateRight) && !deadSet.Contains(candidateRight))
                    {
                        visited.Add(candidateRight);
                        queue.Enqueue(candidateRight);
                    }

                    if (!visited.Contains(candidateLeft) && !deadSet.Contains(candidateLeft))
                    {
                        visited.Add(candidateLeft);
                        queue.Enqueue(candidateLeft);
                    }
                }
            }

            moves++;
        }

        return -1;
    }

    private string Move(string s, int index, bool isForward)
    {
        var charr = s.ToCharArray();
        var num = int.Parse(charr[index].ToString());

        if (isForward)
        {
            if (num == 9)
            {
                num = 0;
            }
            else
            {
                num++;
            }
        }
        else
        {
            if (num == 0)
            {
                num = 9;
            }
            else
            {
                num--;
            }
        }

        charr[index] = num.ToString()[0];
        return new string(charr);
    }
}

public class Leet997
{
    public int FindJudge(int n, int[][] trust)
    {
        if (trust.Length == 0 && n == 1)
        {
            return 1;
        }

        var input = new Dictionary<int, int>();
        var output = new Dictionary<int, int>();
        foreach (var tr in trust)
        {
            if (output.ContainsKey(tr[0]))
            {
                output[tr[0]]++;
            }
            else
            {
                output.Add(tr[0], 1);
            }

            if (input.ContainsKey(tr[1]))
            {
                input[tr[1]]++;
            }
            else
            {
                input.Add(tr[1], 1);
            }
        }

        foreach (var (num, cnt) in input)
        {
            if (cnt == n - 1 && !output.ContainsKey(num))
            {
                return num;
            }
        }

        return -1;
    }
}

public class Leet1557
{
    public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
    {
        var input = new Dictionary<int, int>();
        foreach (var edge in edges)
        {
            if (!input.ContainsKey(edge[1]))
            {
                input.Add(edge[1], 1);
            }
            else
            {
                input[edge[1]] = 1;
            }
        }

        var result = new List<int>();
        for (var i = 0; i < n; i++)
        {
            if (!input.ContainsKey(i))
            {
                result.Add(i);
            }
        }

        return result;
    }
}
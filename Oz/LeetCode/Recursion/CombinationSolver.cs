#region

using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Recursion;

public class CombinationSolver
{
    public static IEnumerable<IList<int>> Combine(int n, int k)
    {
        var combinations = new List<IList<int>>();
        CombineInternal(1, n, k, new List<int>(), combinations);
        return combinations;
    }

    private static void CombineInternal(int startNumber, int n, int k, IList<int> combination,
        ICollection<IList<int>> allCombinations)
    {
        if (k == 0)
        {
            allCombinations.Add(new List<int>(combination));
        }
        else
        {
            for (var i = startNumber; i <= n; i++)
            {
                combination.Add(i);
                CombineInternal(i + 1, n, k - 1, combination, allCombinations);
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }
}
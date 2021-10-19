using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode.Recursion;

internal class PermutationSolver
{
    public IList<IList<int>> Permute(int[] nums)
    {
        switch (nums.Length)
        {
            case 0:
                return new List<IList<int>>();
            case 1:
            {
                var permutations = new List<int> {nums[0]};
                var result = new List<IList<int>> {permutations};
                return result;
            }
        }

        var val = nums[0];
        var remainNums = nums.Skip(1).ToArray();
        var subResults = Permute(remainNums);

        var finalResult = new List<IList<int>>();
        foreach (var sr in subResults)
        {
            for (var i = 0; i <= sr.Count; i++)
            {
                var newResult = new List<int>(sr);
                newResult.Insert(i, val);
                finalResult.Add(newResult);
            }
        }

        return finalResult;
    }
}
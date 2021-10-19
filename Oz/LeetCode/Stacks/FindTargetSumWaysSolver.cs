using System;

namespace Oz.LeetCode.Stacks;

public class FindTargetSumWaysSolver
{
    public int FindTargetSumWays(int[] nums, int target)
    {
        switch (nums.Length)
        {
            case 0:
                return 0;
            case 1 when nums[0] == Math.Abs(target):
                return 1;
            case 1:
                return 0;
        }

        var result = 0;
        FindTargetSumWays(nums, target, 0, 0, ref result);
        return result;
    }

    private static void FindTargetSumWays(int[] nums, int target, int count, int sum, ref int result)
    {
        if (count == nums.Length)
        {
            if (sum == target)
            {
                result++;
            }
        }
        else
        {
            FindTargetSumWays(nums, target, count + 1, sum + nums[count], ref result);
            FindTargetSumWays(nums, target, count + 1, sum - nums[count], ref result);
        }
    }
}
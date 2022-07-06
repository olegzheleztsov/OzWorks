// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _826
{

    public static void Test()
    {
        int[] difficulty =
        {
            2, 4, 6, 8, 10
        };
        int[] profit =
        {
            10, 20, 30, 40, 50
        };
        int[] workers =
        {
            4, 5, 6, 7
        };
        _826 solution = new _826();
        Console.WriteLine(solution.MaxProfitAssignment(difficulty, profit, workers));
    }
    public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
    {
        int res = 0;
        Job[] jobs = new Job[difficulty.Length];
        for (int i = 0; i < difficulty.Length; i++)
        {
            jobs[i] = new Job(difficulty[i], profit[i]);
        }
        Array.Sort(jobs, (j1, j2) => j1.Difficulty.CompareTo(j2.Difficulty));
        Array.Sort(worker);

        int lastJobIdx = 0;
        int maxProfit = 0;

        foreach (var w in worker)
        {
            while (lastJobIdx < jobs.Length && w >= jobs[lastJobIdx].Difficulty)
            {
                maxProfit = Math.Max(maxProfit, jobs[lastJobIdx].Profit);
                lastJobIdx++;
            }

            res += maxProfit;
        }

        return res;
    }

    public record Job(int Difficulty, int Profit);
}
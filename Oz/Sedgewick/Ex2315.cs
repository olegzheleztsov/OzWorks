// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Numerics;

namespace Oz.Sedgewick;

public class Ex2315
{
    public void Match(int[] nuts, int[] bolts)
    {
        nuts.Shuffle();
        bolts.Shuffle();
        MatchNutsBolts(nuts, bolts, 0, nuts.Length - 1);
    }

    private void MatchNutsBolts(int[] nuts, int[] bolts, int lo, int hi)
    {
        if (hi <= lo)
        {
            return;
        }

        var pivot = Partition(nuts, lo, hi, bolts[hi]);
        Partition(bolts, lo, hi, nuts[pivot]);

        MatchNutsBolts(nuts, bolts, lo, pivot - 1);
        MatchNutsBolts(nuts, bolts, pivot + 1, hi);
    }

    private int Partition(int[] array, int lo, int hi, int pivot)
    {
        var i = lo;
        for (var j = lo; j < hi; j++)
        {
            if (array[j] < pivot)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
            }
            else if (array[j] == pivot)
            {
                (array[j], array[hi]) = (array[hi], array[j]);
                j--;
            }
        }

        (array[i], array[hi]) = (array[hi], array[i]);
        return i;
    }
}
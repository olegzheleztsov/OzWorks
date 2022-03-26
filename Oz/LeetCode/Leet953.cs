// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet953
{
    public bool IsAlienSorted(string[] words, string order) {
        if (words.Length == 1)
        {
            return true;
        }

        for (int i = 1; i < words.Length; i++)
        {
            if (!IsLess(words[i - 1], words[i], order))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsLess(string first, string second, string order)
    {
        int fIndex = 0;
        int sIndex = 0;
        while (fIndex < first.Length && sIndex < second.Length && first[fIndex] == second[sIndex])
        {
            fIndex++;
            sIndex++;
        }

        if (fIndex >= first.Length && sIndex >= second.Length)
        {
            return true;
        }
        if (fIndex < first.Length && sIndex >= second.Length)
        {
            return false;
        }

        if (fIndex >= first.Length && sIndex < second.Length)
        {
            return true;
        }

        int fOrder = order.IndexOf(first[fIndex]);
        int sOrder = order.IndexOf(second[sIndex]);
        return fOrder < sOrder;
    }
}
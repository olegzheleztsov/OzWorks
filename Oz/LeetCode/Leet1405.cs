// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

namespace Oz.LeetCode;

public class Leet1405
{
    public string LongestDiverseString(int a, int b, int c)
    {
        var totalCount = a + b + c;
        var s = string.Empty;
        var previousChar = ' ';
        var prevCount = 0;

        while (totalCount > 0)
        {
            var currentChar = ' ';
            if (string.IsNullOrEmpty(s) || prevCount < 2)
            {
                currentChar = FirstLongestChar(a, b, c);
                if (currentChar != previousChar && previousChar != ' ')
                {
                    prevCount = 0;
                }
            }
            else
            {
                currentChar = NextLongestChar(previousChar, a, b, c);
                if (currentChar != previousChar && previousChar != ' ')
                {
                    prevCount = 0;
                }
            }

            if (currentChar == previousChar && currentChar != ' ' && prevCount == 2)
            {
                break;
            }

            var countToInsert = 1;
            s = s + new string(currentChar, countToInsert);
            prevCount++;

            if (currentChar == 'a')
            {
                a -= countToInsert;
            }
            else if (currentChar == 'b')
            {
                b -= countToInsert;
            }
            else
            {
                c -= countToInsert;
            }

            totalCount -= countToInsert;
            previousChar = currentChar;
        }

        return s;
    }

    private char FirstLongestChar(int a, int b, int c)
    {
        if (a > b && a > c)
        {
            return 'a';
        }

        return b > c ? 'b' : 'c';
    }

    private char NextLongestChar(char forbiddenChar, int a, int b, int c)
    {
        if (forbiddenChar == 'a')
        {
            if (b > c)
            {
                return 'b';
            }

            if (c > 0)
            {
                return 'c';
            }
        }

        if (forbiddenChar == 'b')
        {
            if (a > c)
            {
                return 'a';
            }

            if (c > 0)
            {
                return 'c';
            }
        }

        if (forbiddenChar == 'c')
        {
            if (a > b)
            {
                return 'a';
            }

            if (b > 0)
            {
                return 'b';
            }
        }

        return forbiddenChar;
    }
}
// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Text;

namespace Oz.LeetCode;

public class Leet168
{
    public string ConvertToTitle(int columnNumber)
    {
        var builder = new StringBuilder();
        while (columnNumber > 26)
        {
            var mode = columnNumber % 26;
            if (mode == 0)
            {
                mode = 26;
            }

            builder.Insert(0, NumberToChar(mode));
            columnNumber -= mode;
            columnNumber /= 26;
        }

        if (columnNumber > 0)
        {
            builder.Insert(0, NumberToChar(columnNumber));
        }

        return builder.ToString();
    }

    private char NumberToChar(int number) =>
        (char)('A' + number - 1);

    public static void Test()
    {
        var leet = new Leet168();
        for (var i = 1; i <= 100000; i++)
        {
            Console.WriteLine($"For number {i} page index is {leet.ConvertToTitle(i)}");
        }
    }
}
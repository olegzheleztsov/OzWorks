// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet118
{
    public IList<IList<int>> Generate(int numRows)
    {
        var triangle = new List<IList<int>>();

        if (numRows >= 1)
        {
            triangle.Add(new List<int> {1});
        }

        if (numRows >= 2)
        {
            triangle.Add(new List<int> {1, 1});
        }

        for (var r = 3; r <= numRows; r++)
        {
            var row = new List<int> {1};
            for (var c = 1; c < r - 1; c++)
            {
                row.Add(triangle[r - 2][c] + triangle[r - 2][c - 1]);
            }

            row.Add(1);
            triangle.Add(row);
        }

        return triangle;
    }

    public IList<int> GetRow(int rowIndex)
    {
        if (rowIndex == 0)
        {
            return new List<int> {1};
        }

        if (rowIndex == 1)
        {
            return new List<int> {1, 1};
        }

        var prevList = new List<int> {1, 1};

        for (var k = 2; k <= rowIndex; k++)
        {
            var newRow = new List<int> {1};
            for (var c = 1; c < k; c++)
            {
                newRow.Add(prevList[c] + prevList[c - 1]);
            }

            newRow.Add(1);
            prevList = newRow;
        }

        return prevList;
    }

    public static void Test()
    {
        var obj = new Leet118();
        obj.Generate(5);
    }
}
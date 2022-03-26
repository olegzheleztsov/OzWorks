// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet695
{
    public int MaxAreaOfIsland(int[][] grid)
    {
        var rows = grid.Length;
        var columns = grid[0].Length;

        var vertices = new Vertex[rows][];
        for (var i = 0; i < rows; i++)
        {
            vertices[i] = new Vertex[columns];
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                vertices[i][j] = new Vertex {Value = grid[i][j], Row = i, Column = j};
            }
        }

        var maxArea = 0;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                if (vertices[i][j].Value == 1 && !vertices[i][j].Visited)
                {
                    var area = GetAreaAtPosition(vertices, i, j);
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
        }

        return maxArea;
    }

    private int GetAreaAtPosition(Vertex[][] grid, int row, int column)
    {
        var area = 0;
        var rows = grid.Length;
        var columns = grid[0].Length;

        var stack = new Stack<Vertex>();
        grid[row][column].Visited = true;
        area++;
        stack.Push(grid[row][column]);

        while (stack.Any())
        {
            var vert = stack.Pop();

            var tr = vert.Row - 1;
            var br = vert.Row + 1;
            var lc = vert.Column - 1;
            var rc = vert.Column + 1;

            if (tr >= 0 && grid[tr][vert.Column].Value == 1 && !grid[tr][vert.Column].Visited)
            {
                grid[tr][vert.Column].Visited = true;
                area++;
                stack.Push(grid[tr][vert.Column]);
            }

            if (br < rows && grid[br][vert.Column].Value == 1 && !grid[br][vert.Column].Visited)
            {
                grid[br][vert.Column].Visited = true;
                area++;
                stack.Push(grid[br][vert.Column]);
            }

            if (lc >= 0 && grid[vert.Row][lc].Value == 1 && !grid[vert.Row][lc].Visited)
            {
                grid[vert.Row][lc].Visited = true;
                area++;
                stack.Push(grid[vert.Row][lc]);
            }

            if (rc < columns && grid[vert.Row][rc].Value == 1 && !grid[vert.Row][rc].Visited)
            {
                grid[vert.Row][rc].Visited = true;
                area++;
                stack.Push(grid[vert.Row][rc]);
            }
        }

        return area;
    }

    public class Vertex
    {
        public int Value { get; set; }
        public bool Visited { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
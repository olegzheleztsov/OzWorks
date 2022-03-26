// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet1559
{
    public void Test()
    {
        char[][] grid =
        {
            new[] {'c', 'a', 'a'}, new[] {'d', 'a', 'd'}, new[] {'d', 'a', 'c'}, new[] {'d', 'c', 'd'},
            new[] {'a', 'a', 'c'}
        };

        var result = ContainsCycle(grid);
        Console.WriteLine(result);
    }

    public bool ContainsCycle(char[][] grid)
    {
        var rows = grid.Length;
        var columns = grid[0].Length;

        if (rows < 2 || columns < 2)
        {
            return false;
        }

        var nodes = new Node[rows][];

        for (var i = 0; i < rows; i++)
        {
            nodes[i] = new Node[columns];
            for (var j = 0; j < columns; j++)
            {
                nodes[i][j] = new Node {Column = j, Row = i, Value = grid[i][j]};
            }
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                if (!nodes[i][j].Visited)
                {
                    if (IsContainsCircleInner(nodes, i, j, rows, columns))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool IsContainsCircleInner(Node[][] nodes, int startRow, int startColumn, int rows, int columns)
    {
        List<Node> visitedPath = new();
        var stack = new Stack<Node>();
        var targetChar = nodes[startRow][startColumn].Value;
        stack.Push(nodes[startRow][startColumn]);


        while (stack.Any())
        {
            var current = stack.Pop();
            current.Visited = true;
            visitedPath.Add(current);

            var leftColumn = current.Column - 1;
            if (leftColumn >= 0 && nodes[current.Row][leftColumn].Value == targetChar)
            {
                if (!nodes[current.Row][leftColumn].Visited)
                {
                    stack.Push(nodes[current.Row][leftColumn]);
                }
                else
                {
                    if (IsCircleEnd(visitedPath, nodes[current.Row][leftColumn]))
                    {
                        return true;
                    }
                }
            }

            var rightColumn = current.Column + 1;
            if (rightColumn < columns &&
                nodes[current.Row][rightColumn].Value == targetChar)
            {
                if (!nodes[current.Row][rightColumn].Visited)
                {
                    stack.Push(nodes[current.Row][rightColumn]);
                }
                else
                {
                    if (IsCircleEnd(visitedPath, nodes[current.Row][rightColumn]))
                    {
                        return true;
                    }
                }
            }

            var upRow = current.Row - 1;
            if (upRow >= 0 &&
                nodes[upRow][current.Column].Value == targetChar)
            {
                if (!nodes[upRow][current.Column].Visited)
                {
                    stack.Push(nodes[upRow][current.Column]);
                }
                else
                {
                    if (IsCircleEnd(visitedPath, nodes[upRow][current.Column]))
                    {
                        return true;
                    }
                }
            }

            var downRow = current.Row + 1;
            if (downRow < rows &&
                nodes[downRow][current.Column].Value == targetChar)
            {
                if (!nodes[downRow][current.Column].Visited)
                {
                    stack.Push(nodes[downRow][current.Column]);
                }
                else
                {
                    if (IsCircleEnd(visitedPath, nodes[downRow][current.Column]))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool IsCircleEnd(List<Node> visitedPath, Node candidate)
    {
        var index = visitedPath.IndexOf(candidate);
        return visitedPath.Count - index >= 4 && IsAdjacent(candidate, visitedPath[visitedPath.Count - 1]);
    }

    private bool IsAdjacent(Node a, Node b) =>
        (a.Row == b.Row && (a.Column == b.Column - 1 || a.Column == b.Column + 1)) ||
        (a.Column == b.Column && (a.Row == b.Row - 1 || a.Row == b.Row + 1));

    public class Node
    {
        public bool Visited { get; set; }

        public char Value { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
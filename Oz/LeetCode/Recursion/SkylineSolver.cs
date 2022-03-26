#region

using Oz.Algorithms.DataStructures;
using System;
using System.Collections.Generic;

#endregion

namespace Oz.LeetCode.Recursion;

public class SkylineSolver
{
    public IList<IList<int>> GetSkyline(int[][] buildings)
    {
        var points = new List<Point>();

        foreach (var building in buildings)
        {
            points.Add(new Point(building[0], building[2], 'S'));
            points.Add(new Point(building[1], building[2], 'E'));
        }

        int Comparison(Point p1, Point p2)
        {
            if (p1.X < p2.X)
            {
                return -1;
            }

            if (p1.X > p2.X)
            {
                return 1;
            }

            if (p1.X == p2.X)
            {
                if (p1.Type == p2.Type && p1.Type == 'S')
                {
                    if (p1.Y < p2.Y)
                    {
                        return 1;
                    }

                    return -1;
                }

                if (p1.Type == p2.Type && p1.Type == 'E')
                {
                    if (p1.Y < p2.Y)
                    {
                        return -1;
                    }

                    return 1;
                }

                if (p1.Type != p2.Type)
                {
                    if (p1.Y < p2.Y)
                    {
                        return 1;
                    }

                    return -1;
                }
            }

            return 0;
        }

        points.Sort((Comparison<Point>)Comparison);

        var priorityQueue = new MaxPriorityQueue<int>();
        priorityQueue.Insert(0, 0);
        var output = new List<(int x, int y)>();

        foreach (var point in points)
        {
            if (point.Type == 'S')
            {
                var prevMax = priorityQueue.Maximum();
                priorityQueue.Insert(point.Y, point.Y);
                if (prevMax != priorityQueue.Maximum())
                {
                    output.Add((point.X, point.Y));
                }
            }
            else
            {
                var prevMax = priorityQueue.Maximum();
                if (priorityQueue.ContainsElement(point.Y, (a, b) => a.CompareTo(b)))
                {
                    priorityQueue.RemoveElement(point.Y, (a, b) => a.CompareTo(b));

                    if (priorityQueue.Maximum() != prevMax)
                    {
                        output.Add((point.X, priorityQueue.Maximum()));
                    }
                }
            }
        }

        var result = new List<IList<int>>();
        foreach (var (x, y) in output)
        {
            var prevIndex = result.FindIndex(t => t[0] == x);
            if (prevIndex >= 0)
            {
                if (result[prevIndex][1] <= y)
                {
                    result.RemoveAt(prevIndex);
                }
            }

            result.Add(new List<int> {x, y});
        }

        return result;
    }

    private class Point
    {
        public Point(int x, int y, char type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public int X { get; }
        public int Y { get; }
        public char Type { get; }
    }
}
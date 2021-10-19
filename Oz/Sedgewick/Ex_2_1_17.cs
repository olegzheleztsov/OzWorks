// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Sort.V2;
using System;
using System.Threading.Tasks;
namespace Oz.Sedgewick;

public class Ex_2_1_17
{
    public Ex_2_1_17() =>
        Console.OutputEncoding = System.Text.Encoding.UTF8;

    public void VisualizeSorting(int[] array, SortKind sortKind, int delayMilliseconds = 100)
    {
        switch (sortKind)
        {
            case SortKind.Insertion:
            {
                Insertion.Sort(array, arr =>
                {
                    PrintArray(arr);
                    Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds)).Wait();
                });
            }
                break;
            case SortKind.Selection:
            {
                Selection.Sort(array, arr =>
                {
                    PrintArray(arr);
                    Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds)).Wait();
                });
            }
                break;
            case SortKind.Shell:
            {
                Shell.Sort(array, arr =>
                {
                    PrintArray(arr);
                    Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds)).Wait();
                });
            }
                break;
        }
    }

    public void PrintArray(int[] nums)
    {
        Console.Clear();
        var xStart = 1;
        var yStart = Console.WindowHeight - 5;

        foreach (var number in nums)
        {
            PrintNumber(number, xStart, yStart);
            xStart += 2;
        }
    }

    private void PrintNumber(int number, int xStart, int yStart, bool clear = false)
    {
        if (clear)
        {
            Console.Clear();
        }

        var xPos = xStart;
        var yPos = yStart;
        for (var i = 0; i < number; i++)
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.Write('□');
            yPos--;
        }
        
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
    }
}
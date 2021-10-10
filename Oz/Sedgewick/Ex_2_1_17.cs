// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace Oz.Sedgewick;

public class Ex_2_1_17
{
    public Ex_2_1_17()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
    }

    public void PrintNumber(int number)
    {
        Console.Clear();
        int xPos = Console.WindowWidth / 2;
        int yPos = Console.WindowHeight - 5;
        
        

        for (int i = 0; i < number; i++)
        {
            Console.SetCursorPosition(xPos, yPos);
            Console.Write('□');
            yPos--;
        }
        
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
    }
}
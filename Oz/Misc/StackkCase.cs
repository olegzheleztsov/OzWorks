using Oz.Algorithms.DataStructures;
using System;

namespace Oz;

public class StackkCase
{
    public void Run()
    {
        var stack = new OzStack<int>(10);
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine(stack.ToString());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.Pop());
        Console.WriteLine(stack.ToString());
        Console.WriteLine(stack.IsEmpty);

        try
        {
            for (var i = 0; i < 20; i++)
            {
                stack.Push(i);
            }
        }
        catch (IndexOutOfRangeException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
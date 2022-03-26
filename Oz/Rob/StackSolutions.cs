using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using static System.Console;

namespace Oz.Rob;

public class StackSolutions
{
    public void TestReversedStack()
    {
        var stack = new OzLinkedListBasedStack<int>();
        for (var i = 1; i <= 5; i++)
        {
            stack.Push(i);
        }

        WriteLine($"Source: {stack}");

        var reversedStack = stack.Reversed();
        WriteLine($"Reversed: {reversedStack}");
    }

    public void TestInsertionSort()
    {
        var randomSource = new DefaultRandomSource();
        var stack = new OzLinkedListBasedStack<int>();
        for (var i = 0; i < 20; i++)
        {
            stack.Push(randomSource.RandomValue(1, 11));
        }

        WriteLine($"Source: {stack}");
        stack.InsertionSort(Comparisions.StandardComparision);
        WriteLine(stack);
    }

    public void TestSelectionSort()
    {
        var randomSource = new DefaultRandomSource();
        var stack = new OzLinkedListBasedStack<int>();
        for (var i = 0; i < 20; i++)
        {
            stack.Push(randomSource.RandomValue(1, 11));
        }

        WriteLine($"Source: {stack}");
        stack.SelectionSort(Comparisions.StandardComparision);
        WriteLine(stack);
    }
}
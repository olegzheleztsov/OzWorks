using Oz.Algorithms.DataStructures;
using System;

namespace Oz;

public class PriorityQueueCase
{
    public void Run()
    {
        var maxPriorityQueue = new MaxPriorityQueue<Data>();
        for (var i = 0; i < 33; i++)
        {
            maxPriorityQueue.Insert(new Data(i * 10), i);
        }

        for (var i = 0; i < 33; i++)
        {
            var element = maxPriorityQueue.ExtractMaximum();
            Console.WriteLine(element.ToString());
        }
    }
}
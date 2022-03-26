// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Oz.Algorithms.Sedgewick;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oz.Sedgewick;

public class UsingMaxPriorityQueueProgram
{
    public void InsertionAndDeletion()
    {
        var random = new Random();
        CancellationTokenSource cancellationTokenSource = new();
        var priorityQueue = new MaxPriorityQueue<int>(8);

        var task = Task.Run(async () =>
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationTokenSource.Token);
                Console.WriteLine("Produce:");
                var produceTimes = random.Next(100);
                for (var i = 0; i < produceTimes; i++)
                {
                    var number = random.Next(1000);
                    priorityQueue.Insert(number);
                    Console.Write($"{number}, ");
                }

                Console.WriteLine();
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationTokenSource.Token);
                Console.WriteLine("Consume:");
                while (!priorityQueue.IsEmpty)
                {
                    var number = priorityQueue.DeleteMax();
                    Console.Write($"{number}, ");
                }

                Console.WriteLine();
            }
        }, cancellationTokenSource.Token);
        Console.ReadLine();
        cancellationTokenSource.Cancel();
        try
        {
            task.Wait();
        }
        catch
        {
        }

        Console.WriteLine("Done");
    }

    public void InsertionAndDeletionDebug()
    {
        var random = new Random();
        var priorityQueue = new MaxPriorityQueue<int>(8);

        while (Console.ReadLine() != "q")
        {
            Console.WriteLine("Produce:");
            var produceTimes = random.Next(100);
            for (var i = 0; i < produceTimes; i++)
            {
                var number = random.Next(1000);
                priorityQueue.Insert(number);
                Console.Write($"{number}, ");
            }

            Console.WriteLine();
            Console.WriteLine("Consume:");
            while (!priorityQueue.IsEmpty)
            {
                var number = priorityQueue.DeleteMax();
                Console.Write($"{number}, ");
            }
        }
    }
}
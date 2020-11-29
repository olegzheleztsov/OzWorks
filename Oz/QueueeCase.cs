using System;
using Oz.Algorithms.DataStructures;

namespace Oz
{
    public class QueueeCase
    {
        public void Run()
        {
            var queue = new OzQueue<int>(10);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Enqueue(7);
            queue.Enqueue(8);
            Console.WriteLine(queue.ToString());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            queue.Enqueue(9);
            queue.Enqueue(10);
            queue.Enqueue(11);
            Console.WriteLine(queue.ToString());

            if (!queue.IsFull)
            {
                queue.Enqueue(12);
                Console.WriteLine(queue.ToString());
            }

            if (!queue.IsFull)
            {
                queue.Enqueue(13);
                Console.WriteLine(queue.ToString());
            }

            Console.WriteLine(queue.ToString());
            Console.WriteLine(queue.IsFull);

            while (!queue.IsEmpty)
            {
                Console.WriteLine(queue.Dequeue());
            }
        }
    }
}
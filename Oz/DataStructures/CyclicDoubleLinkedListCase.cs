using System;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;

namespace Oz.DataStructures
{
    public static class CyclicDoubleLinkedListCase
    {
        public static void Run()
        {
            var list = new OzDoubleCyclicLinkedList<int>(Allocators.DoubleLinkedNodeAllocator);
            list.Insert(1);
            list.Insert(2);
            list.Insert(3);
            Console.WriteLine($"after construction: {list.GetStringRepresentation()}");
            var result = list.Search(element => element == 2);
            Console.WriteLine($"result of search 2: {result.Data}");
            list.Delete((e) => e == 2);
            Console.WriteLine($"after deleting 2: {list.GetStringRepresentation()}");
            
        }

        public static void StackBasedOnList()
        {
            var stack = new OzLinkedListBasedStack<int>();
            stack.Push(1);
            stack.Push(2);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
        }

        public static void QueueOnSingleLinkedList()
        {
            var queue = new OzSingleLinkedListBasedQueue<int>();
            for (int i = 0; i < 3; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(queue.Dequeue());
            }
        }
        
        
    }
}
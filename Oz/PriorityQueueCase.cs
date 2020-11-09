using System;
using Oz.Algorithms.DataStructures;

namespace Oz
{
    public class PriorityQueueCase
    {
        public void Run()
        {
            PriorityQueue<Data> priorityQueue = new PriorityQueue<Data>();
            for (int i = 0; i < 33; i++)
            {
                priorityQueue.Insert(new Data(i * 10), i);
            }

            for (int i = 0; i < 33; i++)
            {
                var element = priorityQueue.ExtractMaximum();
                Console.WriteLine(element.ToString());
            }
            
        }
    }
    
}
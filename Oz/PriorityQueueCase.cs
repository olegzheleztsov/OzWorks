using System;
using Oz.Algorithms.DataStructures;

namespace Oz
{
    public class PriorityQueueCase
    {
        public void Run()
        {
            MaxPriorityQueue<Data> maxPriorityQueue = new MaxPriorityQueue<Data>();
            for (int i = 0; i < 33; i++)
            {
                maxPriorityQueue.Insert(new Data(i * 10), i);
            }

            for (int i = 0; i < 33; i++)
            {
                var element = maxPriorityQueue.ExtractMaximum();
                Console.WriteLine(element.ToString());
            }
            
        }
    }
    
}
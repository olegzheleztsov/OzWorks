﻿using System.Collections.Generic;

namespace Oz.LeetCode.QueueStacks
{
    public class RoomVisitor
    {
            public bool CanVisitAllRooms(IList<IList<int>> rooms)
            {
                HashSet<int> visitedRooms = new HashSet<int>();
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(0);
                
                while (queue.Count > 0)
                {
                    int room = queue.Dequeue();
                    visitedRooms.Add(room);
                    
                    foreach(var key in rooms[room])
                    {
                        if (!visitedRooms.Contains(key))
                        {
                            queue.Enqueue(key);
                        }
                    }
                }

                return visitedRooms.Count == rooms.Count;
            }
    }
}
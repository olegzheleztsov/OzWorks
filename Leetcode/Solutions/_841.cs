// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _841
{
    public bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        var visitedRooms = new HashSet<int>();
        Visit(rooms, 0, visitedRooms);
        return visitedRooms.Count == rooms.Count;
    }

    private static void Visit(IList<IList<int>> rooms, int index, HashSet<int> visitedRooms)
    {
        visitedRooms.Add(index);

        foreach (var connectedRoom in rooms[index])
        {
            if (!visitedRooms.Contains(connectedRoom))
            {
                Visit(rooms, connectedRoom, visitedRooms);
            }
        }
    }
}
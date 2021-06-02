using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Oz.LeetCode.QueueStacks
{
    public class BfsSolutions
    {
        public int NumIslands(char[][] grid)
        {
            int numRows = grid.Length;
            int numColumns = grid[0].Length;
            int islandCount = 0;
            var queue = new Queue<Index>();

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        islandCount++;
                        
                        grid[i][j] = '2';
                        queue.Enqueue(new Index(i, j));
                        while (queue.Count > 0)
                        {
                            int queueSize = queue.Count;
                            for (int k = 0; k < queueSize; k++)
                            {
                                var element = queue.Dequeue();
                                if (IsIslandElement(element.Row - 1, element.Column))
                                {
                                    grid[element.Row - 1][element.Column] = '2';
                                    queue.Enqueue(new Index(element.Row-1, element.Column));
                                }

                                if (IsIslandElement(element.Row + 1, element.Column))
                                {
                                    grid[element.Row + 1][element.Column] = '2';
                                    queue.Enqueue(new Index(element.Row + 1,element.Column));
                                }

                                if (IsIslandElement(element.Row, element.Column - 1))
                                {
                                    grid[element.Row][element.Column - 1] = '2';
                                    queue.Enqueue(new Index(element.Row, element.Column - 1));
                                }

                                if (IsIslandElement(element.Row, element.Column + 1))
                                {
                                    grid[element.Row][element.Column + 1] = '2';
                                    queue.Enqueue(new Index(element.Row, element.Column + 1));
                                }
                            }
                            
                        }
                    }
                }
            }

            return islandCount;

            bool IsIslandElement(int r, int c)
            {
                return IsValid(r, c) && grid[r][c] == '1';
            }
            bool IsValid(int r, int c)
            {
                return r >= 0 && r < numRows && c >= 0 && c < numColumns;
            }
        }
        
        public struct Index
        {
            public int Row;
            public int Column;

            public Index(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }
    }
}
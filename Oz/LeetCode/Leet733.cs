// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode;

public class Leet733
{
    public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
    {
        var rows = image.Length;
        var columns = image[0].Length;

        var vertexMatrix = new Vertex[rows][];
        for (var i = 0; i < rows; i++)
        {
            vertexMatrix[i] = new Vertex[columns];
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                vertexMatrix[i][j] = new Vertex {Row = i, Column = j, Color = image[i][j]};
            }
        }


        var sourceColor = image[sr][sc];
        var vertices = new Stack<Vertex>();

        vertexMatrix[sr][sc].Color = newColor;
        vertexMatrix[sr][sc].Visited = true;
        vertices.Push(vertexMatrix[sr][sc]);


        while (vertices.Any())
        {
            var vertex = vertices.Pop();

            var upRow = vertex.Row - 1;
            var downRow = vertex.Row + 1;
            var leftColumn = vertex.Column - 1;
            var rightColumn = vertex.Column + 1;

            if (upRow >= 0 && vertexMatrix[upRow][vertex.Column].Color == sourceColor &&
                !vertexMatrix[upRow][vertex.Column].Visited)
            {
                vertexMatrix[upRow][vertex.Column].Color = newColor;
                vertexMatrix[upRow][vertex.Column].Visited = true;
                vertices.Push(vertexMatrix[upRow][vertex.Column]);
            }

            if (downRow < rows && vertexMatrix[downRow][vertex.Column].Color == sourceColor &&
                !vertexMatrix[downRow][vertex.Column].Visited)
            {
                vertexMatrix[downRow][vertex.Column].Color = newColor;
                vertexMatrix[downRow][vertex.Column].Visited = true;
                vertices.Push(vertexMatrix[downRow][vertex.Column]);
            }

            if (leftColumn >= 0 && vertexMatrix[vertex.Row][leftColumn].Color == sourceColor &&
                !vertexMatrix[vertex.Row][leftColumn].Visited)
            {
                vertexMatrix[vertex.Row][leftColumn].Color = newColor;
                vertexMatrix[vertex.Row][leftColumn].Visited = true;
                vertices.Push(vertexMatrix[vertex.Row][leftColumn]);
            }

            if (rightColumn < columns && vertexMatrix[vertex.Row][rightColumn].Color == sourceColor &&
                !vertexMatrix[vertex.Row][rightColumn].Visited)
            {
                vertexMatrix[vertex.Row][rightColumn].Color = newColor;
                vertexMatrix[vertex.Row][rightColumn].Visited = true;
                vertices.Push(vertexMatrix[vertex.Row][rightColumn]);
            }
        }

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                image[i][j] = vertexMatrix[i][j].Color;
            }
        }

        return image;
    }

    public class Vertex
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public bool Visited { get; set; }

        public int Color { get; set; }
    }
}
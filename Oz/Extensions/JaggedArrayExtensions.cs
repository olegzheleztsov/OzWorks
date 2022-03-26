// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.Extensions;

public static class JaggedArrayExtensions
{
    public static string AsString<T>(this T[][] matrix)
    {
        if (matrix == null)
        {
            return string.Empty;
        }

        if (matrix.Length == 0)
        {
            return string.Empty;
        }

        var rows = matrix.Length;
        var columns = matrix[0].Length;

        var stringBuilder = new StringBuilder();
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                stringBuilder.Append(j != columns - 1
                    ? $"{matrix[i][j]}, "
                    : $"{matrix[i][j]}");
            }

            stringBuilder.AppendLine();
        }

        return stringBuilder.ToString();
    }
}
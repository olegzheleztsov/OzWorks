// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Leetcode.Solutions;

/*
    On an alphabet board, we start at position (0, 0), corresponding to character board[0][0].
    Here, board = ["abcde", "fghij", "klmno", "pqrst", "uvwxy", "z"], as shown in the diagram below.
    We may make the following moves:

    'U' moves our position up one row, if the position exists on the board;
    'D' moves our position down one row, if the position exists on the board;
    'L' moves our position left one column, if the position exists on the board;
    'R' moves our position right one column, if the position exists on the board;
    '!' adds the character board[r][c] at our current position (r, c) to the answer.
    (Here, the only positions that exist on the board are positions with letters on them.)

    Return a sequence of moves that makes our answer equal to target in the minimum number of moves.  You may return any path that does so.
 */
public class _1138
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public string AlphabetBoardPath(string target)
    {
        var positions = new Dictionary<char, Position>();
        var charIndex = 0;

        for (var r = 0; r < 5; r++)
        {
            for (var c = 0; c < 5; c++)
            {
                positions[(char)('a' + charIndex)] = new Position(r, c);
                charIndex++;
            }
        }

        positions.Add('z', new Position(5, 0));

        var pathBuilder = new StringBuilder();

        var previousChar = 'a';
        var previousPosition = positions[previousChar];
        foreach (var c in target)
        {
            if (previousChar == 'z' && c == 'z')
            {
                pathBuilder.Append('!');
                continue;
            }

            if (previousChar == 'z')
            {
                pathBuilder.Append('U');
                previousPosition = positions['u'];
            }

            var targetChar = c;
            if (targetChar == 'z')
            {
                targetChar = 'u';
            }

            pathBuilder.Append(BuildPath(previousPosition, positions[targetChar]));

            if (c == 'z')
            {
                pathBuilder.Remove(pathBuilder.Length - 1, 1);
                pathBuilder.Append("D!");
            }

            previousChar = c;
            previousPosition = positions[previousChar];
        }

        return pathBuilder.ToString();
    }

    private string BuildPath(Position startPosition, Position endPosition)
    {
        var vertOffset = endPosition.Row - startPosition.Row;
        var vertDirection = Direction.None;
        if (vertOffset > 0)
        {
            vertDirection = Direction.Down;
        }
        else if (vertOffset < 0)
        {
            vertDirection = Direction.Up;
        }

        var horzOffset = endPosition.Col - startPosition.Col;
        var horzDirection = Direction.None;

        if (horzOffset > 0)
        {
            horzDirection = Direction.Right;
        }
        else if (horzOffset < 0)
        {
            horzDirection = Direction.Left;
        }

        var row = startPosition.Row;
        var pathBuilder = new StringBuilder();

        while (row != endPosition.Row)
        {
            switch (vertDirection)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                {
                    pathBuilder.Append('U');
                    row--;
                }
                    break;
                case Direction.Down:
                {
                    pathBuilder.Append('D');
                    row++;
                }
                    break;
            }
        }

        var col = startPosition.Col;
        while (col != endPosition.Col)
        {
            switch (horzDirection)
            {
                case Direction.None:
                    break;
                case Direction.Left:
                {
                    pathBuilder.Append('L');
                    col--;
                }
                    break;
                case Direction.Right:
                {
                    pathBuilder.Append('R');
                    col++;
                }
                    break;
            }
        }

        pathBuilder.Append('!');
        return pathBuilder.ToString();
    }

    public record Position(int Row, int Col);
}
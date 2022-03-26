#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace Oz.Rob;

public class KnightTourSolver
{
    private readonly int _columns;
    private readonly int[] _deltaColumns = {-1, 1, 2, 2, 1, -1, -2, -2};
    private readonly int[] _deltaRows = {-2, -2, -1, 1, 2, 2, 1, -1};
    private readonly int _rows;

    public KnightTourSolver(int rows, int columns)
    {
        _rows = rows;
        _columns = columns;
    }

    public KnightTourResult Solve()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var success = KnightsTour(0, 0, new int[_rows, _columns], 0);
        stopwatch.Stop();
        return new KnightTourResult(success, stopwatch.Elapsed);
    }

    public KnightTourResult SolveWansdorff()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var success = KnightsTourHeuristic(0, 0, new int[_rows, _columns], 0);
        stopwatch.Stop();
        return new KnightTourResult(success, stopwatch.Elapsed);
    }

    private int NumberOfAllowedPositionsForPoint(int row, int column, int oldRow, int oldColumn, int[,] moves)
    {
        var count = 0;
        for (var i = 0; i < _deltaRows.Length; i++)
        {
            var r = row + _deltaRows[i];
            var c = column + _deltaColumns[i];
            if (r != oldRow && c != oldColumn && r != row && c != column)
            {
                if (r >= 0 && r < _rows && c >= 0 && c < _columns && moves[r, c] == 0)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private (int row, int column) SelectNextPosition(int oldRow, int oldColumn, int[,] moves)
    {
        List<(int row, int column, int movesCount)> results =
            new List<(int _rows, int column, int movesCount)>(_deltaRows.Length);
        for (var i = 0; i < _deltaRows.Length; i++)
        {
            var r = oldRow + _deltaRows[i];
            var c = oldColumn + _deltaColumns[i];
            if (r >= 0 && r < _rows && c >= 0 && c < _columns && moves[r, c] == 0)
            {
                results.Add((r, c, NumberOfAllowedPositionsForPoint(r, c, oldRow, oldColumn, moves)));
            }
        }

        Console.WriteLine(string.Join(", ", results));
        var (row, column, movesCount) = results.OrderBy(v => v.movesCount).FirstOrDefault(v => v.movesCount > 0);
        return movesCount == 0 ? (-1, -1) : (row, column);
    }

    private bool KnightsTour(int row, int col, int[,] moveNumber, int numberMovesTaken)
    {
        numberMovesTaken++;
        moveNumber[row, col] = numberMovesTaken;

        if (numberMovesTaken == _rows * _columns)
        {
            return true;
        }

        for (var i = 0; i < 8; i++)
        {
            var r = row + _deltaRows[i];
            var c = col + _deltaColumns[i];
            if (r >= 0 && r < _rows && c >= 0 && c < _columns && moveNumber[r, c] == 0)
            {
                if (KnightsTour(r, c, moveNumber, numberMovesTaken))
                {
                    return true;
                }
            }
        }

        moveNumber[row, col] = 0;
        return false;
    }

    private bool KnightsTourHeuristic(int row, int col, int[,] moveNumber, int numberMovesTaken)
    {
        numberMovesTaken++;
        moveNumber[row, col] = numberMovesTaken;

        if (numberMovesTaken == _rows * _columns)
        {
            return true;
        }


        var (newRow, newCol) = SelectNextPosition(row, col, moveNumber);
        if (newRow >= 0 && newCol >= 0)
        {
            if (KnightsTourHeuristic(newRow, newCol, moveNumber, numberMovesTaken))
            {
                return true;
            }
        }

        moveNumber[row, col] = 0;
        return false;
    }
}
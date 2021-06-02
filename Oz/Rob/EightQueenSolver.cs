#region

using System;
using System.Diagnostics;

#endregion

namespace Oz.Rob
{
    public class EightQueenSolver
    {
        private readonly int _size;

        public EightQueenSolver(int size)
        {
            _size = size;
        }

        public EightQueenResult Solve()
        {
            var spotTaken = new bool[_size, _size];
            var positionsVerified = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var found = Solve(spotTaken, 0, () => { positionsVerified++; });
            stopwatch.Stop();
            return new EightQueenResult(spotTaken, found, stopwatch.Elapsed, positionsVerified);
        }

        public EightQueenResult SolveOptimized()
        {
            var spotTaken = new bool[_size, _size];
            var positionsVerified = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var found = SolveOptimized(spotTaken, new int[_size, _size], 0, () => { positionsVerified++; });
            stopwatch.Stop();
            return new EightQueenResult(spotTaken, found, stopwatch.Elapsed, positionsVerified);
        }

        private bool Solve(bool[,] spotTaken, int numberQueensPositioned, Action setSpotAction)
        {
            if (!IsLegal(spotTaken))
            {
                return false;
            }

            if (numberQueensPositioned == _size)
            {
                return true;
            }


            for (var row = 0; row < _size; row++)
            {
                if (!HasQueenAtRow(row, spotTaken))
                {
                    for (var col = 0; col < _size; col++)
                    {
                        if (!spotTaken[row, col])
                        {
                            spotTaken[row, col] = true;
                            setSpotAction();
                            if (Solve(spotTaken, numberQueensPositioned + 1, setSpotAction))
                            {
                                return true;
                            }

                            spotTaken[row, col] = false;
                        }
                    }
                }
            }

            return false;
        }

        private bool SolveOptimized(bool[,] spotTaken, int[,] attackersCount, int numberQueensPositioned,
            Action setSpotAction)
        {
            if (!IsLegalOptimized(spotTaken, attackersCount))
            {
                return false;
            }

            if (numberQueensPositioned == _size)
            {
                return true;
            }

            for (var row = 0; row < _size; row++)
            {
                if (!HasQueenAtRow(row, spotTaken))
                {
                    for (var col = 0; col < _size; col++)
                    {
                        if (!spotTaken[row, col] && attackersCount[row, col] == 0)
                        {
                            spotTaken[row, col] = true;
                            MarkAttackedSpots(attackersCount, row, col, 1);
                            setSpotAction();
                            if (Solve(spotTaken, numberQueensPositioned + 1, setSpotAction))
                            {
                                return true;
                            }

                            spotTaken[row, col] = false;
                            MarkAttackedSpots(attackersCount, row, col, -1);
                        }
                    }
                }
            }

            return false;
        }

        private bool HasQueenAtRow(int row, bool[,] spotTaken)
        {
            for (var j = 0; j < _size; j++)
            {
                if (spotTaken[row, j])
                {
                    return true;
                }
            }

            return false;
        }

        private void MarkAttackedSpots(int[,] numAttacks, int row, int col, int amount)
        {
            // Mark the row and column.
            for (var c = 0; c < _size; c++) numAttacks[row, c] += amount;
            for (var r = 0; r < _size; r++) numAttacks[r, col] += amount;

            // Mark the upper left/lower right diagonal.
            var minDx1 = -Math.Min(row, col);
            var maxDx1 = Math.Min(_size - row - 1, _size - col - 1);
            for (var dx = minDx1; dx <= maxDx1; dx++)
                numAttacks[row + dx, col + dx] += amount;

            // Mark the upper right/lower left diagonal.
            var minDx2 = -Math.Min(row, _size - col - 1);
            var maxDx2 = Math.Min(_size - row - 1, col);
            for (var dx = minDx2; dx <= maxDx2; dx++)
                numAttacks[row + dx, col - dx] += amount;
        }

        private bool IsLegalOptimized(bool[,] spotTaken, int[,] attackersCount)
        {
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    if (spotTaken[i, j] && attackersCount[i, j] > 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsLegal(bool[,] spotTaken)
        {
            if (IsRowsValid(spotTaken) && IsColumnsValid(spotTaken) && IsMainDiagonalsValid(spotTaken) &&
                IsSecondaryDiagonalsValid(spotTaken))
            {
                return true;
            }

            return false;
        }

        private bool IsRowsValid(bool[,] spotTaken)
        {
            for (var i = 0; i < _size; i++)
            {
                var count = 0;
                for (var j = 0; j < _size; j++)
                {
                    if (spotTaken[i, j])
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsColumnsValid(bool[,] spotTaken)
        {
            for (var j = 0; j < _size; j++)
            {
                var count = 0;
                for (var i = 0; i < _size; i++)
                {
                    if (spotTaken[i, j])
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsMainDiagonalsValid(bool[,] spotTaken)
        {
            for (var k = 0; k < _size; k++)
            {
                var count = 0;
                var i = k;
                var j = 0;
                for (; i >= 0; i--, j++)
                {
                    if (spotTaken[i, j])
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsSecondaryDiagonalsValid(bool[,] spotTaken)
        {
            for (var k = 0; k < _size; k++)
            {
                var count = 0;
                var i = _size - 1 - k;
                var j = 0;
                for (; i < _size; i++, j++)
                {
                    if (spotTaken[i, j])
                    {
                        count++;
                    }
                }

                if (count >= 2)
                {
                    return false;
                }
            }

            return true;
        }


        public static void TestValidation()
        {
            var arr1 = new bool[8, 8];
            arr1[1, 2] = true;
            arr1[1, 7] = true;

            var solver = new EightQueenSolver(8);
            Console.WriteLine($"should invalid: {solver.IsLegal(arr1)}");

            var arr2 = new bool[8, 8];
            arr2[3, 4] = true;
            arr2[4, 4] = true;
            Console.WriteLine($"should invalid: {solver.IsLegal(arr2)}");

            var arr3 = new bool[8, 8];
            arr3[2, 1] = true;
            arr3[0, 3] = true;
            Console.WriteLine($"should invalid: {solver.IsLegal(arr3)}");

            var arr4 = new bool[8, 8];
            arr4[2, 0] = true;
            arr4[4, 2] = true;
            Console.WriteLine($"should invalid: {solver.IsLegal(arr4)}");

            var arr5 = new bool[8, 8];
            arr5[4, 4] = true;
            arr5[0, 1] = true;
            Console.WriteLine($"should be valid: {solver.IsLegal(arr5)}");
        }
    }
}
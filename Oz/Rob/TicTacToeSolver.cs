#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Oz.Rob
{
    public class TicTacToeSolver
    {
        public enum BoardValue
        {
            Loose = 1,
            Draw = 2,
            Unclear = 3,
            Win = 4,
            Infinity = int.MaxValue
        }

        private const int BoardSize = 3;
        private const int MaxDepth = 1000;
        private readonly char[,] _board;

        public TicTacToeSolver()
        {
            _board = new char[BoardSize, BoardSize];
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    _board[i, j] = ' ';
                }
            }
        }

        public IEnumerable<Result> Play()
        {
            var player1 = new Player('X');
            var player2 = new Player('O');
            
            while (GetAllowedMoves(_board).Any())
            {
                Solve(_board, out var bestMove, out var bestValue, player1, player2, 0, MaxDepth);
                if (bestMove == null)
                {
                    yield break;
                }
                UpdateBoardWithMove(_board, bestMove, player1);
                var result = new Result(bestMove, bestValue, _board);
                var temp = player1;
                player1 = player2;
                player2 = temp;
                yield return result;
            }
        }
        

        private static void Solve(char[,] board, out Move bestMove, out BoardValue bestValue, Player player1,
            Player player2,
            int depth, int maxDepth)
        {
            if (depth > maxDepth)
            {
                bestValue = BoardValue.Unclear;
                bestMove = null;
                return;
            }

            var lowestValue = BoardValue.Infinity;
            Move lowestMove = null;
            foreach (var move in GetAllowedMoves(board))
            {
                UpdateBoardWithMove(board, move, player1);
                var boardValue = AnalyzeForPlayerMove(board, player1);
                if (boardValue is BoardValue.Win or BoardValue.Loose or BoardValue.Draw)
                {
                    lowestValue = boardValue;
                    lowestMove = move;
                }
                else
                {
                    Solve(board, out var testMove, out var testValue, player2, player1, depth, maxDepth);
                    if ((int) testValue < (int) lowestValue)
                    {
                        lowestValue = testValue;
                        lowestMove = testMove;
                    }
                }

                RestoreBoard(board, move);
            }

            bestMove = lowestMove;
            bestValue = lowestValue switch
            {
                BoardValue.Win => BoardValue.Loose,
                BoardValue.Loose => BoardValue.Win,
                _ => lowestValue
            };
        }

        private static void RestoreBoard(char[,] board, Move move)
        {
            var (row, col) = move;
            board[row, col] = ' ';
        }

        private static BoardValue AnalyzeForPlayerMove(char[,] board, Player player)
        {
            for (var row = 0; row < BoardSize; row++)
            {
                var rowChar = TryCheckRowForEnd(board, row);
                if (rowChar != null)
                {
                    return rowChar.Value == player.Name ? BoardValue.Win : BoardValue.Loose;
                }
            }

            for (var col = 0; col < BoardSize; col++)
            {
                var colChar = TryCheckColumnForEnd(board, col);
                if (colChar != null)
                {
                    return colChar.Value == player.Name ? BoardValue.Win : BoardValue.Loose;
                }
            }

            var diagChar = TryCheckDiagonalForEnd(board);
            if (diagChar != null)
            {
                return diagChar.Value == player.Name ? BoardValue.Win : BoardValue.Loose;
            }

            return !GetAllowedMoves(board).Any() ? BoardValue.Draw : BoardValue.Unclear;
        }

        private static char? TryCheckRowForEnd(char[,] board, int row)
        {
            var firstChar = board[row, 0];
            for (var i = 0; i < BoardSize; i++)
            {
                if (board[row, i] != firstChar)
                {
                    return null;
                }
            }

            return firstChar;
        }

        private static char? TryCheckColumnForEnd(char[,] board, int column)
        {
            var firstChar = board[0, column];
            for (var i = 0; i < BoardSize; i++)
            {
                if (board[i, column] != firstChar)
                {
                    return null;
                }
            }

            return firstChar;
        }

        private static char? TryCheckDiagonalForEnd(char[,] board)
        {
            var isWin = true;
            var firstChar = board[0, 0];
            for (var i = 0; i < BoardSize; i++)
            {
                if (board[i, i] != firstChar)
                {
                    isWin = false;
                    break;
                }
            }

            if (isWin)
            {
                return firstChar;
            }

            firstChar = board[BoardSize - 1, 0];
            var colIndex = 0;
            var rowIndex = BoardSize - 1;

            while (colIndex < BoardSize)
            {
                if (board[rowIndex, colIndex] != firstChar)
                {
                    return null;
                }

                rowIndex--;
                colIndex++;
            }

            return firstChar;
        }

        private static void UpdateBoardWithMove(char[,] board, Move move, Player player)
        {
            board[move.X, move.Y] = player.Name;
        }

        private static IEnumerable<Move> GetAllowedMoves(char[,] board)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        yield return new Move(i, j);
                    }
                }
            }
        }

        public record Result(Move BestMove, BoardValue BestValue, char[,] Board);

        public record Move(int X, int Y);

        public record Player(char Name);
    }
}
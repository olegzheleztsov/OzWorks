using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Numerics
{
    public class LongestCommonSubstring<T>
    {
        private readonly T[] _firstString;
        private readonly T[] _secondString;

        public LongestCommonSubstring(IReadOnlyList<T> firstString, IReadOnlyList<T> secondString)
        {
            _firstString = new T[firstString.Count + 1];
            _secondString = new T[secondString.Count + 1];

            for (var i = 0; i < firstString.Count; i++)
            {
                _firstString[i + 1] = firstString[i];
            }

            for (var i = 0; i < secondString.Count; i++)
            {
                _secondString[i + 1] = secondString[i];
            }
        }

        public T[] GetLongestSubstring()
        {
            var (_, moves) = _FindLengths();
            var substring = new List<T>();
            _ComputeLongestSubstring(moves, _firstString.Length - 1, _secondString.Length - 1, substring);
            return substring.ToArray();
        }

        public T[] GetLongestSubstring2()
        {
            var (lengths, _) = _FindLengths();
            return _ComputeLongestSubstring(lengths);
        }

        public T[] GetLongestSubstringTopDown()
        {
            var (_, moves) = _FindLengthsTopDown();
            var substring = new List<T>();
            _ComputeLongestSubstring(moves, _firstString.Length - 1, _secondString.Length - 1, substring);
            return substring.ToArray();
        }

        private void _ComputeLongestSubstring(char[,] moves, int i, int j, ICollection<T> substring)
        {
            if (i == 0 || j == 0)
            {
                return;
            }

            switch (moves[i, j])
            {
                case '\\':
                    _ComputeLongestSubstring(moves, i - 1, j - 1, substring);
                    substring.Add(_firstString[i]);
                    break;
                case '|':
                    _ComputeLongestSubstring(moves, i - 1, j, substring);
                    break;
                default:
                    _ComputeLongestSubstring(moves, i, j - 1, substring);
                    break;
            }
        }

        private T[] _ComputeLongestSubstring(int[,] lengths)
        {
            var xLen = _firstString.Length - 1;
            var yLen = _secondString.Length - 1;
            var n = lengths[xLen, yLen];
            var s = new T[n + 1];
            var i = xLen;
            var j = yLen;
            while (i > 0 && j > 0)
            {
                if (_firstString[i].Equals(_secondString[j]))
                {
                    s[n] = _firstString[i];
                    n--;
                    i--;
                    j--;
                }
                else if (lengths[i - 1, j] >= lengths[i, j - 1])
                {
                    i--;
                }
                else
                {
                    j--;
                }
            }

            var result = new List<T>();
            for (var ind = 1; ind < s.Length; ind++)
            {
                result.Add(s[ind]);
            }

            return result.ToArray();
        }

        private (int[,] lengths, char[,] moves) _FindLengthsTopDown()
        {
            var firstStringLength = _firstString.Length - 1;
            var secondStringLength = _secondString.Length - 1;
            var moves = (char[,]) Array.CreateInstance(typeof(char), new[] {firstStringLength, secondStringLength},
                new[] {1, 1});
            var lengths = (int[,]) Array.CreateInstance(typeof(int),
                new[] {firstStringLength + 1, secondStringLength + 1}, new[] {0, 0});
            var _ = _FindLengthsTopDownAux(new ArraySegment<T>(_firstString, 1, firstStringLength),
                new ArraySegment<T>(_secondString, 1, secondStringLength), lengths, moves);
            return (lengths, moves);
        }


        private int _FindLengthsTopDownAux(ArraySegment<T> firstStringPrefix, ArraySegment<T> secondStringPrefix,
            int[,] lengths, char[,] moves)
        {
            var firstPrefixLength = firstStringPrefix.Count;
            var secondPrefixLength = secondStringPrefix.Count;

            if (lengths[firstPrefixLength, secondPrefixLength] != 0 || firstPrefixLength == 0 ||
                secondPrefixLength == 0)
            {
                return lengths[firstPrefixLength, secondPrefixLength];
            }

            if (firstStringPrefix.Array[firstPrefixLength].Equals(secondStringPrefix.Array[secondPrefixLength]))
            {
                moves[firstPrefixLength, secondPrefixLength] = '\\';
                lengths[firstPrefixLength, secondPrefixLength] = _FindLengthsTopDownAux(
                    new ArraySegment<T>(_firstString, 1, firstPrefixLength - 1),
                    new ArraySegment<T>(_secondString, 1, secondPrefixLength - 1), lengths, moves) + 1;
                return lengths[firstPrefixLength, secondPrefixLength];
            }

            var left = _FindLengthsTopDownAux(new ArraySegment<T>(_firstString, 1, firstPrefixLength - 1),
                new ArraySegment<T>(_secondString, 1, secondPrefixLength), lengths, moves);
            var right = _FindLengthsTopDownAux(new ArraySegment<T>(_firstString, 1, firstPrefixLength),
                new ArraySegment<T>(_secondString, 1, secondPrefixLength - 1), lengths, moves);

            if (left >= right)
            {
                moves[firstPrefixLength, secondPrefixLength] = '|';
                lengths[firstPrefixLength, secondPrefixLength] = left;
                return left;
            }

            moves[firstPrefixLength, secondPrefixLength] = '-';
            lengths[firstPrefixLength, secondPrefixLength] = right;
            return right;
        }

        private (int[,] lengths, char[,] moves) _FindLengths()
        {
            var firstStringLength = _firstString.Length - 1;
            var secondStringLength = _secondString.Length - 1;
            var moves = (char[,]) Array.CreateInstance(typeof(char), new[] {firstStringLength, secondStringLength},
                new[] {1, 1});
            var lengths = (int[,]) Array.CreateInstance(typeof(int),
                new[] {firstStringLength + 1, secondStringLength + 1}, new[] {0, 0});

            for (var i = 1; i <= firstStringLength; i++)
            {
                lengths[i, 0] = 0;
            }

            for (var j = 0; j <= secondStringLength; j++)
            {
                lengths[0, j] = 0;
            }

            for (var i = 1; i <= firstStringLength; i++)
            {
                for (var j = 1; j <= secondStringLength; j++)
                {
                    if (_firstString[i].Equals(_secondString[j]))
                    {
                        lengths[i, j] = lengths[i - 1, j - 1] + 1;
                        moves[i, j] = '\\';
                    }
                    else if (lengths[i - 1, j] >= lengths[i, j - 1])
                    {
                        lengths[i, j] = lengths[i - 1, j];
                        moves[i, j] = '|';
                    }
                    else
                    {
                        lengths[i, j] = lengths[i, j - 1];
                        moves[i, j] = '-';
                    }
                }
            }

            return (lengths, moves);
        }
    }
}
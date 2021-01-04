using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Numerics
{
    public class RodCutter
    {
        private readonly double[] _prices;

        public RodCutter(double[] prices)
        {
            _prices = prices;
        }

        public double BottomUpCutRod(int rodLength)
        {
            var memo = new double[rodLength + 1];
            memo[0] = 0.0;

            for (var j = 1; j <= rodLength; j++)
            {
                var currentValue = double.NegativeInfinity;
                for (var i = 1; i <= j; i++)
                {
                    currentValue = Math.Max(currentValue, _prices[i - 1] + memo[j - i]);
                }

                memo[j] = currentValue;
            }

            return memo[rodLength];
        }

        public double MemoizedCurRod(int rodLength)
        {
            if (rodLength > _prices.Length)
            {
                throw new ArgumentException(
                    $"{nameof(rodLength)} should be less or equal than: {_prices.Length}, actual value: {rodLength}");
            }

            var memo = new double[rodLength + 1];
            for (var i = 0; i < memo.Length; i++)
            {
                memo[i] = double.NegativeInfinity;
            }

            return MemoizedCutRodAuxiliary(memo, rodLength);
        }

        public IEnumerable<int> GetOptimalCutting(int rodLength)
        {
            var (memo, sequence) = ExtendedBottomUpCutRod(rodLength);
            var cutting = new List<int>();
            while (rodLength > 0)
            {
                cutting.Add(sequence[rodLength]);
                rodLength -= sequence[rodLength];
            }

            return cutting;
        }

        private double MemoizedCutRodAuxiliary(double[] memo, int rodLength)
        {
            if (memo[rodLength] >= 0)
            {
                return memo[rodLength];
            }

            double currentMax = 0;
            if (rodLength == 0)
            {
                currentMax = 0;
            }
            else
            {
                currentMax = int.MinValue;
                for (var i = 1; i <= rodLength; i++)
                {
                    currentMax = Math.Max(currentMax, _prices[i - 1] + MemoizedCutRodAuxiliary(memo, rodLength - i));
                }
            }

            memo[rodLength] = currentMax;
            return currentMax;
        }

        private (double[] memo, int[] sequence) ExtendedBottomUpCutRod(int rodLength)
        {
            var memo = new double[rodLength + 1];
            var sequence = new int[rodLength + 1];
            memo[0] = 0.0;
            for (var j = 1; j <= rodLength; j++)
            {
                var currentValue = double.NegativeInfinity;
                for (var i = 1; i <= j; i++)
                {
                    if (currentValue < _prices[i - 1] + memo[j - i])
                    {
                        currentValue = _prices[i - 1] + memo[j - i];
                        sequence[j] = i;
                    }
                }

                memo[j] = currentValue;
            }

            return (memo, sequence);
        }
    }
}
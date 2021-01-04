using System;
using System.Collections.Generic;

namespace Oz.Algorithms.Matrices
{
    public class TopDownMatrixChainOrder : IMatrixChainOrder
    {
        private const int Infinity = int.MaxValue;

        public (int[,] costs, int[,] sequence) Find(List<FloatMatrix> matrices)
        {
            var dimensions = matrices.GetMatrixCollectionDimensions();
            var countOfMatrices = dimensions.Length - 1;
            var costs = (int[,]) Array.CreateInstance(typeof(int), new[] {countOfMatrices, countOfMatrices},
                new[] {1, 1});
            var sequence = (int[,]) Array.CreateInstance(typeof(int), new[] {countOfMatrices - 1, countOfMatrices - 1},
                new[] {1, 2});

            costs.SetElementsToValue(Infinity);
            var _ = LookupChain(costs, sequence, dimensions, 1, countOfMatrices);

            for (var i = 1; i <= countOfMatrices; i++)
            {
                for (var j = 1; j <= countOfMatrices; j++)
                {
                    if (costs[i, j] == Infinity)
                    {
                        costs[i, j] = 0;
                    }
                }
            }

            return (costs, sequence);
        }

        private static int LookupChain(int[,] costs, int[,] sequence, int[] dimensions, int i,
            int j)
        {
            if (costs[i, j] < Infinity)
            {
                return costs[i, j];
            }

            if (i == j)
            {
                costs[i, j] = 0;
            }
            else
            {
                for (var k = i; k < j; k++)
                {
                    var q = LookupChain(costs, sequence, dimensions, i, k)
                            + LookupChain(costs, sequence, dimensions, k + 1, j)
                            + dimensions[i - 1] * dimensions[k] * dimensions[j];
                    if (q < costs[i, j])
                    {
                        costs[i, j] = q;
                        sequence[i, j] = k;
                    }
                }
            }

            return costs[i, j];
        }
    }
}
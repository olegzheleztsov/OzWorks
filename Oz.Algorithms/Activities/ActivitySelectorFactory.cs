using System;

namespace Oz.Algorithms.Activities
{
    public static class ActivitySelectorFactory
    {
        public static IActivitySelector<T> Create<T>(AlgorithmKind algorithmKind = AlgorithmKind.Recursive) =>
            algorithmKind switch
            {
                AlgorithmKind.Iterative => new IterativeActivitySelector<T>(),
                AlgorithmKind.Recursive => new RecursiveActivitySelector<T>(),
                AlgorithmKind.Inverse => new InverseActivitySelector<T>(),
                _ => throw new NotImplementedException($"Not implemented algorithm kind: {algorithmKind}")
            };
    }
}
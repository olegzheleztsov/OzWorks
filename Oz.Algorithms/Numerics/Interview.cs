using System;

namespace Oz.Algorithms.Numerics
{
    public class Interview<T>
    {
        private readonly T[] _candidates;
        private readonly Func<T, int> _rank;
        public const int MaxCandidates = 1200;
        

        public Interview(T[] candidates, Func<T, int> rank)
        {
            _candidates = candidates ?? throw new NullReferenceException(nameof(candidates));
            _rank = rank;
            if (_candidates.Length == 0)
            {
                throw new ArgumentException("candidates count must be greater than zero");
            }

            if (_candidates.Length > MaxCandidates)
            {
                throw new ArgumentException($"Candidates count should be less than or equal of {MaxCandidates}");
            }
        }

        public (T canditate, int index, int hireCount) BestCandidate
        {
            get
            {
                var bestIndex = 0;
                var hireCount = 1;
                for (var i = 1; i < _candidates.Length; i++)
                {
                    if (_rank(_candidates[i]) > _rank(_candidates[bestIndex]))
                    {
                        bestIndex = i;
                        hireCount++;
                    }
                }

                return (_candidates[bestIndex], bestIndex, hireCount);
            }
        }
    }
}
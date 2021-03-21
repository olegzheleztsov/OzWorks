using System;
using System.Collections.Generic;
using System.Linq;
using Oz.Algorithms.Numerics;

namespace Oz.Algorithms.Strings
{
    public static class StringWrapperUtils
    {
        public static IEnumerable<int> MatchNaive(StringWrapper source, StringWrapper pattern)
        {
            if (StringWrapper.IsNullOrEmpty(pattern))
            {
                return new List<int>();
            }

            if (source.Length < pattern.Length)
            {
                return new List<int>();
            }
            
            var sourceLength = source.Length;
            var patternLength = pattern.Length;
            var resultIndices = new List<int>();
            
            for (var s = 0; s <= (sourceLength - patternLength); s++)
            {
                if (source.SubstringEquals(s + 1, s + patternLength, pattern, 1))
                {
                    resultIndices.Add(s + 1);
                }
            }

            return resultIndices;
        }

        public static List<int> MatchRabinKarp(
            StringWrapper source, 
            StringWrapper pattern,
            int primeNumber,
            Dictionary<char, int> mapping)
        {
            var alphabetSize = mapping.Count;
            var sourceLength = source.Length;
            var patternLength = pattern.Length;
            var exponentValue = (int)(new ModularExponentiation(alphabetSize, patternLength - 1, primeNumber).Value);
            var patternShift = 0;
            var sourceShift = 0;
            for (var i = 1; i <= patternLength; i++)
            {
                patternShift = (alphabetSize * patternShift + mapping[pattern[i]]) % primeNumber;
                sourceShift = (alphabetSize * sourceShift + mapping[source[i]]) % primeNumber;
            }

            var resultPositions = new List<int>();
            for (var s = 0; s <= (sourceLength - patternLength); s++)
            {
                if (patternShift == sourceShift)
                {
                    if (source.SubstringEquals(s + 1, s + patternLength, pattern, 1))
                    {
                        resultPositions.Add(s + 1);
                    }
                }

                if (s < (sourceLength - patternLength))
                {
                    sourceShift = (alphabetSize * (sourceShift - mapping[source[s + 1]] * exponentValue) + mapping[source[s + patternLength + 1]]) %
                         primeNumber;
                }
            }

            return resultPositions;
        }

        public  static List<int> MatchFiniteAutomation(StringWrapper source, StringWrapper pattern, IEnumerable<char> alphabet)
        {
            var transitionFunctionValues = ComputeTransitionFunction(pattern, alphabet);

            int GetTransitionValue(int state, char symbol)
            {
                return transitionFunctionValues.FirstOrDefault(v => v.State == state && v.Symbol == symbol)?.Value ??
                       throw new InvalidOperationException(
                           $"Didn't found transition function value for (state, symbol): ({state}, {symbol})");
            }

            var sourceLength = source.Length;
            var q = 0;
            var resultPositions = new List<int>();
            for (var i = 1; i <= sourceLength; i++)
            {
                q = GetTransitionValue(q, source[i]);
                if (q == pattern.Length)
                {
                    resultPositions.Add(i - pattern.Length + 1);
                }
            }

            return resultPositions;
        }

        private static List<StringTransitionFunctionValue> ComputeTransitionFunction(
            StringWrapper pattern,
            IEnumerable<char> alphabet)
        {
            var patternLength = pattern.Length;
            var computeFunction = new List<StringTransitionFunctionValue>();
            var alphabetArray = alphabet as char[] ?? alphabet.ToArray();
            for (int q = 0; q <= patternLength; q++)
            {
                foreach (var ch in alphabetArray)
                {
                    var k = Math.Min(patternLength + 1, q + 2);
                    do
                    {
                        k--;
                    } while (!(new StringWrapper(pattern.Prefix(q) + new StringWrapper(ch.ToString())).IsSuffix(pattern.Prefix(k))));
                    computeFunction.Add(new StringTransitionFunctionValue(q, ch, k));
                }
            }

            return computeFunction;
        }

        public static List<int> MatchKnuthMorrisPratt(StringWrapper source, StringWrapper pattern)
        {
            var sourceLength = source.Length;
            var patternLength = pattern.Length;
            var prefixFunc = ComputePrefixFunction(pattern);
            var q = 0;
            var results = new List<int>();
            for (var i = 1; i <= sourceLength; i++)
            {
                while (q > 0 && pattern[q + 1] != source[i])
                {
                    q = prefixFunc[q];
                }

                if (pattern[q + 1] == source[i])
                {
                    q++;
                }

                if (q == patternLength)
                {
                    results.Add(i - patternLength + 1);
                    q = prefixFunc[q];
                }
            }

            return results;
        }

        private static Dictionary<int, int> ComputePrefixFunction(StringWrapper pattern)
        {
            var patternLength = pattern.Length;
            var preFunc = new Dictionary<int, int>();
            for (var i = 1; i <= patternLength; i++)
            {
                preFunc[i] = 0;
            }
            preFunc[1] = 0;
            var k = 0;

            for (var q = 2; q <= patternLength; q++)
            {
                while (k > 0 && pattern[k + 1] != pattern[q])
                {
                    k = preFunc[k];
                }

                if (pattern[k + 1] == pattern[q])
                {
                    k++;
                }

                preFunc[q] = k;
            }

            return preFunc;
        }
    }

    public record StringTransitionFunctionValue(int State, char Symbol, int Value);
    
    
}
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Oz.Rob
{
    public class DiceStatistics
    {
        private readonly int _numberOfTries;
        private readonly Dice[] _dices;

        public DiceStatistics(int numberOfTries)
        {
            _numberOfTries = numberOfTries;
            _dices = new Dice[2];
            for (int i = 0; i < _dices.Length; i++)
            {
                _dices[i] = new Dice();
            }
        }

        public Dictionary<int, int> CollectStatistics()
        {
            var statistics = new Dictionary<int, int>();
            for (var i = 0; i < _numberOfTries; i++)
            {
                var sum = _dices.Sum(dice => dice.Roll());
                if (statistics.ContainsKey(sum))
                {
                    statistics[sum]++;
                }
                else
                {
                    statistics[sum] = 1;
                }
            }

            return statistics;
        }

        public ImmutableDictionary<int, double> CollectNormalizedStatistics(int normalizeFactor)
        {
            var statistics = CollectStatistics();
            var normalizedStatistics = statistics.ToImmutableDictionary(
                kvp => kvp.Key,
                kvp => ((double) kvp.Value / (double) _numberOfTries) * normalizeFactor
            );
            return normalizedStatistics;
        }
    }
}
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Oz.Rob
{
    public class DiceStatisticsDrawer
    {
        private readonly ImmutableDictionary<int, double> _values;

        public DiceStatisticsDrawer(ImmutableDictionary<int, double> values)
        {
            _values = values;
        }

        public void Draw() => Draw(_values);

        private void Draw(ImmutableDictionary<int, double> values)
        {
            foreach (var (key, value) in values.OrderBy(kvp => kvp.Key))
            {
                DrawLine(key, value);
            }
        }

        private void DrawLine(int number, double value)
        {
            Console.Write($"{number:00}:{new string('*', (int)Math.Round(value))}");
            Console.WriteLine();
        }
    }
}
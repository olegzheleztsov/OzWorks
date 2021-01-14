using System.Linq;

namespace Oz.Algorithms.Numerics
{
    public class BinaryCounter
    {
        private readonly bool[] _values;

        public BinaryCounter(bool[] values)
        {
            _values = values;
        }

        public void Decrement()
        {
            if (_values[0])
            {
                _values[0] = false;
            }
            else
            {
                var nonZeroIndex = -1;
                for (var i = 1; i < _values.Length; i++)
                {
                    if (_values[i])
                    {
                        nonZeroIndex = i;
                        break;
                    }
                }

                if (nonZeroIndex >= 0)
                {
                    for (var i = nonZeroIndex; i >= 0; i--)
                    {
                        _values[i] = !_values[i];
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Join(" ", _values.Select((k, i) => (k, i)).OrderBy(obj => -obj.i).Select(v => v.k ? 1 : 0));
        }
    }
}
using System;
using Oz.Algorithms;

namespace Oz
{
    public class ArrayGenerator
    {
        private readonly int _maxValue;
        private readonly DefaultRandomSource _defaultRandomSource = new DefaultRandomSource();
        
        public ArrayGenerator(int maxValue = int.MaxValue -1)
        {
            _maxValue = maxValue;
        }
        public ArrayElementData[] Generate(int size)
        {
            var array = new ArrayElementData[size];
            for (var i = 0; i < size; i++)
            {
                array[i] = new ArrayElementData(_defaultRandomSource.RandomValue(1, _maxValue + 1));
            }

            return array;
        }
    }

    public class ArrayElementData
    {
        public int Value { get; }

        public ArrayElementData(int value)
            => Value = value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public static Comparison<ArrayElementData> Comparison { get; } 
            = (a, b) => a.Value.CompareTo(b.Value);
    }
}
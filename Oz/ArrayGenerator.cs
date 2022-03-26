using Oz.Algorithms;
using System;

namespace Oz;

public class ArrayGenerator
{
    private readonly DefaultRandomSource _defaultRandomSource = new();
    private readonly int _maxValue;

    public ArrayGenerator(int maxValue = int.MaxValue - 1) =>
        _maxValue = maxValue;

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
    public ArrayElementData(int value)
        => Value = value;

    public int Value { get; }

    public static Comparison<ArrayElementData> Comparison { get; }
        = (a, b) => a.Value.CompareTo(b.Value);

    public override string ToString() =>
        Value.ToString();
}
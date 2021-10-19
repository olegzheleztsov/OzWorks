#region

using System;
using System.Numerics;
using System.Security.Cryptography;

#endregion

namespace Oz.Algorithms;

public class DefaultRandomSource : IRandomSource
{
#pragma warning disable CS0618
    private static readonly RNGCryptoServiceProvider Provider = new();
#pragma warning restore CS0618

    [ThreadStatic] private static Random _random;

    public float RandomFloat => (float)RandomDouble;

    public int RandomValue(int minValue, int maxValue)
    {
        var instance = GetRandomInstance();
        return instance.Next(minValue, maxValue);
    }

    public int[] RandomArray(int length, int min, int max)
    {
        var array = new int[length];
        for (var i = 0; i < array.Length; i++)
        {
            array[i] = RandomValue(min, max + 1);
        }

        return array;
    }

    public double RandomDouble => GetRandomInstance().NextDouble();

    private static Random GetRandomInstance()
    {
        var instance = _random;
        if (instance == null)
        {
            var buffer = new byte[4];
            Provider.GetBytes(buffer);
            _random = instance = new Random(BitConverter.ToInt32(buffer, 0));
        }

        return _random!;
    }

    public BigInteger RandomBigInteger(BigInteger minValue, BigInteger maxValue)
    {
        if (minValue > maxValue)
        {
            (minValue, maxValue) = (maxValue, minValue);
        }

        var offset = -minValue;
        maxValue += offset;
        var value = RandomInRangeFromZeroToPositive(maxValue) - offset;
        return value;
    }

    private BigInteger RandomInRangeFromZeroToPositive(BigInteger max)
    {
        BigInteger value;
        var bytes = max.ToByteArray();
        byte zeroBitsMask = 0b00000000;
        var mostSignificantByte = bytes[^1];
        for (var i = 7; i >= 0; i--)
        {
            if ((mostSignificantByte & (0b1 << i)) != 0)
            {
                var zeroBits = 7 - i;
                zeroBitsMask = (byte)(0b11111111 >> zeroBits);
                break;
            }
        }

        do
        {
            GetRandomInstance().NextBytes(bytes);
            bytes[^1] &= zeroBitsMask;
            value = new BigInteger(bytes);
        } while (value > max);

        return value;
    }
}
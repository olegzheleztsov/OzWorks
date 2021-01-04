using System;
using System.Security.Cryptography;
using Oz.Algorithms.Matrices;

namespace Oz.Algorithms
{
    public class DefaultRandomSource : IRandomSource
    {
        private static readonly RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();

        [ThreadStatic] private static Random _random;

        private static Random GetRandomInstance()
        {
            var instance = _random;
            if (instance == null)
            {
                var buffer = new byte[4];
                _provider.GetBytes(buffer);
                _random = instance = new Random(BitConverter.ToInt32(buffer, 0));
            }

            return _random;
        }

        public int RandomValue(int minValue, int maxValue)
        {
            var instance = GetRandomInstance();
            return instance.Next(minValue, maxValue);
        }

        public double RandomDouble => GetRandomInstance().NextDouble();

        public float RandomFloat => (float) RandomDouble;
    }
}
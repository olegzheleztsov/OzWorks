namespace Oz.Algorithms
{
    public interface IRandomSource
    {
        /// <summary>
        ///     Returns random value in range [0, 1]
        /// </summary>
        double RandomDouble { get; }

        /// <summary>
        ///     Returns random value in range [minValue, maxValue - 1]
        /// </summary>
        /// <param name="minValue">Min random value</param>
        /// <param name="maxValue">On 1 bigger than max random value</param>
        /// <returns>Random value in range [minValue, maxValue - 1]</returns>
        int RandomValue(int minValue, int maxValue);
    }
}
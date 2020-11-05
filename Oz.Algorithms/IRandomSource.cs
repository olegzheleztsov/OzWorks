namespace Oz.Algorithms
{
    public interface IRandomSource
    {
        int RandomValue(int minValue, int maxValue);
        double RandomDouble { get; }
    }
}
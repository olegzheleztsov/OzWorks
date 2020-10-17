namespace Oz.Algorithms.Search
{
    public class IntLinearSearcher : LinearSearcher<int>
    {
        public int? FindIndex(int[] array, int searchIndex)
        {
            return base.FindIndex(array, searchIndex, element => element);
        }
    }
}
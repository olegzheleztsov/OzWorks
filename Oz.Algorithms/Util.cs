namespace Oz.Algorithms
{
    public class Util
    {
        public static void Exchange<T>(ref T first, ref T second)
        {
            var temp = first;
            first = second;
            second = temp;
        }
    }
}
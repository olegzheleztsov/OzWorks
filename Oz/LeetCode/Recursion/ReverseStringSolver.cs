#region

using System;

#endregion

namespace Oz.LeetCode.Recursion
{
    public class ReverseStringSolver
    {
        private static void ReverseString(char[] s)
        {
            if (s == null || s.Length <= 1)
            {
                return;
            }

            if (s.Length == 2)
            {
                var temp = s[0];
                s[0] = s[1];
                s[1] = temp;
                return;
            }

            var forwardIndex = 0;
            var backwardIndex = s.Length - 1;
            ReverseStringRecursion(s, forwardIndex, backwardIndex);
        }

        private static void ReverseStringRecursion(char[] s, int forwardIndex, int backwardIndex)
        {
            if (forwardIndex >= backwardIndex)
            {
                return;
            }

            ReverseStringRecursion(s, forwardIndex + 1, backwardIndex - 1);
            var temp = s[forwardIndex];
            s[forwardIndex] = s[backwardIndex];
            s[backwardIndex] = temp;
        }

        public static void Test1()
        {
            var str = new[] {'h', 'e', 'l', 'l', 'o'};
            ReverseString(str);
            Console.WriteLine(string.Join(' ', str));
        }
    }
}
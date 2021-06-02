using System;

namespace Oz.LeetCode.Recursion
{
    public class ReverseStringSolver
    {
        public void ReverseString(char[] s) {
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

            int forwardIndex = 0;
            int backwardIndex = s.Length - 1;
            ReverseStringRecursion(s, forwardIndex, backwardIndex);
        }

        private void ReverseStringRecursion(char[] s, int forwardIndex, int backwardIndex)
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
            var str = new char[] {'h', 'e', 'l', 'l', 'o'};
            var solver = new ReverseStringSolver();
            solver.ReverseString(str);
            Console.WriteLine(string.Join(' ', str));
        }
    }
}
using System.Collections.Generic;

namespace Oz.LeetCode.Recursion
{
    public class PascalTriangleSolver
    {
        public IList<int> GetRow(int rowIndex) {
            switch (rowIndex)
            {
                case 0:
                    return new List<int>() {1};
                case 1:
                    return new List<int>() {1, 1};
            }

            var previousNumbers = GetRow(rowIndex - 1);
            var currentNumbers = new List<int>(previousNumbers.Count + 1) {1};

            for (int i = 1; i < previousNumbers.Count; i++)
            {
                currentNumbers.Add(previousNumbers[i - 1] + previousNumbers[i]);
            }

            currentNumbers.Add(1);
            return currentNumbers;
        }
    }
}
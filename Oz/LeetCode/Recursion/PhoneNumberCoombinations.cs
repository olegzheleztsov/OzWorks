using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode.Recursion
{
    public class PhoneNumberCoombinations
    {
        private static Dictionary<string, List<string>> _mapping = new Dictionary<string, List<string>>()
        {
            ["2"] = new List<string>() {"a", "b", "c"},
            ["3"] = new List<string>() {"d", "e", "f"},
            ["4"] = new List<string>() {"g", "h", "i"},
            ["5"] = new List<string>() {"j", "k", "l"},
            ["6"] = new List<string>() {"m", "n", "o"},
            ["7"] = new List<string>() {"p", "q", "r", "s"},
            ["8"] = new List<string>() {"t", "u", "v"},
            ["9"] = new List<string>() {"w", "x", "y", "z"}
        };
        
        public IList<string> LetterCombinations(string digits) {
            if (string.IsNullOrEmpty(digits))
            {
                return new List<string>();
            }

            if (digits.Length == 1)
            {
                return new List<string>(_mapping[digits]);
            }

            var firstDigit = digits[0].ToString();
            var remainDigits = string.Join("", digits.Skip(1));
            var subResult = LetterCombinations(remainDigits);

            var firstDigitResult = _mapping[firstDigit];
            List<string> finalResult = new List<string>();
            foreach (var fs in firstDigitResult)
            {
                foreach (var rs in subResult)
                {
                    finalResult.Add(fs + rs);
                }
            }

            return finalResult;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Oz.LeetCode.Recursion;

public class PhoneNumberCombinations
{
    private static readonly Dictionary<string, List<string>> Mapping = new()
    {
        ["2"] = new() {"a", "b", "c"},
        ["3"] = new() {"d", "e", "f"},
        ["4"] = new() {"g", "h", "i"},
        ["5"] = new() {"j", "k", "l"},
        ["6"] = new() {"m", "n", "o"},
        ["7"] = new() {"p", "q", "r", "s"},
        ["8"] = new() {"t", "u", "v"},
        ["9"] = new() {"w", "x", "y", "z"}
    };

    public IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits))
        {
            return new List<string>();
        }

        if (digits.Length == 1)
        {
            return new List<string>(Mapping[digits]);
        }

        var firstDigit = digits[0].ToString();
        var remainDigits = string.Join("", digits.Skip(1));
        var subResult = LetterCombinations(remainDigits);

        var firstDigitResult = Mapping[firstDigit];
        var finalResult = new List<string>();
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
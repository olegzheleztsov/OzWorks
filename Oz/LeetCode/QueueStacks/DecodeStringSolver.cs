using System.Collections.Generic;

namespace Oz.LeetCode.QueueStacks;

public class DecodeStringSolver
{
    public string DecodeString(string s)
    {
        var numTimes = new Stack<int>();
        var prefixes = new Stack<string>();

        var accum = string.Empty;
        var tokenType = TokenType.Num;
        var result = string.Empty;
        var counter = 0;

        foreach (var ch in s)
        {
            if (char.IsNumber(ch))
            {
                if (tokenType == TokenType.Str)
                {
                    if (accum.Length > 0)
                    {
                        prefixes.Push(accum);
                    }

                    accum = ch.ToString();
                    tokenType = TokenType.Num;
                }
                else
                {
                    accum += ch;
                }
            }
            else
            {
                switch (ch)
                {
                    case '[':
                    {
                        counter++;
                        if (tokenType == TokenType.Num && accum.Length > 0)
                        {
                            numTimes.Push(int.Parse(accum));
                            accum = string.Empty;
                            tokenType = TokenType.Str;
                        }

                        break;
                    }
                    case ']':
                    {
                        counter--;
                        if (tokenType == TokenType.Str && accum.Length > 0)
                        {
                            prefixes.Push(accum);
                        }

                        accum = string.Empty;
                        if (counter == 0)
                        {
                            result += Accumulate(numTimes, prefixes);
                        }

                        break;
                    }
                    default:
                        if (tokenType == TokenType.Str)
                        {
                            accum += ch.ToString();
                        }
                        else
                        {
                            tokenType = TokenType.Str;
                            accum = ch.ToString();
                        }

                        break;
                }
            }
        }

        result += accum;
        return result;
    }

    private string Accumulate(Stack<int> numTimes, Stack<string> prefixes)
    {
        var cur = string.Empty;

        while (numTimes.Count > 0)
        {
            var times = numTimes.Pop();
            var prefix = prefixes.Pop();
            var tempTok = prefix + cur;
            var res = string.Empty;
            for (var i = 0; i < times; i++)
            {
                res += tempTok;
            }

            cur = res;
        }

        while (prefixes.Count > 0)
        {
            cur = prefixes.Pop() + cur;
        }

        return cur;
    }

    private enum TokenType
    {
        Str,
        Num
    }
}
#region

using Oz.Algorithms.DataStructures;

#endregion

namespace Oz.Misc
{
    public class ParenthesisChecker
    {
        private readonly string _parenthesisInput;

        public ParenthesisChecker(string parenthesisInput)
        {
            _parenthesisInput = parenthesisInput;
        }

        public int GetLengthOfTheLongestClosedSubstring()
        {
            var stack = new OzStack<(char, int)>(_parenthesisInput.Length);
            int longestLength = 0;
            for (var i = 0; i < _parenthesisInput.Length; i++)
            {
                var checkChar = _parenthesisInput[i];
                var isClosed = false;
                if (checkChar == ')')
                {
                    if (!stack.IsEmpty)
                    {
                        if (stack.Peek().Item1 == '(')
                        {
                            var poppedParenthesis = stack.Pop();
                            longestLength += 2;
                            isClosed = true;
                        }
                    }
                }
                if (!isClosed)
                {
                    stack.Push((checkChar, i));
                }
            }

            return longestLength;
        }

        public ParenthesisCheckResult Validate()
        {
            var stack = new OzStack<(char, int)>(_parenthesisInput.Length);
            for (var i = 0; i < _parenthesisInput.Length; i++)
            {
                var checkChar = _parenthesisInput[i];
                var isClosed = false;
                if (checkChar == ')')
                {
                    if (!stack.IsEmpty)
                    {
                        if (stack.Peek().Item1 == '(')
                        {
                            stack.Pop();
                            isClosed = true;
                        }
                    }
                }

                if (!isClosed)
                {
                    stack.Push((checkChar, i));
                }
            }

            if (stack.IsEmpty)
            {
                return new ParenthesisCheckResult(true, -1);
            }

            (char, int) val = default;
            while (!stack.IsEmpty)
            {
                val = stack.Pop();
            }

            return new ParenthesisCheckResult(false, val.Item2);
        }
    }

    public record ParenthesisCheckResult(bool Valid, int InvalidIndex);
}
using System;
using System.Collections.Generic;

namespace Oz.LeetCode;

public class PolishNotationSolver
{
    private readonly Dictionary<string, Func<int, int, int>> _opEvaluator = new()
    {
        ["+"] = (a, b) => a + b, ["-"] = (a, b) => a - b, ["*"] = (a, b) => a * b, ["/"] = (a, b) => a / b
    };

    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<string>();
        foreach (var token in tokens)
        {
            if (_opEvaluator.ContainsKey(token))
            {
                var secondOperand = stack.Pop();
                var firstOperand = stack.Pop();
                stack.Push(EvaluateOperation(firstOperand, secondOperand, token).ToString());
            }
            else
            {
                stack.Push(token);
            }
        }

        if (stack.Count == 0)
        {
            return 0;
        }

        return int.Parse(stack.Pop());
    }

    private int EvaluateOperation(string aStr, string bStr, string op)
        => _opEvaluator[op](int.Parse(aStr), int.Parse(bStr));
}
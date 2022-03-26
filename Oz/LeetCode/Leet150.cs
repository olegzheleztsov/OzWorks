// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet150
{
    public int EvalRPN(string[] tokens)
    {
        var processor = new Stack<string>();
        foreach (var token in tokens)
        {
            switch (token)
            {
                case "+":
                {
                    var first = processor.Pop();
                    var second = processor.Pop();
                    var result = int.Parse(first) + int.Parse(second);
                    processor.Push(result.ToString());
                }
                    break;
                case "-":
                {
                    var first = processor.Pop();
                    var second = processor.Pop();
                    var result = int.Parse(first) - int.Parse(second);
                    processor.Push(result.ToString());
                }
                    break;
                case "*":
                {
                    var first = processor.Pop();
                    var second = processor.Pop();
                    var result = int.Parse(first) * int.Parse(second);
                    processor.Push(result.ToString());
                }
                    break;
                case "/":
                {
                    var first = processor.Pop();
                    var second = processor.Pop();
                    var result = int.Parse(second) / int.Parse(first);
                    processor.Push(result.ToString());
                }
                    break;
                default:
                {
                    processor.Push(token);
                }
                    break;
            }
        }

        return int.Parse(processor.Pop());
    }
}
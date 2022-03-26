// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Text;

namespace Oz.LeetCode;

public class Leet1678
{
    public string Interpret(string command)
    {
        StringBuilder stringBuilder = new StringBuilder();

        int index = 0;
        while (index < command.Length)
        {
            switch (command[index])
            {
                case 'G':
                    stringBuilder.Append('G');
                    index++;
                    break;
                case '(':
                    if (command[index + 1] == ')')
                    {
                        stringBuilder.Append('o');
                        index += 2;
                    }
                    else
                    {
                        stringBuilder.Append("al");
                        index += 4;
                    }
                    break;
            }
        }

        return stringBuilder.ToString();
    }
}
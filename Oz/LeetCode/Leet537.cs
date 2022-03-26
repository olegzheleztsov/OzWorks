// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet537
{
    public string ComplexNumberMultiply(string num1, string num2)
    {
        var c1 = Complex.Parse(num1);
        var c2 = Complex.Parse(num2);
        return (c1 * c2).ToString();
    }
    
    public class Complex
    {
        public int Real { get; set; }
        public int Imaginary { get; set; }

        public static Complex operator *(Complex c1, Complex c2)
        {
            Complex result = new Complex();
            result.Real = c1.Real * c2.Real - c1.Imaginary * c2.Imaginary;
            result.Imaginary = c1.Real * c2.Imaginary + c2.Real * c1.Imaginary;
            return result;
        }

        public override string ToString()
        {
            return $"{Real}+{Imaginary}i";
        }

        public static Complex Parse(string s)
        {
            string[] tokens = s.Split(new char[] {'+'}, StringSplitOptions.RemoveEmptyEntries);

            Complex complex = new Complex();
            if (tokens.Length == 2)
            {
                complex.Real = int.Parse(tokens[0]);
                string imgS = tokens[1].Replace("i", string.Empty);
                complex.Imaginary = int.Parse(imgS);
            }
            else
            {
                string token = tokens[0];
                if (token.Contains("i"))
                {
                    complex.Imaginary = int.Parse(token.Replace("i", string.Empty));
                }
                else
                {
                    complex.Real = int.Parse(token);
                }
            }

            return complex;
        }
    }
}
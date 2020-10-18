// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz.Algorithms     File: IMaxSubArray.cs    Created at 2020/10/18/6:17 PM
// All rights reserved, for personal using only
// 

namespace Oz.Algorithms.Arrays
{
    public interface IMaxSubArray
    {
        (int leftIndex, int rightIndex, int sum) FindMaximumSubArray(int lowIndex, int highIndex);
        (int leftIndex, int rightIndex, int sum) Value { get; }
    }
}
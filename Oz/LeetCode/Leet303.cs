// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Oz.LeetCode;

public class Leet303
{
    public class NumArray
    {
        private readonly int[] _nums;

        private readonly int[] _partialSumArray;

        public NumArray(int[] nums)
        {
            _nums = nums;
            _partialSumArray = new int[nums.Length];
            _partialSumArray[0] = nums[0];
            for (var i = 1; i < _partialSumArray.Length; i++)
            {
                _partialSumArray[i] = _partialSumArray[i - 1] + nums[i];
            }
        }

        public int SumRange(int left, int right) =>
            _partialSumArray[right] - _partialSumArray[left] + _nums[left];
    }
}
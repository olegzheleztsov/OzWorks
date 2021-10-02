namespace Oz.LeetCode.Easy
{
    public class EasyTasks
    {
        /// <summary>
        ///     https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int MaxProfit(int[] prices)
        {
            var minPrice = int.MaxValue;
            var maxProfit = 0;
            foreach (var price in prices)
            {
                if (price < minPrice)
                {
                    minPrice = price;
                }
                else if (price - minPrice > maxProfit)
                {
                    maxProfit = price - minPrice;
                }
            }

            return maxProfit;
        }
        
        /// <summary>
        /// https://leetcode.com/problems/single-number/
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int SingleNumber(int[] nums)
        {
            var singleNumber = 0;
            foreach (var num in nums) {
                singleNumber ^= num;
            }
            return singleNumber;
        }
        

        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            var temp = new int[k];

            for (var i = nums.Length - k; i < nums.Length; i++)
            {
                temp[i - nums.Length + k] = nums[i];
            }

            var last = nums.Length - 1;
            var first = nums.Length - k - 1;

            for (var i = 0; i < nums.Length - k; i++)
            {
                nums[last] = nums[first];
                last--;
                first--;
            }

            for (var i = 0; i < k; i++)
            {
                nums[i] = temp[i];
            }
        }
    }
}
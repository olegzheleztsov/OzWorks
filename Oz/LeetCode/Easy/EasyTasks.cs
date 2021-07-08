namespace Oz.LeetCode.Easy
{
    public class EasyTasks
    {
        public int MaxProfit(int[] prices)
        {
            int buyPointer = 0;
            int sellPointer = 1;
            int diff = 0;

            while (buyPointer < prices.Length)
            {
                while (buyPointer < prices.Length - 1 && prices[buyPointer+1] < prices[buyPointer])
                {
                    buyPointer++;
                }

                sellPointer = buyPointer;
                while (sellPointer < prices.Length - 1 && prices[sellPointer] < prices[sellPointer + 1])
                {
                    sellPointer++;
                }

                if (buyPointer < sellPointer)
                {
                    diff += prices[sellPointer] - prices[buyPointer];
                }

                buyPointer = sellPointer + 1;
            } 

            return diff;
        }
        
        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            int[] temp = new int[k];

            for (int i = nums.Length - k; i < nums.Length; i++)
            {
                temp[i - nums.Length + k] = nums[i];
            }

            int last = nums.Length - 1;
            int first = nums.Length - k - 1;

            for (int i = 0; i < nums.Length - k; i++)
            {
                nums[last] = nums[first];
                last--;
                first--;
            }

            for (int i = 0; i < k; i++)
            {
                nums[i] = temp[i];
            }
        }
    }
}
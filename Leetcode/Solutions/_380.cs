// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace Leetcode.Solutions;

public class _380
{
    public class RandomizedSet
    {

        private List<int> nums = new List<int>();
        private Dictionary<int, int> _map = new Dictionary<int, int>();
        private Random _random = new Random();
        
        public RandomizedSet() {
        
        }
    
        public bool Insert(int val)
        {
            if (_map.ContainsKey(val))
            {
                return false;
            }
            nums.Add(val);
            _map.Add(val, nums.Count - 1);
            return true;
        }
    
        public bool Remove(int val) {
            if (!_map.ContainsKey(val))
            {
                return false;
            }

            int i = _map[val];
            nums[i] = nums[nums.Count - 1];
            _map[nums[i]] = i;
            
            nums.RemoveAt(nums.Count-1);
            _map.Remove(val);
            return true;
        }
    
        public int GetRandom()
        {
            return nums[_random.Next(0, nums.Count)];
        }
    }
}
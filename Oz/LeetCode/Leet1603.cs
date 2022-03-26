// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System.Collections.Generic;

namespace Oz.LeetCode;

public class Leet1603
{
    public class ParkingSystem
    {
        private Dictionary<int, int> _slots = new Dictionary<int, int>();

        public ParkingSystem(int big, int medium, int small)
        {
            _slots[1] = big;
            _slots[2] = medium;
            _slots[3] = small;
        }
    
        public bool AddCar(int carType) {
            if (_slots[carType] > 0)
            {
                _slots[carType]--;
                return true;
            }

            return false;
        }
    }
}
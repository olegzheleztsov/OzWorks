// Create By: Oleg Gelezcov                        (olegg )
// Project: Oz.Algorithms     File: IKey.cs    Created at 2021/01/14/4:45 PM
// All rights reserved, for personal using only
// 

namespace Oz.Algorithms.DataStructures
{
    public interface IKey
    {
        int Key { get; }
        void SetKey(int key);
    }
}
using Newtonsoft.Json;
using Oz.Algorithms;
using Oz.Algorithms.Sort;
using System;

namespace Oz;

public class CountingSorterCase
{
    public void Run()
    {
        var sorter = new CountingSorter<Data>();
        var array = GenerateData();
        sorter.Sort(array, data => data.Key, Comparisions.StandardComparision);
        Console.WriteLine(JsonConvert.SerializeObject(array));
    }

    private Data[] GenerateData()
    {
        Data[] array =
        {
            new() {Key = 2, Value = 'a'}, new() {Key = 5, Value = 'b'}, new() {Key = 3, Value = 'c'},
            new() {Key = 0, Value = 'd'}, new() {Key = 2, Value = 'e'}, new() {Key = 3, Value = 'f'},
            new() {Key = 0, Value = 'g'}, new() {Key = 3, Value = 'h'}
        };
        return array;
    }

    private class Data
    {
        public int Key { get; set; }
        public char Value { get; set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this);
    }
}
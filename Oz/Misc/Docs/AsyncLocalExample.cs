// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Oz.Misc.Docs;

public class AsyncLocalExample
{
    private static readonly AsyncLocal<string> _asyncLocalString = new();
    private static readonly ThreadLocal<string> _threadLocalString = new();

    private static async Task AsyncMethodA()
    {
        _asyncLocalString.Value = "Value 1";
        _threadLocalString.Value = "Value 1";


        var t1 = AsyncMethodB("Value 1");

        _asyncLocalString.Value = "Value 2";
        _threadLocalString.Value = "Value 2";
        var t2 = AsyncMethodB("Value 2");

        await t1;
        await t2;
    }

    private static async Task AsyncMethodB(string expectedValue)
    {
        WriteLine($"Entering {nameof(AsyncMethodB)}");
        WriteLine(
            $"  Expected '{expectedValue}', AsyncLocal value is '{_asyncLocalString.Value}', ThreadLocal string is {_threadLocalString.Value}");
        await Task.Delay(100).ConfigureAwait(false);
        WriteLine($"Exiting {nameof(AsyncMethodB)}");
        WriteLine(
            $"  Expected '{expectedValue}', got '{_asyncLocalString.Value}', ThreadLocal value is '{_threadLocalString.Value}'");
    }

    public static async Task FMain() =>
        await AsyncMethodA().ConfigureAwait(false);
}
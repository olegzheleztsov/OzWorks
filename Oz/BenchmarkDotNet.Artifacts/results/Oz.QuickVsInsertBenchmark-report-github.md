``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
Intel Core i3-7100U CPU 2.40GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT DEBUG
  Job-UHXFZR : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                               Method |         Mean |        Error |       StdDev |       Median |
|------------------------------------- |-------------:|-------------:|-------------:|-------------:|
|    RandomizedQuickSortWithSameValues | 869,325.0 μs | 14,361.91 μs | 25,897.49 μs | 859,019.6 μs |
|              QuickSortWithSameValues | 827,428.1 μs | 15,346.15 μs | 12,814.73 μs | 823,463.4 μs |
|             InsertSortWithSameValues |     212.8 μs |     18.13 μs |     52.61 μs |     185.1 μs |
|         QuickSortWithDifferentValues |   3,162.0 μs |    177.39 μs |    508.96 μs |   2,979.6 μs |
|        InsertSortWithDifferentValues | 260,850.6 μs |  4,221.61 μs |  3,948.90 μs | 260,051.1 μs |
|              InsertForDescendingData | 537,945.5 μs |  3,289.52 μs |  2,916.07 μs | 538,799.1 μs |
|           QuicksortForDescendingData | 649,143.3 μs | 15,647.51 μs | 44,389.39 μs | 630,069.1 μs |
| RandomizedQuicksortForDescendingData |   2,999.8 μs |    170.96 μs |    504.08 μs |   2,792.6 μs |

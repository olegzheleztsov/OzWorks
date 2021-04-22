``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i3-7100U CPU 2.40GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.104
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT DEBUG
  DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT


```
|               Method |     Mean |     Error |    StdDev |
|--------------------- |---------:|----------:|----------:|
| MoveToFrontBenchmark | 4.761 ms | 0.0309 ms | 0.0241 ms |
|        SwapBenchmark | 3.200 ms | 0.0292 ms | 0.0259 ms |
|       CountBenchmark | 2.662 ms | 0.0312 ms | 0.0292 ms |

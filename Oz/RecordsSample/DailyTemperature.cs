using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oz.RecordsSample;

public record DailyTemperature(double HighTemp, double LowTemp)
{
    public double Mean => (HighTemp + LowTemp) / 2.0;
}

public abstract record DegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append($"{nameof(BaseTemperature)} = {BaseTemperature}");
        return true;
    }
}

public record HeatingDegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemperature, TempRecords)
{
    public double DegreeDays
        => TempRecords.Where(s => s.Mean < BaseTemperature)
            .Sum(s => BaseTemperature - s.Mean);

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append(nameof(HeatingDegreeDays));
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(" ");
        }

        stringBuilder.Append("}");
        return stringBuilder.ToString();
    }
}

public sealed record CoolingDegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemperature, TempRecords)
{
    public double DegreeDays =>
        TempRecords.Where(s => s.Mean > BaseTemperature)
            .Sum(s => s.Mean - BaseTemperature);
}

public static class Sample
{
    private static readonly DailyTemperature[] data =
    {
        new(57, 30), new(60, 35), new(63, 33), new(68, 29), new(72, 47), new(75, 55), new(77, 55), new(72, 58),
        new(70, 47), new(77, 59), new(85, 65), new(87, 65), new(85, 72), new(83, 68), new(77, 65), new(72, 58),
        new(77, 55), new(76, 53), new(80, 60), new(85, 66)
    };

    public static void Run()
    {
        foreach (var item in data)
        {
            Console.WriteLine(item);
        }

        var heatingDegreeDays = new HeatingDegreeDays(65, data);
        Console.WriteLine(heatingDegreeDays);

        var coolingDegreeDays = new CoolingDegreeDays(65, data);
        Console.WriteLine(coolingDegreeDays);

        var growingDegreeDays = coolingDegreeDays with {BaseTemperature = 41};
        Console.WriteLine(growingDegreeDays);

        List<CoolingDegreeDays> movingAccumulation = new();
        var rangeSize = data.Length > 5 ? 5 : data.Length;
        for (var start = 0; start < data.Length - rangeSize; start++)
        {
            var fiveDayTotal = growingDegreeDays with {TempRecords = data[start..(start + rangeSize)]};
            movingAccumulation.Add(fiveDayTotal);
        }

        Console.WriteLine();
        Console.WriteLine("Total degree days in the last five days");
        foreach (var item in movingAccumulation)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("-----------");
        var growingDegreeDaysCopy = growingDegreeDays with { };
        Console.WriteLine(growingDegreeDaysCopy);
    }
}
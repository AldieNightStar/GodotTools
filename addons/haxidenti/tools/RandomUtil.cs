using System;
using System.Collections.Generic;

namespace HaxiDenti.Tools;

public class RandomUtil
{
    private static Random _random = new Random();

    private static void Reset(int seed)
    {
        _random = new Random(seed);
    }

    private static void Reset()
    {
        _random = new Random();
    }

    private static int NextInt() => _random.Next();
    private static int NextInt(int min, int max) => _random.Next(min, max);
    private static double NextDouble() => _random.NextDouble();
    public static bool Chance(double chance) => NextDouble() <= chance;

    public static T Choose<T>(IList<T> list)
    {
        if (list.Count == 0) return default;
        var id = NextInt(0, list.Count);
        return list[id];
    }

    public static T Choose<T>(T[] arr)
    {
        if (arr.Length == 0) return default;
        var id = NextInt(0, arr.Length);
        return arr[id];
    }

    public static T ChooseByChance<T>(params (T, double)[] chances)
    {
        if (chances.Length < 1) return default;
        if (chances.Length == 1) return chances[0].Item1;

        var first = chances[0];

        var mostItem = first.Item1;
        var mostChance = first.Item2;

        foreach (var (item, chance) in chances)
        {
            if (Chance(chance)) return item;
            if (chance >= mostChance)
            {
                mostItem = item;
                mostChance = chance;
            }
        }

        return mostItem;
    }
}
using System;
using System.Collections.Generic;

namespace HaxiDenti.Tools;

public class Chans
{
    private static Dictionary<Type, IChan> _dict = new();

    public static Chan<T> Of<T>() {
        var type = typeof(T);
        if (!_dict.ContainsKey(type)) _dict[type] = new Chan<T>();
        return (Chan<T>) _dict[type];
    }

    public static void ClearOf<T>()
    {
        var type = typeof(T);
        _dict.Remove(type);
    }

    public static void ClearAll()
    {
        _dict.Clear();
    }
}
using System;
using System.Collections.Generic;

namespace HaxiDenti.Tools;

public class ObjectPool<T> where T : class
{
    private readonly Stack<T> _free = new();
    private readonly HashSet<T> _used = new(ReferenceEqualityComparer.Instance);
    private readonly Func<T> _createFunc;

    public ObjectPool(Func<T> creator) => _createFunc = creator;

    public T Poll()
    {
        T value = _free.Count == 0 ? _createFunc() : _free.Pop();
        _used.Add(value);
        return value;
    }

    public bool Free(T value)
    {
        if (value == null || !_used.Remove(value))
            return false; // From different pool, because _used didn't have that value

        _free.Push(value);
        return true;
    }

    public void Clear()
    {
        _used.Clear();
        _free.Clear();
    }
}
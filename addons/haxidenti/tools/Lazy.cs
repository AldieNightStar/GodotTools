using System;

namespace HaxiDenti.Tools;

public struct Lazy<T>
{
    private T _value;
    private bool _initialized;
    private Func<T> _func;

    public Lazy(Func<T> func)
    {
        _func = func;
    }

    public T Value
    {
        get
        {
            if (!_initialized)
            {
                _value = _func();
                _initialized = true;
            }
            return _value;
        }
    }
}
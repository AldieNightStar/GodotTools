using System;
using System.Collections.Generic;

namespace HaxiDenti.Tools;

public class Chan<T> : IDisposable
{
    private HashSet<Action<T>> _actions = new();

    public void Clear()
    {
        _actions.Clear();
    }
    
    public void Send(T dat)
    {
        foreach (var act in _actions)
        {
            act(dat);
        }
    }

    public void Listen(Action<T> act)
    {
        _actions.Add(act);
    }

    public IDisposable ListenDispose(Action<T> act)
    {
        Listen(act);
        return new Subscription(this, act);
    }

    public void Unlisten(Action<T> act)
    {
        _actions.Remove(act);
    }

    public void Dispose()
    {
        Clear();
    }

    private class Subscription(Chan<T> Ch, Action<T> Act) : IDisposable
    {
        public void Dispose()
        {
            if (Ch == null || Act == null) return;
            Ch.Unlisten(Act);
            Ch = null;
            Act = null;
        }
    }
}
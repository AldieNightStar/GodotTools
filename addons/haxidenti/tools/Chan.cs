using System;
using System.Collections.Generic;

namespace HaxiDenti.Tools;

public interface IChan;

public class Chan<T> : IDisposable, IChan
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

    public IDisposable Listen(Action<T> act)
    {
        _actions.Add(act);
        return new Subscription(_actions, act);
    }

    public void Dispose()
    {
        Clear();
    }

    private struct Subscription(HashSet<Action<T>> Actions, Action<T> Act) : IDisposable
    {
        public void Dispose()
        {
            if (Actions == null || Act == null) return;
            Actions.Remove(Act);
            Actions = null;
            Act = null;
        }
    }
}
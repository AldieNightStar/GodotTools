using System;
using Godot;

namespace HaxiDenti.Tools;

public class AnimationUtil
{
    public static Tween Shake(Node2D node, double duration=0.3, Vector2 amp=default)
    {
        if (amp == default) amp = new Vector2(50, 0);

        var xduration = duration / 5;
        var oldPos = node.Position;
        var newPos1 = oldPos -= amp;
        var newPos2 = oldPos += amp;
        var t = node.CreateTween();
        t.TweenProperty(node, "position", newPos1, xduration);
        t.TweenProperty(node, "position", newPos2, xduration);
        t.TweenProperty(node, "position", newPos1, xduration);
        t.TweenProperty(node, "position", newPos2, xduration);
        t.TweenProperty(node, "position", oldPos, xduration);

        return t;
    }

    public static Tween Float(Node2D node, double interval=1.0, Vector2 amp=default)
    {
        if (amp == default) amp = new Vector2(0, 25);

        var oldPos = node.Position;
        var newPos = node.Position + amp;

        var t = node.CreateTween();
        t.SetTrans(Tween.TransitionType.Quad);

        t.TweenProperty(node, "position", newPos, interval);
        t.TweenProperty(node, "position", oldPos, interval);
        t.SetLoops();

        return t;
    }

    public static Tween PushInOut(Node2D node, double duration=0.15, float size=0.8f)
    {
        var oldScale = node.Scale;
        var newScale = node.Scale * size;
        var xduration = duration/2;
        
        var t = node.CreateTween();
        t.TweenProperty(node, "scale", newScale, xduration);
        t.TweenProperty(node, "scale", oldScale, xduration);

        return t;
    }

    public static Tween FadeOut(Node2D node, double duration=0.3)
    {
        var newColor = node.Modulate;
        newColor.A = 0f;

        var t = node.CreateTween();
        t.TweenProperty(node, "modulate", newColor, duration);
        return t;
    }

    public static Tween FadeIn(Node2D node, double duration=0.3)
    {
        var newColor = node.Modulate;
        newColor.A = 1f;

        var t = node.CreateTween();
        t.TweenProperty(node, "modulate", newColor, duration);
        return t;
    }

    public static void FadeOutIn(Node2D node, double duration=0.3, Action<Node2D> act=default)
    {
        var xduration = duration/2;
        FadeOut(node, xduration).Finished += () =>
        {
            if (act != default) act(node);
            FadeIn(node, xduration);
        };
    }

    public static Tween TypingText(Label label, double duration=0.5)
    {
        label.VisibleRatio = 0;
        var t = label.CreateTween();
        t.TweenProperty(label, "visible_ratio", 1, duration);

        return t;
    }

    public static Tween TypingText(RichTextLabel label, double duration=0.5)
    {
        label.VisibleRatio = 0;
        var t = label.CreateTween();
        t.TweenProperty(label, "visible_ratio", 1, duration);

        return t;
    }

    public static Timer WhileTimer(Node node, double interval, Func<bool> whileFunc, Action act, Action onDone=null)
    {
        var timer = new Timer();
        timer.Timeout += () =>
        {
            if (whileFunc())
            {
                act();
            }
            else
            {
                timer.QueueFree();
                if (onDone != null) onDone();
            }
        };

        node.AddChild(timer);
        timer.WaitTime = interval;
        timer.OneShot = false;
        timer.Start();

        return timer;
    }

    public static Tween Move(Node2D node, double duration, Vector2 targetPos, Tween.TransitionType ttype=Tween.TransitionType.Quad)
    {
        var t = node.CreateTween();
        t.SetTrans(ttype);
        t.TweenProperty(node, "global_position", targetPos, duration);

        return t;
    }

    public static Tween Move(Node2D node, double duration, Vector2[] targetPoses, Tween.TransitionType ttype=Tween.TransitionType.Quad)
    {
        var t = node.CreateTween();
        t.SetTrans(ttype);

        double xduration = duration / targetPoses.Length;
        foreach (var pos in targetPoses)
        {
            t.TweenProperty(node, "global_position", pos, xduration);
        }

        return t;
    }
}
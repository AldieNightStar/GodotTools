using System;
using System.Linq;
using Godot;

namespace HaxiDenti.Tools;

public class NodeUtil
{
    public static T Instance<T>(string sceneFilePath) where T : Node
    {
        var scene = GD.Load<PackedScene>(sceneFilePath);
        if (scene == null) return null;

        var node = scene.Instantiate<T>();
        return node;
    }

    public static T Spawn<T>(Node into, string sceneFilePath) where T : Node
    {
        var node = Instance<T>(sceneFilePath);
        into.AddChild(node);
        return node;
    }

    public static T SpawnAt<T>(Node into, string sceneFilePath, Vector2 gPos) where T : Node2D
    {
        var node = Spawn<T>(into, sceneFilePath);
        if (node != null)
        {
            node.GlobalPosition = gPos;
        }
        return node;
    }

    public static T CreateOrUpdate<T>(Node parent, string nodeName, string sceneFilePath, Action<T> update) where T : Node
    {
        var node = parent.GetNodeOrNull<T>(nodeName);
        if (node == null)
        {
            node = Spawn<T>(parent, sceneFilePath);
            node.Name = nodeName;
        }

        Callable.From(() => update(node)).CallDeferred();

        return node;
    }

    public static bool IsChild(Node owner, Node child)
    {
        if (owner == null || child == null) return false;
        if (!GodotObject.IsInstanceValid(owner)) return false;
        if (!GodotObject.IsInstanceValid(child)) return false;
        return child.GetParent() == owner;
    }

    public static void ClearNodes(Node node, string[] ignores = null)
    {
        foreach (var child in node.GetChildren())
        {
            var name = child.Name.ToString();
            if (ignores != null && ignores.Contains(name)) continue;
            child.QueueFree();
        }
    }

    private static SceneTree _SCENE_TREE;

    private static SceneTree _getSceneTree()
    {
        if (_SCENE_TREE == null) _SCENE_TREE = Engine.GetMainLoop() as SceneTree;
        return _SCENE_TREE;
    }

    public static T GetRootNode<T>(string name)
    {
        var node = _getSceneTree().Root.GetNode("/root/" + name);
        if (node is T typedNode)
        {
            return typedNode;
        }
        else
        {
            return default;
        }
    }

    public static void VisibleOnly(Node node, string[] names)
    {
        var items = node.GetChildren().OfType<CanvasItem>();
        foreach (var item in items)
        {
            var flag = names.Contains<string>(item.Name);
            item.Visible = flag;
        }
    }

    public static void SetProcessing(CanvasItem item, bool flag)
    {
        var PROC = Node.ProcessModeEnum.Inherit;
        var IGNORE = Node.ProcessModeEnum.Disabled;

        item.SetProcess(flag);
        item.SetProcessInput(flag);
        item.SetProcessInternal(flag);
        item.SetProcessUnhandledInput(flag);
        item.SetProcessUnhandledKeyInput(flag);
        item.SetPhysicsProcess(flag);
        item.SetPhysicsProcessInternal(flag);
        item.ProcessMode = flag ? PROC : IGNORE;
    }

    public static void ProcessOnly(Node node, string[] names)
    {
        var items = node.GetChildren().OfType<CanvasItem>();
        foreach (var item in items)
        {
            var flag = names.Contains<string>(item.Name);
            item.Visible = flag;
            SetProcessing(item, flag);
        }
    }

    public static T ChangeScene<T>(Node prev, string sceneFilePath, Action<T> act = null) where T : Node
    {
        var scene = Instance<T>(sceneFilePath);
        if (act != null) act(scene);
        var root = prev.GetTree().Root;
        root.AddChild(scene);
        prev.QueueFree();
        return scene;
    }

    public static Timer RepeatTimer(Node node, double interval, Action action)
    {
        var timer = new Timer() { OneShot = false, };
        timer.Timeout += action;
        timer.WaitTime = interval;
        node.AddChild(timer);
        timer.Start();
        return timer;
    }

    public static void DeferCall(Action act)
    {
        Callable.From(act).CallDeferred();
    }
}
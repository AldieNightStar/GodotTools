# HaxiDenti Tools

# Animation
```cs
// Shake Node
var tween = AnimationUtil.Shake(node);
var tween = AnimationUtil.Shake(node, durationSec, vector2);

// Make node floating forever.
// To stop: tween.Kill()
var tween = AnimationUtil.Float(node);
var tween = AnimationUtil.Float(node, intervalSec, vector2);

// Make node animate pushing in, then out
var tween = AnimationUtil.PushInOut(node);
var tween = AnimationUtil.PushInOut(node, duration, 0.75f);

// Fade In/Out
var tween = AnimationUtil.FadeOut(node, durationSec);
var tween = AnimationUtil.FadeIn(node, durationSec);

// Fade Out, do something, then Fade In
// Calls fadeout animation, then calls action, then fades in
AnimationUtil.FadeOutIn(node, durationSec, node => { /* ... */ });

// Typing text animation for labels
var tween = AnimationUtil.TypingText(label, seconds);

// Creates temporary timer that asks whileFunc and calls action
//     interval    - Interval between calls in seconds
//     whileFunc   - Func that returns true/false
//     action      - Action that is called while true
//     onDone      - (Optional) Called when ended
var timer = AnimationUtil.WhileTimer(node, interval, whileFunc, action, onDone);

// Move node with tween interpolation
//     duration       - Duration in seconds
//     globalPosition - Global Vector2 Position. You can also provide an array
//     transType      - (Optional) Tween transition type. Ex: Tween.TransitionType.Quad
var tween = AnimationUtil.Move(node, durationSec, globalPosition, transType);
var tween = AnimationUtil.Move(node, durationSec, [pos1, pos2], transType);
```

# Atlas
```cs
// Creates tileset from texture.
// Just need to put how many tiles is in tileset, and what their size
AtlasUtil.Slice(texture, countX, countY, tileW, tileH);
```

# Channels
```cs
// Create
var chan = new Chan<int>();

// Or Get from Global by defining custom type
var chan = Chans.Of<CustomEventType>();

// Clear all the listeners
chan.Clear();

// Send data
chan.Send(data);

// Listen to events
// [!] Need to .Dispose() when done
var connection = chan.Listen((data) => {});

// Close connection
disposable.Dispose();
```

# NodeUtil
```cs
// Create new Node without spawning
var node = Instance<T>("Ball");

// Spawn entity into a node with no position set
var node = NodeUtil.Spawn<T>(this, "Ball");

// Spawn "Ball" on the screen at (15, 25)
var node = NodeUtil.SpawnAt<T>(this, "Ball", new Vector2(15, 25));

// Gets node with name "Ball34", and if not exist, then spawn new one
// Then after create/update it will call updateFunc for this node at the next frame
var node = NodeUtil.CreateOrUpdate(this, "Ball34", "Ball", updateFunc);

// Change scene
// Sets new scene instead of previous `prevScene`
//
// prevScene    - Previous scene to replace.
// scene name   - Name of the scene to load. As well as in Spawn(...) function
// (Opt) action - Some Manipulation with scene before the tree
//
// Returns newly created scene
NodeUtil.ChangeScene<T>(prevScene, "NewScene");
NodeUtil.ChangeScene<T>(prevScene, "NewScene", scene => {});

// Check that node is child of the owner (Not deep)
NodeUtil.IsChild(this, node);

// Clear all the nodes
//   node   - Node to clear
//   ignore - (Optional) Array of names to ignore
NodeUtil.ClearNode(node);
NodeUtil.ClearNode(node, ["IgnoreMe"]);

// Get root node by name (Autoload or Singleton)
//   name - Name of the node
NodeUtil.GetRootNode<GlobalData>("G");

// Show only specific children inside node
// The rest will set Visibe=false
NodeUtil.VisibleOnly(node, ["Scene1"]);

// Set processing to ON or OFF
// Will not change visibility
NodeUtil.SetProcessing(item, true);

// Process/Show only specific children inside node
// The rest will not be processed or visible
NodeUtil.ProcessOnly(node, ["Scene1"]);

// Create repeatable timer each seconds interval
var timer = NodeUtil.Repeat(node, seconds, action);
```

# Random
```cs
// Generate random number
RandomUtil.NextInt();
RandomUtil.NextInt(0, 10);
RandomUtil.NextDouble(0, 10);

// Generate true/false by chance
RandomUtil.Chance(0.3);

// Chose random element
RandomUtil.Choose(list);
RandomUtil.Choose(array);

// Choose elements by chance table
RandomUtil.ChooseByChance(
    (item1, 0.5),
    (item2, 0.3),
    (item3, 0.1),
    (item4, 0.01)
);

// Resets the generator
RandomUtil.Reset();
RandomUtil.Reset(seed);
```


# Lazy
```cs
// Create Lazy Object
var lazy = new Lazy(() => 32);

// Get value
// Will compute first time, then reuse that value
lazy.Value;
```

# RenderCanvas
```cs
// Create new Render Canvas Node
// By using Into(node), you can add it as child to some node
var can = RenderCanvas.Create().Into(node)

// Node2D element with Canvas RID
can.Canvas

// Clears drawings
can.Clear();

// Gets local mouse pointer
can.GetMousePointer();

// Draws Texture2D/AtlasTexture at some coordinates
can.DrawTexture(t, rect);
can.DrawTexture(t, x, y, w, h);

// Draws geometric figures
can.DrawRect(color, rect);
can.DrawRect(color, x, y, w, h);
can.DrawLine(color, start, end, width=1);
can.DrawCircle(color, pos, radius);

// Draws text
// font can be null - then will use default
can.DrawText(font, text, pos, fontSize, alignment=HorizontalAlignment.Left, color=Colors.White);
```

# Object Pools
* Allows to use less `GC` and reallocate
* Used only for `class` objects, not for `struct`
```cs
// Create new one
var pool = new ObjectPool<MyType>();

// Poll already allocated value from pool
var value = pool.Poll();

// Free the value back to pool
pool.Free(value);
```
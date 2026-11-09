using Godot;

namespace HaxiDenti.Tools;

public partial class RenderCanvas : Node2D
{
    public Rid CanvasRid;

    public static RenderCanvasControl Create()
    {
        var rc = new RenderCanvas();
        return new RenderCanvasControl(rc);
    }

    public override void _EnterTree()
    {
        CanvasRid = RenderingServer.CanvasItemCreate();
        RenderingServer.CanvasItemSetParent(CanvasRid, GetCanvasItem());
    }

    public override void _ExitTree()
    {
        RenderingServer.FreeRid(CanvasRid);
    }

    public class RenderCanvasControl
    {
        public RenderCanvas Canvas { get; private set; }

        public RenderCanvasControl(RenderCanvas canvas)
        {
            Canvas = canvas;
        }

        public RenderCanvasControl Into(Node node)
        {
            node.AddChild(Canvas);
            return this;
        }

        public void DrawTexture(AtlasTexture t, Rect2 rect)
        {
            if (t == default) return;
            RenderingServer.CanvasItemAddTextureRectRegion(Canvas.CanvasRid,
                rect,
                t.Atlas.GetRid(),
                t.Region
            );
        }

        public void DrawTexture(AtlasTexture t, int x, int y, int w, int h)
        {
            DrawTexture(t, new Rect2(x, y, w, h));
        }

        public void DrawTexture(Texture t, Rect2 rect)
        {
            if (t == default) return;
            RenderingServer.CanvasItemAddTextureRect(Canvas.CanvasRid, rect, t.GetRid());
        }

        public void DrawTexture(Texture2D t, int x, int y, int w, int h)
        {
            DrawTexture(t, new Rect2(x, y, w, h));
        }

        public void DrawRect(Color color, Rect2 rect)
        {
            RenderingServer.CanvasItemAddRect(Canvas.CanvasRid, rect, color);
        }

        public void DrawRect(Color color, int x, int y, int w, int h)
        {
            DrawRect(color, new Rect2(x, y, w, h));
        }

        public void DrawLine(Color color, Vector2 start, Vector2 end, float width = 1)
        {
            RenderingServer.CanvasItemAddLine(Canvas.CanvasRid, start, end, color, width);
        }

        public void DrawCircle(Color color, Vector2 pos, float radius)
        {
            RenderingServer.CanvasItemAddCircle(Canvas.CanvasRid, pos, radius, color);
        }

        public void DrawText(Font font, string text, Vector2 pos, int fontSize,
            HorizontalAlignment alignment = HorizontalAlignment.Left,
            Color color=default)
        {
            if (font == null) font = ThemeDB.FallbackFont;
            if (color == default) color = Colors.White;
            font.DrawString(Canvas.CanvasRid, pos, text, alignment, fontSize: fontSize, modulate: color);
        }

        public void Clear()
        {
            RenderingServer.CanvasItemClear(Canvas.CanvasRid);
        }

        public Vector2 GetMousePointer()
        {
            return Canvas.GetLocalMousePosition();
        }
    }
}
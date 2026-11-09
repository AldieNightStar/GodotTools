using Godot;

namespace HaxiDenti.Tools;

public class AtlasUtil
{
    public static AtlasTexture[] Slice(Texture2D texture, int tileW, int tileH)
    {
        var (countX, countY) = texture.GetSize();
        AtlasTexture[] tileGrid = new AtlasTexture[(int)countX * (int)countY];
        int count = 0;
        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                tileGrid[count++] = new AtlasTexture
                {
                    Atlas = texture,
                    Region = new Rect2(x * tileW, y * tileH, tileW, tileH),
                };
            }
        }
        return tileGrid;
    }

    public static AtlasTexture[] Slice(string textureResource, int tileW, int tileH)
    {
        var texture = GD.Load<Texture2D>(textureResource);
        return Slice(texture, tileW, tileH);
    }
}
using Godot;

public class AtlasUtil
{
    public static AtlasTexture[] Slice(Texture2D masterTexture, int countX, int countY, int tileW, int tileH)
    {
        AtlasTexture[] tileGrid = new AtlasTexture[countX*countY];
        int count = 0;
        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                tileGrid[count++] = new AtlasTexture
                {
                    Atlas=masterTexture,
                    Region=new Rect2(x * tileW, y * tileH, tileW, tileH),
                };
            }
        }
        return tileGrid;
    }

    public static AtlasTexture[] Slice(string textureResource, int countX, int countY, int tileW, int tileH)
    {
        var texture = GD.Load<Texture2D>(textureResource);
        return Slice(texture, countX, countY, tileW, tileH);
    }
}
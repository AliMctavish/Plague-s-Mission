using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Effects
{
    public Vector2 position;
    public Texture2D texture;
    public Vector2 origin;

    public void CoinEffect(int x , int y)
    {
        this.position = new Vector2(x, y);
        this.origin = new Vector2(x, y);
    }

}

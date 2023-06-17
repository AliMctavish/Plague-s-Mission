using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class Effects
{
    public  Vector2 position;
    public Texture2D texture;
    public Vector2 origin;

    public void CoinEffect(int x , int y)
    {
        this.position = new Vector2(x, y);
        this.origin = new Vector2(x, y);
    }
}

public class HumanEffect
{
    public Rectangle position;
    public Texture2D texture;
    public bool isActivated = false;
    public int animationCounter = 1;

    public void Effect(int x , int y)
    {
        this.position = new Rectangle(x,y,80,80);
    }
}

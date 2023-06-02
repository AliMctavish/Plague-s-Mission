using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gravityProject
{
    public class Enemy
    {
        public Texture2D enemyTexture;

        public Rectangle enemyPos;

        public bool enemyIsFlipped = false;

        public int counter = 1;

        public bool isDead = false;

        public int attackCounter = 1;

        public Color Color = Color.White;

        public bool isStopped = false;

        public int Health = 100;

        public void Draw(int type)
        {
            if(type == 0)
                Globals.spriteBatch.Draw(enemyTexture, new Rectangle(enemyPos.X - 35, enemyPos.Y - 20, 90, 100), null, Color, 0, Vector2.Zero, SpriteEffects.None, 0);
                else
                Globals.spriteBatch.Draw(enemyTexture, new Rectangle(enemyPos.X - 35, enemyPos.Y - 20, 90, 100), null, Color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }

    }
}

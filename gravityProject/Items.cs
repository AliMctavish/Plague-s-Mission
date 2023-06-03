using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gravityProject
{
    public class Items
    {
        public Texture2D texture { get; set; }
        public Rectangle position { get; set; }
        public Texture2D injectTexture { get; set; }
        public int injectCounter = 1;
        public int CoinCounter = 1;
        public int ChestCounter = 1;


        public virtual void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Vector2(position.X - 35, position.Y - 35), Color.White);
        }
    }
    class Inject : Items
    {
        public override void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Vector2(position.X, position.Y - 45), Color.White);
        }

    }
    class Chest : Items
    {
        public bool isInside = false;
        public bool soundPlayed = false;
        public int animationCounter = 1;
        public float animateCounter = 0.1f;
        public static SoundEffect chestSound = Globals.Content.Load<SoundEffect>("sound");


        public override void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Vector2(position.X - 35, position.Y - 20), Color.White);
        }
    }
    class Trap : Items
    {
        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Vector2(position.X, position.Y - 45), Color.White);
        }
    }


    public class Platform : Items
    {
        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Rectangle(position.X - 80, position.Y - 40, 140, 64), Color.White);
        }
    }

    public class Ladder : Items
    {
        public void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Rectangle(position.X - 35, position.Y + 10, 47, 72), Color.White);
        }
    }
}

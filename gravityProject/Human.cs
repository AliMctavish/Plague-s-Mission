using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    public class Human : Items
    {
        public int Healing = 0;
        public int animationCounter = 1;
        public bool isCured = false;
        public override void Draw()
        {
            Globals.spriteBatch.Draw(texture, new Rectangle(position.X - 80, position.Y + 10, 70, 75), Color.White);
        }
    }
}

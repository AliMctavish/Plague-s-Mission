using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace gravityProject
{
    public class Ground 
    {
        public Rectangle GroundPos;
        public Texture2D groundTexture = null;
        public Rectangle groundWall;
        public Ground(int posX, int posY)
        {
            this.GroundPos = new Rectangle(posX, posY, 50 ,64);
        }
        public void Draw()
        {
            Globals.spriteBatch.Draw(groundTexture, new Vector2(GroundPos.X - 38,GroundPos.Y - 35), Color.White);
        }
    }
}

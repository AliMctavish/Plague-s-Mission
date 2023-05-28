using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    public class Human
    {
        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }
        public int Healing = 0;
        public int animationCounter = 1;

    }
}

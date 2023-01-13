using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class Items
    {
        public Texture2D chestTexture { get; set; }
        public Texture2D coinsTexture { get; set; }
        public Rectangle chestPos  {get; set; }
        public Rectangle coinsPos { get; set; }




     

     


        public Rectangle AddChest(int PosX , int PosY)

        {
            this.chestPos = new Rectangle(PosX, PosY, 64, 64);

            return chestPos;


        }


        public Rectangle AddCoins(int PosX , int PosY)
        {
            this.coinsPos = new Rectangle(PosX, PosY, 35, 35);
            return coinsPos;

        }
    }
}

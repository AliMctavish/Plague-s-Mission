using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

        public Rectangle injectPos;
        public Texture2D injectTexture { get; set; }

        public int injectCounter = 1;

        public int CoinCounter = 1;

        public int ChestCounter = 1;

    }

    class Inject : Items
    {
    
    
    }

}

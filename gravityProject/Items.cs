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
    internal class Items
    {
        public Texture2D texture { get; set; }
        public Rectangle position { get; set; }
        public Texture2D injectTexture { get; set; }
        public int injectCounter = 1;
        public int CoinCounter = 1;
        public int ChestCounter = 1;
    }
    class Inject : Items
    {


    }
    class Chest : Items
    {
        public bool isInside = false;
        public bool soundPlayed = false;
        public int animationCounter = 1;
        public float animateCounter = 0.1f;
        public static SoundEffect chestSound = Globals.Content.Load<SoundEffect>("sound");
    }
    class Trap : Items
    {


    }
}

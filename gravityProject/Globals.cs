using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace gravityProject
{
    internal class Globals 
    {
        public static float TotalSeconds {  get; set; }
        private GameServiceContainer _services;
        static public ContentManager Content { get; set; }
        public ContentManager _content;
        public static SpriteBatch spriteBatch { get; set; }
        public static void Update(GameTime gameTime)
        {
            TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gravityProject
{
    internal class Globals 
    {
        public static float TotalSeconds {  get; set; }
        private GameServiceContainer _services;
        public static GraphicsDeviceManager _graphics;
        static public ContentManager Content { get; set; }
        public ContentManager _content;
        public static SpriteBatch spriteBatch { get; set; }
        public static void Update(GameTime gameTime)
        {
            TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        //THIS METHOD IS CALLED IN DRAW SECTION OF THE GAME1
        public static void DrawObjects(Player player)
        {

            foreach (var ladder in LevelMapper.ladders) ladder.Draw();

            if (!player.isFlipped)
            {
                player.Draw(0);
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    player.isFlipped = true;
            }
            else
            {
                player.Draw(1);
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    player.isFlipped = false;
            }

            foreach (var trap in LevelMapper.traps)
                trap.Draw();

            foreach (var inject in LevelMapper.injects)
                inject.Draw();

            foreach (var platform in LevelMapper.platforms)
                platform.Draw();

            foreach (var human in LevelMapper.humans)
                human.Draw();



            //WATCH OUT ! DIRTY CODE A HEAD....
            //RENDERING ENEMIES WITH FLIPING ANIMATION
            foreach (var enemy in LevelMapper.enemies)
            {
                switch (enemy.enemyIsFlipped)
                {
                    case false:
                        {
                            enemy.Draw(0);
                            break;
                        }
                    case true:
                        {
                            enemy.Draw(1);
                            break;
                        }
                }
            }
        }
    }
}

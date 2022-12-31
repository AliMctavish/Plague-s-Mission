using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace gravityProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D texture2D;
        private Rectangle PlayerPos;
        private Ground[][] ground;
        private int jumpConuter = 0;
        private bool isFlipped = false;

        private string[] map = {"##########" ,
                                "##########"};

        //private string map = "####....###..####...999999999999############...................###########################.................";

        private int groundAxis =  50 ;
        private double timePassed=1d;
        bool hasJump = false;
        private Texture2D backgroundColor;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth= 1200;
            _graphics.PreferredBackBufferHeight= 600;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {


            ground = new Ground[map.Length][];
            // TODO: Add your initialization logic here
          
            texture2D = Content.Load<Texture2D>("playerCharacter");
            backgroundColor = Content.Load<Texture2D>("background");
            
               

            _font = Content.Load<SpriteFont>("File");
            PlayerPos = new Rectangle(300 , 100 , 35, 35);
                
            base.Initialize();

        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            for (int i = 0; i < map.Length; i++)
            {
                    for(int j = 0; j < map[i].Length; j++)
                    {
                    if (map[i][j] == '#')
                    {
                        ground[i][j] = new Ground(0 + groundAxis, i*4);
                        ground[i][j].groundTexture = Content.Load<Texture2D>("ground3");
                    }


                    }
                groundAxis += 50;
            }






            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {

            
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 140;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPos.X = PlayerPos.X + (int)time - 2  ;

                for (int i = 0; i < ground.Length; i++)
                { 
                    for(int j  = 0; j < ground[i].Length; j++)
                    {
                    ground[i][j].GroundPos.X = ground[i][j].GroundPos.X  - 1 - (int)time;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlayerPos.X = PlayerPos.X - (int)time + 2;
                for (int i = 0; i < ground.Length; i++)
                {
                    for(int j = 0; j < ground[i].Length; j++)
                    {
                        ground[i][j].GroundPos.X = ground[i][j].GroundPos.X + 1 + (int)time;

                    }
                }
            }
            for (int i = 0; i < ground.Length; i++)
            {
                for(int j = 0; j < ground[i].Length; j++)
                {
                    if (PlayerPos.Intersects(ground[i][j].GroundPos) && hasJump == false)
                    {

                        PlayerPos.Y -= 3;
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            hasJump = true;
                        }
                    }
                }
             
            }
                    PlayerPos.Y += 3;
            
            if(hasJump == true)
            {
                timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < 2; i++)
                {
                    PlayerPos.Y -= 2;
                }

                if(timePassed <= 0)
                {
                    timePassed = 1d;
                    hasJump = false;
                    jumpConuter++;
                }
                
            }



            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundColor, new Vector2(0, 0), Color.White);

            switch (isFlipped)
            {
                case false:
                    {
                        _spriteBatch.Draw(texture2D, new Rectangle(PlayerPos.X - 34, PlayerPos.Y - 34, PlayerPos.Width, PlayerPos.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            isFlipped = true;
                        }
                        break;
                    }
                case true:
                    {
                        _spriteBatch.Draw(texture2D, new Rectangle(PlayerPos.X - 34, PlayerPos.Y - 34, PlayerPos.Width, PlayerPos.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.D))
                        {
                            isFlipped = false;
                        }

                        break;
                    }
               
            }
          

            _spriteBatch.DrawString(_font,"Player Position On Y : " + PlayerPos.Y , new Vector2(10, 0), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Position On X : " + PlayerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + hasJump , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,60), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,80), color: Color.White);
            for(int i = 0; i < ground.Length; i++)
            {
                _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 35, ground[i].GroundPos.Y - 35), Color.White);

            }

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
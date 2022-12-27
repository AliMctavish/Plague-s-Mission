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
        private Ground[] ground;
        private Rectangle GroundPos;
        private Rectangle GroundPos2;
        private int jumpConuter = 0;
        private Vector2 movement;
        private bool isFlipped = false;
        private float sumVectors;
        private int groundRad =  230 ;
        private int PlayerRad = 15 + 15  ;
        SpriteEffect spriteEffect = null;
        private int sumRad;
        private int groundAxis =  50 ; 
        private double timePassed=1d;
        bool hasJump = false;
        private int increase;
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


            ground = new Ground[20];
            // TODO: Add your initialization logic here
          
            texture2D = Content.Load<Texture2D>("playerCharacter");
   

            _font = Content.Load<SpriteFont>("File");
            
            GroundPos = new Rectangle(300, 200, 50, 50);
            GroundPos2 = new Rectangle(300+50, 200, 50, 50);
            PlayerPos = new Rectangle(300 , 100 , 35, 35);
            movement = new Vector2(PlayerPos.X, PlayerPos.Y);
    
            
        


  
                               
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
         
            for (int i = 0; i < 20; i++)
            {
                ground[i] = new Ground(100 + groundAxis, 300);
                ground[i].groundTexture = Content.Load<Texture2D>("ground1");
                groundAxis += 50;
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            float time = (float)gameTime.ElapsedGameTime.TotalSeconds *50;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            //switch (true)
            //{
            //    case Keyboard.GetState().IsKeyDown(Keys.D):
            //        PlayerPos.X = PlayerPos.X + (int)time;
            //        break;

            //    case Keyboard.GetState().IsKeyDown(Keys.A):
            //        PlayerPos.X = PlayerPos.X - (int)time;
            //        break;
            //}


            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPos.X = PlayerPos.X + (int)time;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlayerPos.X = PlayerPos.X - (int)time;
            }

            for(int i = 0; i < ground.Length; i++)
            {
                if (PlayerPos.Intersects(ground[i].GroundPos) && hasJump == false)
                {
                    PlayerPos.Y -= 1;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        hasJump = true;
                    }
                }
                else
                {
                    PlayerPos.Y = PlayerPos.Y + (int)time;

                }
            }
            if(hasJump == true)
            {
                timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < 2; i++)
                {
                    PlayerPos.Y -= 1;
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
            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin();

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
            _spriteBatch.DrawString(_font,"Collide With Ground : " + PlayerPos.Intersects(GroundPos) , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + hasJump , new Vector2(10, 60), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,80), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,100), color: Color.White);
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private Rectangle GroundPos;
        private int jumpConuter = 0;
        private Texture2D ground; 
        private Vector2 movement;
        private float sumVectors;
        private int groundRad =  230 ;
        private int PlayerRad = 15 + 15  ;
        private int sumRad;
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
            // TODO: Add your initialization logic here
            texture2D  = new Texture2D(GraphicsDevice , 32 , 32);
            ground = new Texture2D(GraphicsDevice, 500, 20);
            _font = Content.Load<SpriteFont>("File");
            GroundPos = new Rectangle(300, 200, 500, 20);
            PlayerPos = new Rectangle(50 , 50 , 32, 32);
            movement = new Vector2(PlayerPos.X, PlayerPos.Y);
            var colorData = new Color[32 * 32];
            for(int i = 0; i < colorData.Length; i++)
            {
                colorData[i] = Color.Black;
            }

            var groundColor = new Color[500 * 20];

            for(int i = 0; i< groundColor.Length; i++)
            {

                groundColor[i] = Color.White;
            }

           // sumVectors = GroundPos.Size + PlayerPos.Size;

            sumRad = PlayerRad + groundRad;
            texture2D.SetData(colorData);

            ground.SetData(groundColor);
                               
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 240;
            
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


            if (PlayerPos.Intersects(GroundPos) && hasJump == false)
            {
                PlayerPos.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    hasJump = true;
                }
            }
            else
            {
                PlayerPos.Y += 1;

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

            _spriteBatch.Draw(texture2D,new Vector2(PlayerPos.X - 32, PlayerPos.Y - 32) , Color.White);

            _spriteBatch.DrawString(_font,"Player Position On Y : " + PlayerPos.Y , new Vector2(10, 0), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Position On X : " + PlayerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Collide With Ground : " + PlayerPos.Intersects(GroundPos) , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + hasJump , new Vector2(10, 60), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,80), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,100), color: Color.White);

            _spriteBatch.Draw(ground, new Vector2(GroundPos.X -32  , GroundPos.Y -32 ), Color.White);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
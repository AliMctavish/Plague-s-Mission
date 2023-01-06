using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
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
        float fallingTime = 10;

        Texture2D ballAnimation;
        
        private Rectangle ballPos;
        private Texture2D ballTexture;
        

        Maps level =  new Maps();

        private int velocity = 10;
        private int jumpConuter = 0;
        private bool isFlipped = false;
        int num = 0;


        int selectLevel = 6;

        private int groundAxis =  50 ;
        private double timePassed=2d;
        bool hasJump = false;
        private Texture2D backgroundColor;
        public Game1()
        {       
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
                _graphics.PreferredBackBufferWidth = 1600;
                _graphics.PreferredBackBufferHeight = 800;
                _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            int sumOfArrays = -1;
            foreach(var cell in level.LoadLevel(selectLevel))
            {
                sumOfArrays += cell.Length;
            }
            level = new Maps();



        



   
           ground = new Ground[sumOfArrays];



         

            Debug.WriteLine(level);



            // TODO: Add your initialization logic here

            ballTexture = Content.Load<Texture2D>("ball");

            texture2D = Content.Load<Texture2D>("playerCharacter");
            backgroundColor = Content.Load<Texture2D>("background-export");
            
               

            _font = Content.Load<SpriteFont>("File");

            ballPos = new Rectangle(300,100 , 32 ,32);
            PlayerPos = new Rectangle(500 , 200 , 35, 35);
                
            base.Initialize();

        }
        
        protected override void LoadContent()
        {
           

           

            string[] map = level.LoadLevel(selectLevel);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            int push = 0 ; 
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                        if (map[i][j] == '#')
                        {
                        ground[num] = new Ground(50 * j   , i * 50 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground3");
                            num++;
                        }
                        if (map[i][j] == '$')
                        {
                        ground[num] = new Ground(50 * j , i * 50 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground1");
                            num++;
                        }
                        if (map[i][j] == '%')
                        {
                            ground[num] = new Ground( 50* j , i * 50 + 50);
                            ground[num].groundTexture = Content.Load<Texture2D>("groundBase");
                            num++;
                         
                        }
                        if (map[i][j] == '.')
                        {
                            ground[num] = new Ground(900,900);
                            ground[num].groundTexture = Content.Load<Texture2D>("groundBase");
                            num++;
                        }
                     }
                groundAxis += 50;
            }






            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {









            float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 140;
            fallingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPos.X = PlayerPos.X + (int)time + 2;

                //for (int i = 0; i < ground.Length; i++)
                //{

                //    ground[i].GroundPos.X = ground[i].GroundPos.X + 1 - (int)time;

                //}
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    PlayerPos.X += 2;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlayerPos.X = PlayerPos.X - (int)time - 2;
                //for (int i = 0; i < ground.Length; i++)
                //{
                //    ground[i].GroundPos.X = ground[i].GroundPos.X - 1 + (int)time;
                //}
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    PlayerPos.X -= 2;
                }
            }



            for (int i = 0; i < ground.Length; i++)
            {

                if (PlayerPos.Intersects(ground[i].GroundPos))
                    {
                    if(PlayerPos.Y <= ground[i].GroundPos.Y)
                    {
                        PlayerPos.Y = ground[i].GroundPos.Y - PlayerPos.Height  ;
                    }
                  
                    else
                    {
                        if(isFlipped)
                        {
                        PlayerPos.X = ground[i].GroundPos.X + PlayerPos.Width + 15;
                        }
                        else
                        {
                        PlayerPos.X = ground[i].GroundPos.X - PlayerPos.Width;
                        }
                    }
                

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            hasJump = true;
                        } 
                }
                //if (PlayerPos.Intersects(ground[i].GroundPos) && hasJump == false && ground[i].groundTexture.Name == "groundBase" &&  isFlipped)
                //{
                //    PlayerPos.X = ground[i].GroundPos.X + PlayerPos.Width;
                //}
                //if(PlayerPos.Intersects(ground[i].GroundPos) && hasJump == false && ground[i].groundTexture.Name == "groundBase" && !isFlipped)
                //{
                //    PlayerPos.X = ground[i].GroundPos.X - PlayerPos.Width;
                //}
            }
            //falling
            PlayerPos.Y += 4 ;

            if (hasJump == true)
            {
                timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                PlayerPos.Y  = PlayerPos.Y - velocity ;

                if(timePassed <= 1.8)
                {
                    velocity = velocity - 1;
                    PlayerPos.Y = PlayerPos.Y  + velocity/2;
                    if (velocity <= 0 && timePassed <= 1.6)
                    {
                        hasJump = false;
                        timePassed = 2d;
                        velocity = 10;
                    }
                    //hasJump = false;
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
            _spriteBatch.Draw(ballTexture, new Vector2( PlayerPos.X -32 ,  PlayerPos.Y - 50), Color.White);
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

        
            //gameInfo
            _spriteBatch.DrawString(_font,"Player Position On Y : " + PlayerPos.Y , new Vector2(10, 0), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Position On X : " + PlayerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + hasJump , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,60), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,80), color: Color.White);
            //gameInfo

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
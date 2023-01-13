using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
using System.Threading;

namespace gravityProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D texture2D;
        private Rectangle PlayerPos;
        private Rectangle chestPos;
        private Texture2D  EnemyTexture;

        private Animate animate;
        private Ground[] ground;
        private Enemy[] enemies;
        private EnemyCollider[] enemyColliders;
        int moveCounter = 5;
        private Items[] items;
        float waitingTime = 0;
        float waitingTime2 = 0;
        int Counter = 1;

        
        private Rectangle ballPos;
        private Rectangle coinPos;
        private Texture2D ballTexture;
        private Texture2D coinTexture;
        private SoundEffect coinSound;
        private SoundEffect chestSound;
        double moveTimer = 1;
        float animateCounter2 = 0.1f;
        private int numberOfcoins = 0;

        private bool isInside = false;
        private bool played = false;

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







            items = new Items[sumOfArrays];
           ground = new Ground[sumOfArrays];
           enemies = new Enemy[sumOfArrays];
            enemyColliders = new EnemyCollider[sumOfArrays];





        
            Debug.WriteLine(level);



            // TODO: Add your initialization logic here

            ballTexture = Content.Load<Texture2D>("ball");

            texture2D = Content.Load<Texture2D>("animations/playerMovement1");
            backgroundColor = Content.Load<Texture2D>("background-export");
            EnemyTexture = Content.Load<Texture2D>("EnemyIdle1");
            coinSound = Content.Load<SoundEffect>("coinSound");
            chestSound = Content.Load<SoundEffect>("sound");
            coinTexture = Content.Load<Texture2D>("coin1");
            _font = Content.Load<SpriteFont>("File");



        
            PlayerPos = new Rectangle(500 , 200 , 76, 98);
           
                
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
                        ground[num] = new Ground(64 * j , i * 64 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground1");
                            num++;
                        }
                        if (map[i][j] == '%')
                        {
                            ground[num] = new Ground( 64* j , i * 64 + 50);
                            ground[num].groundTexture = Content.Load<Texture2D>("groundBase");
                            num++;
                         
                        } 
                        if (map[i][j] == 'y')
                        {
                            ground[num] = new Ground( 64* j , i * 64 + 50);
                            ground[num].groundTexture = Content.Load<Texture2D>("ground4");
                            num++;
                         
                        } if (map[i][j] == 'x')
                        {
                            ground[num] = new Ground( 64* j , i * 64 + 50);
                            ground[num].groundTexture = Content.Load<Texture2D>("ground4x");
                            num++;
                         
                        }
                    if (map[i][j] == '.')
                    {
                        //ground[num] = new Ground(900, 900);
                        //ground[num].groundTexture = Content.Load<Texture2D>("groundBase");
                        num++;
                    }
                    if (map[i][j] == '@')
                    {
                        items[num] = new Items();
                        items[num].coinsPos = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        items[num].coinsTexture = Content.Load<Texture2D>("coin1");

                        num++;
                    }
                    if (map[i][j] == '?')
                    {
                        items[num] = new Items();
                        items[num].chestPos = new Rectangle(64 * j, i * 64 + 61, 60, 60);
                        items[num].chestTexture = Content.Load<Texture2D>("chest1");
                        num++;
                    }
                    if (map[i][j] == '!')
                    {
                        enemies[num] = new Enemy();
                        enemies[num].enemyPos = new Rectangle(64 * j, i * 64, 61, 75);
                        enemies[num].enemyTexture = Content.Load<Texture2D>("EnemyIdle1");
                        num++;
                    }
                    if (map[i][j] == '|')
                    {
                        enemyColliders[num] = new EnemyCollider();
                        enemyColliders[num].ColliderPos = new Rectangle(64 * j, i * 64, 60, 60);
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
            waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;






            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                moveTimer += gameTime.ElapsedGameTime.TotalSeconds*6;
                for (int i = 1; i < 5; i++)
                {
                    if (moveTimer > i)
                    {
                        texture2D = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                    }
                    if (moveTimer > 5)
                    {
                        texture2D = Content.Load<Texture2D>($"animations/PlayerWalking5");
                        moveTimer = 1;
                    }
                }
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
                moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                for(int i = 1; i < 5; i++)
                {
                    if (moveTimer > i)
                    {
                        texture2D = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                    }
                    if(moveTimer > 5)
                    {
                        texture2D = Content.Load<Texture2D>($"animations/PlayerWalking5");
                        moveTimer = 1;
                    }   
                }
                //for (int i = 0; i < ground.Length; i++)
                //{
                //    ground[i].GroundPos.X = ground[i].GroundPos.X - 1 + (int)time;
                //}
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    PlayerPos.X -= 2;
                }
            }
       

            for(int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (PlayerPos.Intersects(items[i].chestPos))
                    {
                        isInside = true;
                        if (played == false)
                        {
                            chestSound.Play();
                            played = true;
                        }

                    }
                }
     
            }





            if (isInside)
            {
                for(int i = 2; i < items.Length; i++) 
                {
                waitingTime2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                float animateCounter = 0.1f ;
                    
                    if (waitingTime2 >= animateCounter)
                    {
                        if (items[i] != null)
                        {
                        items[i].chestTexture = Content.Load<Texture2D>($"chest{Counter}");
                        }
                        if(waitingTime2 >= 1)
                        {
                            isInside = false;
                        }
                        animateCounter += 0.1f;
                    }
                }

              
            }
        
            for(int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (PlayerPos.Intersects(items[i].coinsPos))
                    {
                        items[i] = null;
                       // coinSound.Play();
                        numberOfcoins++;
                    }
                }
            }

                

        
                if (waitingTime >= animateCounter2 )
                {
                for(int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        items[i].coinsTexture = Content.Load<Texture2D>($"coin{Counter}");
                    }
                }
            
                    
                

                    if (waitingTime <= 0.8)
                {
                    if( !Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                      texture2D = Content.Load<Texture2D>($"animations/playerMovement{Counter}");
                     
                    }
                    if (Counter < 4)
                    {
                        for (int i = 0; i < enemies.Length; i++)
                        {
                            if (enemies[i] != null)
                            {
                            enemies[i].enemyTexture = Content.Load<Texture2D>($"EnemyIdle{Counter}");
                            }

                        }

                    }
                }
                    if (waitingTime >= 1)
                    {
                    for(int i = 0; i < items.Length; i++)
                    {
                        if (items[i] != null)
                        {
                            items[i].coinsTexture = Content.Load<Texture2D>("coin11");
                        }
                    }
                    if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        texture2D = Content.Load<Texture2D>("animations/playerMovement8");
                        EnemyTexture = Content.Load<Texture2D>($"EnemyIdle4");
                    }

                        waitingTime = 0;
                        Counter = 1;
                        animateCounter2 = 0.001f;
                    }
                    Counter+=1;
                    animateCounter2 += 0.1f;
                }








            

            for (int i = 0; i < ground.Length; i++)
            {
                if (ground[i] != null)
                {
                    if (PlayerPos.Intersects(ground[i].GroundPos))
                    {
                        if (PlayerPos.Y <= ground[i].GroundPos.Y - 90 ) 
                        {
                            PlayerPos.Y = ground[i].GroundPos.Y - PlayerPos.Height;
                        }

                        else
                        {
                            if (isFlipped)
                            {
                                PlayerPos.X = ground[i].GroundPos.X + PlayerPos.Width - 15;
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

                }
            
       
            }
            for(int i = 0; i < enemies.Length; i++) 
            {
                if (enemies[i] != null )
                {
                   if(!enemies[i].enemyIsFlipped)
                    {
                    enemies[i].enemyPos.X += 1;
                    }
                    else
                    {
                        enemies[i].enemyPos.X -= 1;
                    }
                    for(int j = 0; j < enemyColliders.Length; j++)
                    {
                        if (enemyColliders[j] != null)
                        {
                            if (enemies[i].enemyPos.Intersects(enemyColliders[j].ColliderPos))
                            {
                                if (!enemies[i].enemyIsFlipped)
                                {
                                enemies[i].enemyIsFlipped = true;
                                }
                                else
                                {
                                    enemies[i].enemyIsFlipped = false;
                                }
                            }
                        }
                
                    }
                 
                }


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
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundColor, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(ballTexture, new Vector2( PlayerPos.X -32 ,  PlayerPos.Y - 50), Color.White);
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

            //Game Debugging Is Here
            _spriteBatch.DrawString(_font,"Player Position On Y : " + PlayerPos.Y , new Vector2(10, 0), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Position On X : " + PlayerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + hasJump , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,60), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,80), color: Color.White);
            _spriteBatch.DrawString(_font,"number of coins: " + numberOfcoins , new Vector2(10,100), color: Color.Black ,0 , new Vector2(0,0) ,2, 0 , 0);
            //Game Debugging Is Here
            for (int i = 0; i < ground.Length; i++)
            {
                if (ground[i] != null)
                {
                    _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 35, ground[i].GroundPos.Y - 35), Color.White);
                }
                    
            }

            for (int i = 0; i < items.Length; i++)
                {
                if (items[i] != null)
                {
                    if (items[i].coinsTexture != null)
                    {
                    _spriteBatch.Draw(items[i].coinsTexture, new Vector2(items[i].coinsPos.X - 35, items[i].coinsPos.Y - 35), Color.White);
                    }
                    if (items[i].chestTexture != null)
                    {
                        _spriteBatch.Draw(items[i].chestTexture, new Vector2(items[i].chestPos.X - 35, items[i].chestPos.Y - 35), Color.White);
                    }
                }
                }

            for(int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y  - 20, 90 , 100), Color.White);
                }

            }
              
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
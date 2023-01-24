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
 
       
        float timeTest = 0;

        private LevelMapper levelMapper = new LevelMapper();

        private Ground[] ground;
        private Enemy[] enemies;
        private EnemyCollider[] enemyColliders;
        Player player;
        int moveCounter = 5;
        private Items[] items;
        float waitingTime2 = 0;
        int Counter = 1;
        int Counter2 = 1;
        double EnemyCounter = 4;

        private int countForEnemyMovment = 1;
        

        private SoundEffect chestSound;
        private SoundEffect coinSound;
        double moveTimer = 1;

        float animateCounter2 = 0.1f;
   
        GamePhysics GamePhysics; 
        private float changingTimer = 0.1f;
        float waitingTime = 0;
        private int animateCounter = 1;
        private int numberOfcoins = 0;

        private bool isInside = false;
        private bool played = false;

        Maps level =  new Maps();
        AnimationManager animation;

        private int jumpConuter = 0;
        private bool isFlipped = false;
        
        int num = 0;


        int selectLevel = 6;

        private int groundAxis =  50 ;
        private double timePassed=2d;
      
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
            
         



            player = new Player();

            items = new Items[sumOfArrays];
           ground = new Ground[sumOfArrays];
           enemies = new Enemy[sumOfArrays];
            enemyColliders = new EnemyCollider[sumOfArrays];

            animation = new AnimationManager();





        
            Debug.WriteLine(level);



            // TODO: Add your initialization logic here


            player.playerTexture = Content.Load<Texture2D>("animations/playerMovement1");
            backgroundColor = Content.Load<Texture2D>("background-export");
            chestSound = Content.Load<SoundEffect>("sound");
            _font = Content.Load<SpriteFont>("File");
            coinSound = Content.Load<SoundEffect>("coinSound");



        
            player.playerPos = new Rectangle(500 , 200 , 76, 98);
           
                
            base.Initialize();

        }
        
        protected override void LoadContent()
        {
            GamePhysics = new GamePhysics();
            string[] map = level.LoadLevel(selectLevel);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
      
            levelMapper.StartMapping(ground , map , items , enemies, Content , enemyColliders);

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
                moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                for (int i = 1; i < 5; i++)
                {
                    if (moveTimer > i)
                    {
                        player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                    }
                    if (moveTimer > 5)
                    {
                        player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking5");
                        moveTimer = 1;
                    }
                }
                player.playerPos.X = player.playerPos.X + (int)time + 2;

                //for (int i = 0; i < ground.Length; i++)
                //{

                //    ground[i].GroundPos.X = ground[i].GroundPos.X + 1 - (int)time;

                //}
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    player.playerPos.X += 2;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.playerPos.X = player.playerPos.X - (int)time - 2;
                moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                for (int i = 1; i < 5; i++)
                {
                    if (moveTimer > i)
                    {
                        player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking{i}");
                    }
                    if (moveTimer > 5)
                    {
                        player.playerTexture = Content.Load<Texture2D>($"animations/PlayerWalking5");
                        moveTimer = 1;
                    }
                }
                //for (int i = 0; i < ground.Length; i++)
                //{
                //    ground[i].GroundPos.X = ground[i].GroundPos.X - 1 + (int)time;
                //}
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    player.playerPos.X -= 2;
                }
            }


            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (player.playerPos.Intersects(items[i].chestPos))
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
                float animateCounter = 0.1f;
                for (int i = 2; i < items.Length; i++)
                {
                    waitingTime2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                  

                    if (waitingTime2 >= animateCounter)
                    {
                        if (items[i] != null)
                        {
                            items[i].chestTexture = Content.Load<Texture2D>($"chest{Counter}");
                        }
                        if (waitingTime2 >= 1)
                        {
                            isInside = false;
                        }
                        animateCounter += 0.1f;
                    }
                }


            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (player.playerPos.Intersects(items[i].coinsPos))
                    {
                        items[i] = null;
                       // coinSound.Play();
                        numberOfcoins++;
                    }
                }
            }


            //solve this in the future
            // all the animtaions happen here and without methods or classes :"|


            if (waitingTime > animateCounter2)
            {
                animation.itemsAnimation(items, Content);
                animation.enemyAnimation(enemies , Content);
                animation.playerAnimationIdle(player, Content);
                animateCounter2 += 0.1f;
            }



            //colliders and physics methods

            if (player.hasJump == true)
            {
                player.timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                GamePhysics.PlayerGravity(player);
            }
            GamePhysics.PlayerBoundries(player, ground,isFlipped);
            GamePhysics.EnemyBoundaries(enemies,enemyColliders);

            player.playerPos.Y += 4 ;

           

      
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundColor, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(ballTexture, new Vector2( player.playerPos.X -32 ,  player.playerPos.Y - 50), Color.White);
            switch (isFlipped)
            {
                case false:
                    {
                        _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            isFlipped = true;
                        }
                        break;
                    }
                case true:
                    {
                        _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        if (Keyboard.GetState().IsKeyDown(Keys.D))
                        {
                            isFlipped = false;
                        }

                        break;
                    }
               
            }

            //Game Debugging Is Here
            _spriteBatch.DrawString(_font,"Player Position On Y : " + player.playerPos.Y , new Vector2(10, 0), color: Color.White);
            _spriteBatch.DrawString(_font,"Player Position On X : " + player.playerPos.X , new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font,"Ability To Jump : " + player.hasJump , new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font,"Time Since Jumped : " + timePassed , new Vector2(10,60), color: Color.White);
            _spriteBatch.DrawString(_font,"Jump Counter : " + jumpConuter , new Vector2(10,80), color: Color.White);
            _spriteBatch.DrawString(_font,"number of coins: " + numberOfcoins , new Vector2(10,100), color: Color.Black ,0 , new Vector2(0,0) ,2, 0 , 0);
            //Game Debugging Is Here
            for (int i = 0; i < ground.Length; i++)
            {
                if (ground[i] != null)
                {
                    _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 38 , ground[i].GroundPos.Y - 35), Color.White);
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
                    switch (enemies[i].enemyIsFlipped)
                    {
                        case false :
                            {
                                _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                                break;
                            }
                        case true:
                            {
                                _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                                break;
                            }
                    }
                  
                }

            }
              
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
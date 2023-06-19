using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace gravityProject
{
    public class Game1 : Game
    {
        public static SpriteFont _font;
        public static SpriteFont _fontLarge;
        private Texture2D coinCounter;
        private LevelMapper levelMapper = new LevelMapper();
        private List<EnemyCollider> enemyColliders;
        private MainMenu menu;
        Services services;
        Player player;
        float waitingTime2 = 0;
        private SoundEffect coinSound;
        private SoundEffect mainMusic;
        private SoundEffect healingSound;
        private SoundEffect pickHealer;
        private SoundEffect meleeSound;
        double moveTimer = 1;
        float animateCounter2 = 0.1f;
        GamePhysics GamePhysics;
        float waitingTime = 0;
        public static int numberOfcoins = 0;
        Maps level = new Maps();
        AnimationManager animation;
        private int jumpConuter = 0;
        int selectLevel = 1;
        public static bool gameStarted = false;
        bool restartCurrentLevel = false;
        private Texture2D backgroundColor;
        //GAME STATES
        public static bool gameInfo = false;
        public static bool exitGame = false;
        public static bool gameOver = false;
        public static bool keyPressed = false;
        public static bool missionCompleted = false;

        public Game1()
        {
            Globals._graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Globals._graphics.PreferredBackBufferWidth = 1600;
            Globals._graphics.PreferredBackBufferHeight = 800;
            Globals._graphics.ApplyChanges();
        }
        protected override void Initialize()
        {
            menu = new MainMenu();
            Globals.Content = Content;
            level = new Maps();
            coinCounter = Content.Load<Texture2D>("coin1");
            player = new Player();
            player.HealthBar = Content.Load<Texture2D>("HealthBar");
            enemyColliders = new List<EnemyCollider>();
            animation = new AnimationManager();
            GamePhysics = new GamePhysics(player);
            services = new Services(GamePhysics, animation);
            player.playerTexture = Content.Load<Texture2D>("animations/playerMovement1");
            backgroundColor = Content.Load<Texture2D>("backjana");
            _font = Content.Load<SpriteFont>("File");
            _fontLarge = Content.Load<SpriteFont>("FileLarge");
            coinSound = Content.Load<SoundEffect>("coinSound");
            healingSound = Content.Load<SoundEffect>("healingSound");
            pickHealer = Content.Load<SoundEffect>("pickHealer");
            meleeSound = Content.Load<SoundEffect>("meleeSound");
            mainMusic = Content.Load<SoundEffect>("mainMusic");
            player.playerPos = new Rectangle(500, 200, 76, 98);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMusic.Play();
            string[] map = level.LoadLevel(selectLevel);
            levelMapper.StartMapping(map, Content, enemyColliders, player);
        }
        public void ClearGame(int? level)
        {
            player.isDead = false;
            player.playerPos.X = 100;
            player.playerPos.Y = 100;
            if (level.HasValue)
            {
                selectLevel = level.Value;
                player.playerHealth = 100;
                numberOfcoins = 0;
            }
            enemyColliders.Clear();
            AnimationManager.counte = 1;
            LevelMapper.enemies.Clear();
            LevelMapper.grounds.Clear();
            LevelMapper.traps.Clear();
            LevelMapper.chests.Clear();
            LevelMapper.Items.Clear();
            LevelMapper.injects.Clear();
            LevelMapper.humans.Clear();
            LevelMapper.platforms.Clear();
            LevelMapper.ladders.Clear();
            Globals.numberOfCoinsCollected = 0;
            Globals.numberOfEnemyKilled = 0;
            player.hasSyringe = false;
            restartCurrentLevel = true;
        }
        protected override void Update(GameTime gameTime)
        {
            if (mainMusic == null)
            {
                mainMusic.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                gameOver = false;
                ClearGame(selectLevel);
            }

            if (LevelMapper.humans.Count() <= 0 && LevelMapper.humanEffects.Count() <= 0 && !Keyboard.GetState().IsKeyDown(Keys.R)) 
            {
                missionCompleted = true;
            }
            else
                missionCompleted = false;

            //boundaries for the player to not get out of bounds
            if (player.playerPos.X < 10)
            {
                player.playerPos.X += 6;
            }

            if (restartCurrentLevel == true)
            {
                string[] map = level.LoadLevel(selectLevel);
                levelMapper.StartMapping(map, Content, enemyColliders, player);
                restartCurrentLevel = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameStarted = false;

            if (exitGame)
                Exit();

            if (player.isDead == true)
            {
                player.hasJump = true;
                player.playerPos.Y += 10;
                if (player.playerPos.Y > 1200)
                    gameOver = true;
            }


            if (!player.isDead && gameStarted)
            {
                float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 140;
                waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                    //THIS IF STATEMENT TO PREVENT THE ATTACK ANIMATION WITH WALKING
                    if (!Keyboard.GetState().IsKeyDown(Keys.RightControl))
                    {
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
                    }
                    player.playerPos.X = player.playerPos.X + (int)time + 2;

                    if (player.playerPos.X > 1550)
                        player.playerPos.X -= 6;

                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        player.playerPos.X += 2;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.playerPos.X = player.playerPos.X - (int)time - 2;
                    moveTimer += gameTime.ElapsedGameTime.TotalSeconds * 6;
                    if (!Keyboard.GetState().IsKeyDown(Keys.RightControl))
                    {
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
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        player.playerPos.X -= 2;
                    }
                }
                //ANIMATIONS
                animation.ChestAnimation(player, waitingTime2, gameTime);
                if (waitingTime > animateCounter2)
                {
                    if (player.isShooting)
                    {
                        animation.PlayerHitAnimation();
                    }

                    services.AnimationService(player, gameTime, meleeSound) ;
                    animateCounter2 += 0.1f;
                }
                animation.PlatformMoving();

                //COLLIDERS AND PLAYER METHODS 
                services.PhysicsService(LevelMapper.grounds, player, enemyColliders, coinSound , healingSound , pickHealer);

                if (player.hasJump == true)
                {
                    player.timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GamePhysics.PlayerGravity();
                }
                //GamePhysics.playerHealing(player, items);
                player.playerPos.Y += 4;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.spriteBatch.Begin();

            if (!gameOver)
                Globals.spriteBatch.Draw(backgroundColor, new Rectangle(0, 0, 1600, 900), Color.White);

            //LOAD PLAYER HEALTH BAR
            if (!missionCompleted && !gameOver)
            {

                player.DrawHealthBar();

                Globals.spriteBatch.Draw(coinCounter, new Rectangle(1510, 50, 70, 70), color: Color.White);

                Globals.spriteBatch.DrawString(_font, "Number Of Humans: " + LevelMapper.humans.Count, new Vector2(100, 10), color: Color.White);

                Globals.spriteBatch.DrawString(_font, numberOfcoins + "x", new Vector2(1470, 67), color: Color.Wheat, 0, new Vector2(0, 0), 2, 0, 0);

                if (player.hasSyringe)
                    Globals.spriteBatch.Draw(Content.Load<Texture2D>("Health"), new Rectangle(10, 10, 140, 140), color: Color.White);

                foreach (var ground in LevelMapper.grounds)
                    ground.Draw();

                foreach (var item in LevelMapper.Items.ToList())
                    item.Draw();

                foreach (var effect in LevelMapper.effects.ToList())
                {
                    Globals.spriteBatch.DrawString(_font, "100", new Vector2(effect.position.X, effect.position.Y -= 3), color: Color.Yellow);

                    if (effect.GetType() == typeof(Effects))
                        if (effect.position.Y < effect.origin.Y - 100)
                            LevelMapper.effects.Remove(effect);
                }

                foreach (var effect in LevelMapper.humanEffects.ToList())
                    Globals.spriteBatch.Draw(effect.texture, effect.position, Color.White);

                foreach (var human in LevelMapper.humans)
                {
                    if (player.playerPos.Intersects(human.position))
                    {
                        if (!player.hasSyringe)
                            Globals.spriteBatch.DrawString(_font, "Syringe Needed!", new Vector2(human.position.X - 40, human.position.Y - 20), color: Color.Yellow);
                        else
                            Globals.spriteBatch.DrawString(_font, "Press 'E' To Heal!", new Vector2(human.position.X - 40, human.position.Y - 20), color: Color.Yellow);
                    }
                }

                foreach (var chest in LevelMapper.chests)
                {
                    if (player.playerPos.Intersects(chest.position) && !chest.isInside)
                    {
                        Globals.spriteBatch.DrawString(_font, "Press 'E' To Open", new Vector2(chest.position.X, chest.position.Y - 40), color: Color.GreenYellow);
                        Globals.spriteBatch.DrawString(_font, "You Need 10 Coins", new Vector2(chest.position.X, chest.position.Y - 20), color: Color.GreenYellow);
                        Globals.spriteBatch.Draw(coinCounter, new Rectangle(chest.position.X + 140, chest.position.Y - 10, 40, 40), color: Color.Wheat);
                    }
                    chest.Draw();
                }

                //LOOPING ON OBJECTS TO RENDER ON WINDOW
                Globals.DrawObjects(player);
            }


            //MISSION COMPLETED SUCCESSFULLY
            if (missionCompleted)
            {
                Globals.spriteBatch.Draw(backgroundColor, new Rectangle(0, 0, 1600, 900), new Color(43, 34, 53, 7));

                Globals.spriteBatch.DrawString(_fontLarge, $"Number of coins : {Globals.numberOfCoinsCollected} / {LevelMapper.Items.Count + Globals.numberOfCoinsCollected}", new Vector2(1200 / 2, 500 / 2), color: Color.White);

                Globals.spriteBatch.Draw(coinCounter, new Rectangle(950 / 2, 430 / 2, 90, 90), color: Color.White);

                Globals.spriteBatch.Draw(Content.Load<Texture2D>("EnemyIdle1"), new Rectangle(950 / 2, 600 / 2, 75, 90), color: Color.White);


                Globals.spriteBatch.DrawString(_fontLarge, $"Number of Enemies : {Globals.numberOfEnemyKilled} / {LevelMapper.enemies.Count + Globals.numberOfEnemyKilled}", new Vector2(1200 / 2, 650 / 2), color: Color.White);

                Globals.spriteBatch.DrawString(_fontLarge, "Mission Accomplished! Press Enter To Continue!", new Vector2(800 / 2, 1000 / 2), color: Color.White);


                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !keyPressed)
                {
                    if (!keyPressed)
                        keyPressed = true;
                    selectLevel++;
                    ClearGame(null);
                }
            }
            if (!missionCompleted)
                keyPressed = false;


            //GAMEOVER STATUS
            if (gameOver)
            {
                Globals.spriteBatch.Draw(backgroundColor, new Rectangle(0, 0, 1600, 900), Color.White);

                Globals.spriteBatch.Draw(Content.Load<Texture2D>("youLost-export"), new Rectangle(800 / 2, 300 / 2, 800, 400), Color.White);



                Globals.spriteBatch.DrawString(_fontLarge, "Your are dead , press 'R' to restart", new Vector2(1100 / 2, 600), color: Color.White);
            }

            //MainMenu
            if (gameStarted is false)
                menu.ButtonSelect();

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
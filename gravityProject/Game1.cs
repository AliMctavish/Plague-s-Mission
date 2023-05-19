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
        private Texture2D HealthBar;
        private Texture2D coinCounter;
        private LevelMapper levelMapper = new LevelMapper();
        private List<Ground> ground;
        private List<Enemy> enemies;
        private List<EnemyCollider> enemyColliders;
        Player player;
        int moveCounter = 5;
        private List<Items> items;
        float waitingTime2 = 0;
        int Counter = 1;
        private SoundEffect chestSound;
        private SoundEffect coinSound;
        double moveTimer = 1;
        float animateCounter2 = 0.1f;
        GamePhysics GamePhysics;
        float waitingTime = 0;
        public static int numberOfcoins = 0;
        Maps level = new Maps();
        AnimationManager animation;
        private int jumpConuter = 0;
        private bool isFlipped = false;
        int selectLevel = 6;
        bool lol = false;
        private double timePassed = 2d;
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
            Globals.Content = Content;
            level = new Maps();
            HealthBar = Content.Load<Texture2D>("HealthBar");
            coinCounter = Content.Load<Texture2D>("coin1");
            player = new Player();
            items = new List<Items>();
            ground = new List<Ground>();
            enemies = new List<Enemy>();
            enemyColliders = new List<EnemyCollider>();
            animation = new AnimationManager();
            // TODO: Add your initialization logic here6
            player.playerTexture = Content.Load<Texture2D>("animations/playerMovement1");
            backgroundColor = Content.Load<Texture2D>("background-export");
            chestSound = Content.Load<SoundEffect>("sound");
            _font = Content.Load<SpriteFont>("File");
            coinSound = Content.Load<SoundEffect>("coinSound");
            player.playerPos = new Rectangle(500, 200, 76, 98);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            GamePhysics = new GamePhysics();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }
        public void ClearGame()
        {
            player.isDead = false;
            player.playerPos.X = 100;
            player.playerPos.Y = 100;
            player.playerHealth = 100;
            numberOfcoins = 0;
            enemies.Clear();
            enemyColliders.Clear();
            items.Clear();
            ground.Clear();
            LevelMapper.chests.Clear();
            lol = true;
        }
        protected override void Update(GameTime gameTime)
        {
            for (int i = 1; i < 6; i++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D0 + i))
                {
                    ClearGame();
                    selectLevel = i;
                }
            }

            if (lol == true)
            {
                string[] map = level.LoadLevel(selectLevel);
                levelMapper.StartMapping(ground, map, items, enemies, Content, enemyColliders, player);
                lol = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (player.isDead == true)
            {
                player.hasJump = true;
                player.playerPos.Y += 10;
            }
            if (player.isDead == false)
            {
                float time = (float)gameTime.ElapsedGameTime.TotalSeconds * 140;
                waitingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                    {
                        player.playerPos.X -= 2;
                    }
                }

                //ANIMATIONS
                GamePhysics.playerIntersectsWithCoins(player, coinSound, items);
                animation.ChestAnimation(player, chestSound, waitingTime2, gameTime);
                if (waitingTime > animateCounter2)
                {
                    animation.itemsAnimation(items, Content);
                    animation.enemyAnimation(enemies, Content);
                    animation.playerAnimationIdle(player, Content);
                    animateCounter2 += 0.1f;
                }

                //COLLIDERS AND PLAYER METHODS 
                if (player.hasJump == true)
                {
                    player.timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GamePhysics.PlayerGravity(player);
                }
                GamePhysics.PlayerIntersectsWithTrap(player);
                GamePhysics.PlayerIntersectsWithGround(player, ground, isFlipped);
                GamePhysics.PlayerIntersectsWithEnemy(player, enemies);
                GamePhysics.EnemyBoundaries(enemies, enemyColliders);
                //GamePhysics.playerHealing(player, items);
                player.playerPos.Y += 4;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundColor, new Rectangle(0, 0, 1600, 900), Color.White);
            if (!isFlipped)
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, player.playerColor, 0, Vector2.Zero, SpriteEffects.None, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    isFlipped = true;
                }
            }
            else
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, player.playerColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    isFlipped = false;
                }
            }
            //Game Debugging Is Here
            _spriteBatch.DrawString(_font, "Player Position On Y : " + player.playerPos.Y, new Vector2(10, 0), color: Color.White);

            for (int i = 0; i < player.playerHealth; i++)
            {
                if (i == 80)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1400, 10, 40, 40), color: Color.White);
                }
                if (i == 60)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1440, 10, 40, 40), color: Color.White);
                }
                if (i == 20)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1480, 10, 40, 40), color: Color.White);
                }
                if (i == 5)
                {
                    _spriteBatch.Draw(HealthBar, new Rectangle(1520, 10, 40, 40), color: Color.White);
                }

            }
            _spriteBatch.Draw(coinCounter, new Rectangle(1510, 50, 70, 70), color: Color.White);

            _spriteBatch.DrawString(_font, "Player Position On X : " + player.playerPos.X, new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font, "Ability To Jump : " + player.hasJump, new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font, "Time Since Jumped : " + timePassed, new Vector2(10, 60), color: Color.White);
            _spriteBatch.DrawString(_font, "Jump Counter : " + jumpConuter, new Vector2(10, 80), color: Color.White);
            _spriteBatch.DrawString(_font, "Player Health : " + player.playerHealth, new Vector2(10, 140), color: Color.White);
            _spriteBatch.DrawString(_font, numberOfcoins + "x", new Vector2(1470, 67), color: Color.Black, 0, new Vector2(0, 0), 2, 0, 0);
            //Game Debugging Is Here
            for (int i = 0; i < ground.Count; i++)
                _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 38, ground[i].GroundPos.Y - 35), Color.White);


            for (int i = 0; i < items.Count; i++)
                _spriteBatch.Draw(items[i].texture, new Vector2(items[i].position.X - 35, items[i].position.Y - 35), Color.White);


            foreach (var chest in LevelMapper.chests)
            {
                if (player.playerPos.Intersects(chest.position) && !chest.isInside)
                {
                    _spriteBatch.DrawString(_font, "Press 'E' To Open", new Vector2(chest.position.X, chest.position.Y - 40), color: Color.GreenYellow);
                    //_spriteBatch.DrawString(_font, "You Need 10 Coins", new Vector2(chest.position.X, chest.position.Y - 20), color: Color.GreenYellow);
                    //_spriteBatch.Draw(coinCounter, new Rectangle(chest.position.X + 140, chest.position.Y - 10, 40, 40), color: Color.Wheat);
                }
                _spriteBatch.Draw(chest.texture, new Vector2(chest.position.X - 35, chest.position.Y - 20), Color.White);
            }

            foreach (var trap in LevelMapper.traps)
                _spriteBatch.Draw(trap.texture, new Vector2(trap.position.X - 38, trap.position.Y - 35), Color.White);


            for (int i = 0; i < enemies.Count; i++)
            {

                switch (enemies[i].enemyIsFlipped)
                {
                    case false:
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
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
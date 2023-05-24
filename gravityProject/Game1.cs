using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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
        private SoundEffect coinSound;
        double moveTimer = 1;
        float animateCounter2 = 0.1f;
        GamePhysics GamePhysics;
        float waitingTime = 0;
        public static int numberOfcoins = 0;
        Maps level = new Maps();
        AnimationManager animation;
        private int jumpConuter = 0;
        int selectLevel = 1;
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
            _font = Content.Load<SpriteFont>("File");
            coinSound = Content.Load<SoundEffect>("coinSound");
            player.playerPos = new Rectangle(500, 200, 76, 98);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            GamePhysics = new GamePhysics();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            string[] map = level.LoadLevel(selectLevel);
            levelMapper.StartMapping(ground, map, enemies, Content, enemyColliders, player);
            // TODO: use this.Content to load your game content here
        }
        public void ClearGame()
        {
            player.isDead = false;
            player.playerPos.X = 100;
            player.playerPos.Y = 100;
            //player.playerHealth = 100;
            //numberOfcoins = 0;
            enemies.Clear();
            enemyColliders.Clear();
            items.Clear();
            ground.Clear();
            LevelMapper.traps.Clear();
            LevelMapper.chests.Clear();
            LevelMapper.Items.Clear();
            LevelMapper.injects.Clear();
            lol = true;
        }
        protected override void Update(GameTime gameTime)
        {

            if(player.playerPos.X > _graphics.PreferredBackBufferWidth)
            {
                selectLevel++;
                ClearGame();
            }
            if(player.playerPos.X < -10)
            {
                selectLevel--;
                ClearGame();
            }

            if (lol == true)
            {
                string[] map = level.LoadLevel(selectLevel);
                levelMapper.StartMapping(ground, map, enemies, Content, enemyColliders, player);
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
                GamePhysics.playerIntersectsWithCoins(player, coinSound);
                animation.ChestAnimation(player ,waitingTime2, gameTime);
                if (waitingTime > animateCounter2)
                {
                    if (player.isShooting)
                        animation.PlayerHitAnimation();

                    animation.injectAnimation(gameTime);
                    animation.itemsAnimation(Content);
                    animation.enemyAnimation(enemies, Content);
                    animation.playerAnimationIdle(player, Content);
                    animateCounter2 += 0.1f;
                }
                animation.PlatformMoving();

                //COLLIDERS AND PLAYER METHODS 
                if (player.hasJump == true)
                {
                    player.timePassed -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    GamePhysics.PlayerGravity(player);
                }

                GamePhysics.PlayerIntersectsWithChest(player);
                GamePhysics.playerHealing(player);
                GamePhysics.PlayerIntersectsWithTrap(player);
                GamePhysics.PlayerIntersectsWithGround(player, ground, player.isFlipped);
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
            if (!player.isFlipped)
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, player.playerColor, 0, Vector2.Zero, SpriteEffects.None, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.isFlipped = true;
                }
            }
            else
            {
                _spriteBatch.Draw(player.playerTexture, new Rectangle(player.playerPos.X - 34, player.playerPos.Y - 34, player.playerPos.Width, player.playerPos.Height), null, player.playerColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.isFlipped = false;
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

            //GAME DEGUGGING IS HERE 

            _spriteBatch.DrawString(_font, "Player Position On X : " + player.playerPos.X, new Vector2(10, 20), color: Color.White);
            _spriteBatch.DrawString(_font, "Ability To Jump : " + player.hasJump, new Vector2(10, 40), color: Color.White);
            _spriteBatch.DrawString(_font, "Time Since Jumped : " + timePassed, new Vector2(10, 60), color: Color.White);
            _spriteBatch.DrawString(_font, "Jump Counter : " + jumpConuter, new Vector2(10, 80), color: Color.White);
            _spriteBatch.DrawString(_font, "Player Health : " + player.playerHealth, new Vector2(10, 140), color: Color.White);
            _spriteBatch.DrawString(_font, numberOfcoins + "x", new Vector2(1470, 67), color: Color.Black, 0, new Vector2(0, 0), 2, 0, 0);

            for (int i = 0; i < ground.Count; i++)
                _spriteBatch.Draw(ground[i].groundTexture, new Vector2(ground[i].GroundPos.X - 38, ground[i].GroundPos.Y - 35), Color.White);

            foreach(var item in LevelMapper.Items.ToList())
                if(item != null)
                _spriteBatch.Draw(item.texture, new Vector2(item.position.X - 35, item.position.Y - 35), Color.White);


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
            //LOOPING ON OBJECTS TO RENDER ON WINDOW

            foreach (var trap in LevelMapper.traps)
                _spriteBatch.Draw(trap.texture, new Vector2(trap.position.X , trap.position.Y - 45), Color.White);

            foreach(var inject in  LevelMapper.injects)
                _spriteBatch.Draw(inject.texture, new Vector2(inject.position.X, inject.position.Y - 45), Color.White);

            foreach(var platform in LevelMapper.platforms.ToList())
                _spriteBatch.Draw(platform.texture, new Rectangle(platform.position.X - 80, platform.position.Y - 40,140,64), Color.White);

            //WATCH OUT ! DIRTY CODE A HEAD....
            //RENDERING ENEMIES WITH FLIPING ANIMATION
            for (int i = 0; i < enemies.Count; i++)
            {

                switch (enemies[i].enemyIsFlipped)
                {
                    case false:
                        {
                            _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, enemies[i].Color, 0, Vector2.Zero, SpriteEffects.None, 0);
                            break;
                        }
                    case true:
                        {
                            _spriteBatch.Draw(enemies[i].enemyTexture, new Rectangle(enemies[i].enemyPos.X - 35, enemies[i].enemyPos.Y - 20, 90, 100), null, enemies[i].Color, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
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
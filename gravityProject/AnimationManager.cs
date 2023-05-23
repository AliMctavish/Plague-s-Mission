using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class AnimationManager
    {
        float counte = 1;

        public void itemsAnimation(ContentManager Content)
        {
            foreach (var item in LevelMapper.Items)
            {
                item.texture = Content.Load<Texture2D>($"coin{item.CoinCounter}");

                if (item.CoinCounter == 11)
                {
                    item.texture = Content.Load<Texture2D>("coin11");
                    item.CoinCounter = 1;
                }
                item.CoinCounter += 1;
            }
        }

        public void PlatformMoving()
        {
            float sinTheta = MathF.Sin(counte++/120) * 2f;
            foreach(var platform in LevelMapper.platforms.ToList())
            { 
                platform.position = new Rectangle(platform.position.X + (int)sinTheta , platform.position.Y ,40,40);
            }
        }
        public void injectAnimation(GameTime gameTime)
        {
            float posY = MathF.Sin(counte++/10) * 2f;
            foreach (var inject in LevelMapper.injects)
            {
                inject.position = new Rectangle(inject.position.X,inject.position.Y + (int)posY, 40, 40 );
            }
            if(counte > 10000)
                counte = 1;
        }
        public void enemyAnimation(List<Enemy> enemies, ContentManager Content)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.isStopped)
                {
                    enemy.enemyTexture = Content.Load<Texture2D>($"EnemyMoving{enemy.counter}");
                    if (enemy.counter == 4)
                    {
                        enemy.enemyTexture = Content.Load<Texture2D>("EnemyMoving4");
                        enemy.counter = 1;
                    }
                    enemy.counter += 1;
                }
                else
                {
                    enemy.enemyTexture = Content.Load<Texture2D>($"animations/EnemyAttack{enemy.attackCounter}");
                    if (enemy.attackCounter == 6)
                    {
                        enemy.enemyTexture = Content.Load<Texture2D>("animations/EnemyAttack1");
                        enemy.attackCounter = 1;
                    }
                    enemy.attackCounter += 1;
                }
            }
        }

        public void ChestAnimation(Player player, float waitingTime2, GameTime gameTime)
        {
            for (int i = 0; i < LevelMapper.chests.Count(); i++)
            {
                if (LevelMapper.chests[i].isInside)
                {
                    waitingTime2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (LevelMapper.chests[i].animationCounter < 9)
                    {
                        LevelMapper.chests[i].texture = Globals.Content.Load<Texture2D>($"chest{LevelMapper.chests[i].animationCounter}");
                        LevelMapper.chests[i].animationCounter++;
                    }
                    if (waitingTime2 >= 1)
                    {
                        LevelMapper.chests[i].isInside = false;
                    }
                }
            }
        }
        public void playerAnimationIdle(Player player, ContentManager Content)
        {
            player.playerPos = new Rectangle(player.playerPos.X, player.playerPos.Y, player.playerPos.Width , player.playerPos.Height );
            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.playerTexture = Content.Load<Texture2D>($"animations/playerMovement{player.PlayerAnimationCounter}");
            }
            player.PlayerAnimationCounter += 1;
            if (player.PlayerAnimationCounter == 8)
            {
                player.PlayerAnimationCounter = 1;
            }
        }
    }
}

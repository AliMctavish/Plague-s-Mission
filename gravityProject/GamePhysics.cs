﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace gravityProject
{
    public class GamePhysics
    {
        private Player player;
        public GamePhysics(Player player)
        {
            this.player = player;
        }
        public void EnemyBoundaries(List<Enemy> enemies, List<EnemyCollider> enemyColliders)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.isStopped)
                {
                    var res =
                        !enemy.enemyIsFlipped ? enemy.enemyPos.X += 1 : enemy.enemyPos.X -= 1;
                    foreach (var enemyCollider in enemyColliders)
                    {
                        if (enemy.enemyPos.Intersects(enemyCollider.ColliderPos))
                        {
                            if (!enemy.enemyIsFlipped)
                            {
                                enemy.enemyIsFlipped = true;
                            }
                            else
                            {
                                enemy.enemyIsFlipped = false;
                            }
                        }
                    }
                }
            }
        }
        public void PlayerIntersectsWithGround(List<Ground> grounds, bool isFlipped)
        {
            foreach (var platform in LevelMapper.platforms)
            {
                if (player.playerPos.Intersects(platform.position))
                {
                    if (player.playerPos.Y <= platform.position.Y - 90)
                    {
                        player.playerPos.Y = platform.position.Y - player.playerPos.Height;
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            player.hasJump = true;
                        }
                        else
                        {
                            player.playerPos.X = platform.position.X;
                        }
                    }
                }
            }

            foreach (var ground in grounds)
            {
                if (player.playerPos.Intersects(ground.GroundPos))
                {
                    if (player.playerPos.Y <= ground.GroundPos.Y - 90)
                    {
                        player.playerPos.Y = ground.GroundPos.Y - player.playerPos.Height;
                    }
                    else
                    {
                        if (isFlipped)
                        {
                            if (player.playerPos.Y <= ground.GroundPos.Y)
                            {
                                player.playerPos.X = ground.GroundPos.X + player.playerPos.Width - 24;
                            }
                        }
                        else
                        {
                            if (player.playerPos.Y <= ground.GroundPos.Y)
                            {
                                player.playerPos.X = ground.GroundPos.X - player.playerPos.Width - 10;
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        player.hasJump = true;
                    }
                }
            }
        }
        public void PlayerIntersectsWithTrap()
        {
            foreach (var trap in LevelMapper.traps)
            {
                if (player.playerPos.Intersects(trap.position))
                {
                    player.isDead = true;
                }
            }
        }
        public void PlayerIntersectWithHumans()
        {
            foreach (var human in LevelMapper.humans.ToList())
            {
                if (player.playerPos.Intersects(human.Position))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.E) && player.hasSyringe)
                    {
                        player.hasSyringe = false;
                        LevelMapper.humans.Remove(human);
                    }
                }
            }
        }
        public void EnemyIsDead(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.isDead)
                    enemy.enemyPos.Y += 10;

                if (enemy.enemyPos.Y < 800)
                    LevelMapper.enemies.Remove(enemy);
            }
        }
        public bool PlayerGravity()
        {
            player.playerPos.Y = player.playerPos.Y - player.playerVelocity;
            if (player.timePassed <= 1.8)
            {
                player.playerVelocity = player.playerVelocity - 1;
                player.playerPos.Y = player.playerPos.Y + player.playerVelocity / 2;
                if (player.playerVelocity <= 0 && player.timePassed <= 1.5)
                {
                    player.timePassed = 2d;
                    player.playerVelocity = 10;
                    return player.hasJump = false;
                }
                //player.hasJump = false;
            }
            return player.hasJump = true;
        }
        public Color PlayerIntersectsWithEnemy(List<Enemy> enemies)
        {
            foreach (var enemy in enemies.ToList())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.RightControl))
                {
                    player.isShooting = true;
                    if (player.playerPos.Intersects(enemy.enemyPos))
                    {
                        enemy.Health -= 5;
                        enemy.Color = Color.Red;
                        if (!player.isFlipped)
                        {
                            enemy.enemyPos = new Rectangle(enemy.enemyPos.X + 1, enemy.enemyPos.Y, 40, 40);
                        }
                        else
                        {
                            enemy.enemyPos = new Rectangle(enemy.enemyPos.X - 1, enemy.enemyPos.Y, 40, 40);
                        }

                        if (enemy.Health <= 0)
                            enemy.isDead = true;
                    }
                }
                else
                {
                    enemy.Color = Color.White;
                }

                if (player.playerPos.Intersects(enemy.enemyPos))
                {
                    enemy.isStopped = true;
                    PlayerTakingDamage();
                    if (player.playerPos.X > enemy.enemyPos.X)
                    {
                        enemy.enemyIsFlipped = false;
                    }
                    else
                    {
                        enemy.enemyIsFlipped = true;
                    }
                    return player.playerColor = Color.Red;
                }
                else
                {
                    enemy.isStopped = false;
                }
            }
            return player.playerColor = Color.White;
        }
        public void playerHealing()
        {
            foreach (var inject in LevelMapper.injects.ToList())
            {
                if (player.playerPos.Intersects(inject.position) && !player.hasSyringe)
                {
                    player.playerHealth += 20;
                    player.playerColor = Color.Green;
                    player.hasSyringe = true;
                    LevelMapper.injects.Remove(inject);
                }
            }
        }
        private void PlayerTakingDamage()
        {
            player.playerHealth -= 1;
            if (player.playerHealth == 0)
            {
                player.isDead = true;
            }
        }
        public void PlayerIntersectsWithChest()
        {
            foreach (var chest in LevelMapper.chests)
            {
                if (player.playerPos.Intersects(chest.position))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        chest.isInside = true;

                        if (chest.soundPlayed == false)
                        {
                            Items item = new Items();
                            item.position =
                                new Rectangle(chest.position.X + 20, chest.position.Y, 40, 40);
                            item.texture = Globals.Content.Load<Texture2D>("coin1");

                            LevelMapper.Items.Add(item);
                            Chest.chestSound.Play();
                            chest.soundPlayed = true;
                        }
                    }
                }
            }
        }
        public void playerIntersectsWithCoins(SoundEffect coinSound)
        {
            foreach (var item in LevelMapper.Items.ToList())
            {
                if (player.playerPos.Intersects(item.position))
                {
                    Effects effect = new Effects();
                    effect.CoinEffect(item.position.X, item.position.Y);
                    LevelMapper.effects.Add(effect);
                    LevelMapper.Items.Remove(item);
                    coinSound.Play();
                    Game1.numberOfcoins++;
                }
            }
        }

    }
}

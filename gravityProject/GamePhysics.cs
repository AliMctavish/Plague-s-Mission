using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace gravityProject
{
    internal class GamePhysics
    {
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
        public void PlayerIntersectsWithGround(Player player, List<Ground> grounds, bool isFlipped)
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
                     player.playerPos.Y = ground.GroundPos.Y -player.playerPos.Height;
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
        public void PlayerIntersectsWithTrap(Player player)
        {
            foreach (var trap in LevelMapper.traps)
            {
                if (player.playerPos.Intersects(trap.position))
                {
                    player.isDead = true;
                }
            }
        }
        public bool PlayerGravity(Player player)
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
        public Color PlayerIntersectsWithEnemy(Player player, List<Enemy> enemies)
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
                            enemies.Remove(enemy);
                    }
                }
                else
                {
                    enemy.Color = Color.White;
                }

                if (player.playerPos.Intersects(enemy.enemyPos))
                {
                    enemy.isStopped = true;
                    PlayerTakingDamage(player);
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
        public void playerHealing(Player player)
        {
            foreach (var inject in LevelMapper.injects.ToList())
            {
                if (player.playerPos.Intersects(inject.position))
                {
                    player.playerHealth += 20;
                    player.playerColor = Color.Green;
                    LevelMapper.injects.Remove(inject);
                }
            }
        }
        private void PlayerTakingDamage(Player player)
        {
            player.playerHealth -= 1;
            if (player.playerHealth == 0)
            {
                player.isDead = true;
            }
        }
        public void PlayerIntersectsWithChest(Player player)
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
        public void playerIntersectsWithCoins(Player player, SoundEffect coinSound)
        {
            foreach (var item in LevelMapper.Items.ToList())
            {
                if (player.playerPos.Intersects(item.position))
                {
                    LevelMapper.Items.Remove(item);
                    coinSound.Play();
                    Game1.numberOfcoins++;
                }
            }
        }

    }
}

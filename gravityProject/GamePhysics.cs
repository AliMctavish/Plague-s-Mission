using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class GamePhysics
    {

        public void EnemyBoundaries(Enemy[] enemies , EnemyCollider[] enemyColliders )
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    if (!enemies[i].enemyIsFlipped)
                    {
                        enemies[i].enemyPos.X += 1;
                    }
                    else
                    {
                        enemies[i].enemyPos.X -= 1;
                    }
                    for (int j = 0; j < enemyColliders.Length; j++)
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
        }

        public void getItems(Items[] items)
        {

        }



        public void PlayerBoundries(Player player ,Ground[] ground, bool isFlipped )
        {
            for (int i = 0; i < ground.Length; i++)
            {
                if (ground[i] != null)
                {
                    if (player.playerPos.Intersects(ground[i].GroundPos))
                    {
                        if (player.playerPos.Y <= ground[i].GroundPos.Y - 90)
                        {
                            player.playerPos.Y = ground[i].GroundPos.Y - player.playerPos.Height;
                        }

                        else
                        { 
                            if (isFlipped)
                            {
                                if(player.playerPos.Y <= ground[i].GroundPos.Y )
                                {
                                player.playerPos.X = ground[i].GroundPos.X + player.playerPos.Width - 24;
                                }
                            }
                            else
                            {
                                if (player.playerPos.Y <= ground[i].GroundPos.Y )
                                {
                                 player.playerPos.X = ground[i].GroundPos.X - player.playerPos.Width;
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

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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



        public void PlayerBoundries(Rectangle PlayerPos ,Ground[] ground, bool hasJump, bool isFlipped )
        {
            for (int i = 0; i < ground.Length; i++)
            {
                if (ground[i] != null)
                {
                    if (PlayerPos.Intersects(ground[i].GroundPos))
                    {
                        if (PlayerPos.Y <= ground[i].GroundPos.Y - 90)
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
        }





    }
}

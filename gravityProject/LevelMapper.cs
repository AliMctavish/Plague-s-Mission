using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class LevelMapper
    {


        int num = 0;
        private int groundAxis = 50;

        public void StartMapping(Ground[] ground , string[] map , Items[] items , Enemy[] enemies , ContentManager Content , EnemyCollider[] enemyColliders)
        {

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        ground[num] = new Ground(50 * j, i * 50 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground3");
                        num++;
                    }
                    if (map[i][j] == '$')
                    {
                        ground[num] = new Ground(64 * j, i * 64 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground1");
                        num++;
                    }
                    if (map[i][j] == '%')
                    {
                        ground[num] = new Ground(64 * j, i * 64 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("groundBase");
                        num++;

                    }
                    if (map[i][j] == 'y')
                    {
                        ground[num] = new Ground(64 * j, i * 64 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground4");
                        num++;

                    }
                    if (map[i][j] == 'x')
                    {
                        ground[num] = new Ground(64 * j, i * 64 + 50);
                        ground[num].groundTexture = Content.Load<Texture2D>("ground4x");
                        num++;

                    }
                    if (map[i][j] == '.')
                    {
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
                    if (map[i][j] == '+')
                    {
                        items[num] = new Items();
                        items[num].injectPos = new Rectangle(64 * j, i * 64 + 61, 60, 60);
                        items[num].injectTexture = Content.Load<Texture2D>("Health");
                        num++;
                    }
                    if (map[i][j] == '!')
                    {
                        enemies[num] = new Enemy();
                        enemies[num].enemyPos = new Rectangle(64 * j, i * 64, 61, 75);
                        enemies[num].enemyTexture = Content.Load<Texture2D>("EnemyMoving1");
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




        }



    }
}

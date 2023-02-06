using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    internal class LevelMapper
    {


        int num = 0;
        private int groundAxis = 50;


        private Items item = new Items();
        private Ground ground= new Ground(0,0);
        private Enemy enemy = new Enemy();
        private EnemyCollider enemyCollider = new EnemyCollider();



        public void StartMapping(List <Ground> grounds , string[] map , List <Items> items ,List<Enemy> enemies , ContentManager Content , List<EnemyCollider> enemyColliders)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        ground = new Ground(50 * j, i * 50 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground3");
                        num++;
                    }
                    if (map[i][j] == '$')
                    {
                        ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground1");
                        grounds.Add(ground);
                    }
                    if (map[i][j] == '%')
                    {
                        ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("groundBase");
                        grounds.Add(ground);

                    }
                    if (map[i][j] == 'y')
                    {
                        ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground4");
                        grounds.Add(ground);

                    }
                    if (map[i][j] == 'x')
                    {
                        ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground4x");
                        grounds.Add(ground);
                    }
                    if (map[i][j] == '@')
                    {
                        item = new Items();
                        item.coinsPos = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        item.coinsTexture = Content.Load<Texture2D>("coin1");
                        items.Add(item);
                    }
                    if (map[i][j] == '?')
                    {
                        item = new Items();
                        item.coinsPos = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        item.coinsTexture = Content.Load<Texture2D>("chest1");
                        items.Add(item);
                    } 
                    //if (map[i][j] == '+')
                    //{
                    //    item = new Items();
                    //    item.injectPos = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                    //    item.injectTexture = Content.Load<Texture2D>("Health");
                    //    items.Add(item);
                    //}
                    if (map[i][j] == '!')
                    {
                        enemy = new Enemy();
                        enemy.enemyPos = new Rectangle(64 * j, i * 64, 61, 75);
                        enemy.enemyTexture = Content.Load<Texture2D>("EnemyMoving1");
                        enemies.Add(enemy);
                      
                    }
                    if (map[i][j] == '|')
                    {
                        enemyCollider = new EnemyCollider();
                        enemyCollider.ColliderPos = new Rectangle(64 * j, i * 64, 60, 60);
                        enemyColliders.Add(enemyCollider);
                    
                    }


                }
                groundAxis += 50;
            }
    
        




        }



    }
}

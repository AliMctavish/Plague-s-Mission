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
        public static List<Chest> chests = new List<Chest>();  
        public static List<Trap> traps = new List<Trap>();
        public void StartMapping(List <Ground> grounds , string[] map , List <Items> items ,List<Enemy> enemies , ContentManager Content , List<EnemyCollider> enemyColliders , Player player)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        var ground = new Ground(50 * j, i * 50 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground3");
                        num++;
                    }
                    if (map[i][j] == '$')
                    {
                        var ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground1");
                        grounds.Add(ground);
                    }
                    if (map[i][j] == '%')
                    {
                        var ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("groundBase");
                        grounds.Add(ground);

                    }
                    if (map[i][j] == 'y')
                    {
                        var ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground4");
                        grounds.Add(ground);

                    }
                    if (map[i][j] == 's')
                    {
                        player.playerPos = new Rectangle(64 * j, i * 64 + 50, 76, 98);
                    }

                    if (map[i][j] == 'x')
                    {
                        var ground = new Ground(64 * j, i * 64 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground4x");
                        grounds.Add(ground);
                    }
                    if (map[i][j] == '@')
                    {
                        var item = new Items();
                        item.position = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        item.texture = Content.Load<Texture2D>("coin1");
                        items.Add(item);
                    }
                    if (map[i][j] == '?')
                    {
                        var chest = new Chest();
                        chest.position = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        chest.texture = Content.Load<Texture2D>("chest1");
                        chests.Add(chest);
                    }
                    if (map[i][j] == '^')
                    {
                        var trap = new Trap();
                        trap.position = new Rectangle(64 * j - 35, i * 64 + 70,60,60);
                        trap.texture = Content.Load<Texture2D>("trap-export");
                        traps.Add(trap);    
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
                        var enemy = new Enemy();
                        enemy.enemyPos = new Rectangle(64 * j, i * 64, 61, 75);
                        enemy.enemyTexture = Content.Load<Texture2D>("EnemyMoving1");
                        enemies.Add(enemy);
                      
                    }
                    if (map[i][j] == '|')
                    {
                        var enemyCollider = new EnemyCollider();
                        enemyCollider.ColliderPos = new Rectangle(64 * j, i * 64, 60, 60);
                        enemyColliders.Add(enemyCollider);
                    }
                }
                groundAxis += 50;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace gravityProject
{
    internal class LevelMapper
    {
        public static List<Chest> chests = new List<Chest>();  
        public static List<Trap> traps = new List<Trap>();
        public static List<Items> Items = new List<Items>();
        public static List<Inject> injects = new List<Inject>();
        public static List<Platform> platforms = new List<Platform>();  
        public static List<Human> humans = new List<Human>();
        public static List<Enemy> enemies = new List<Enemy>();
        public static List<Effects> effects = new List<Effects>();  
        public static List<HumanEffect> humanEffects = new List<HumanEffect>();  
        public static List<Ground> grounds = new List<Ground>();
        public static List<Ladder> ladders = new List<Ladder>();
        public void StartMapping(string[] map ,ContentManager Content , List<EnemyCollider> enemyColliders , Player player)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '#')
                    {
                        var ground = new Ground(50 * j, i * 50 + 50);
                        ground.groundTexture = Content.Load<Texture2D>("ground3");
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
                        Items.Add(item);
                    }
                    if (map[i][j] == '?')
                    {
                        var chest = new Chest();
                        chest.position = new Rectangle(64 * j, i * 64 + 50, 60, 60);
                        chest.texture = Content.Load<Texture2D>("chest1");
                        chests.Add(chest);
                    }
                    if (map[i][j] == '-')
                    {
                        var platform = new Platform();
                        platform.position = new Rectangle(64 * j, i * 64 + 100 , 140,60);
                        platform.texture = Content.Load<Texture2D>("ground1");
                        platforms.Add(platform);
                    }
                    if (map[i][j] == '^')
                    {
                        var trap = new Trap();
                        trap.position = new Rectangle(64 * j - 38, i * 64 + 70,60,60);
                        trap.texture = Content.Load<Texture2D>("trap-export");
                        traps.Add(trap);    
                    }
                    if (map[i][j] == '+')
                    {
                        var inject = new Inject();
                        inject.position = new Rectangle(62 * j, i * 64 + 50, 160, 160);
                        inject.texture = Content.Load<Texture2D>("Health");
                        injects.Add(inject);
                    }
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
                    if (map[i][j] == 'h')
                    {
                        var human = new Human();    
                        human.position = new Rectangle(64 * j, i * 64, 60, 60);
                        human.texture = Content.Load<Texture2D>("sick1");
                        humans.Add(human);
                    }
                    if (map[i][j] == 'H')
                    {
                        var ladder = new Ladder();
                        ladder.position = new Rectangle(64 * j , i * 64, 60, 60);
                        ladder.texture = Content.Load<Texture2D>("Ladder");
                        ladders.Add(ladder);    
                    }
                }
            }
        }
    }
}

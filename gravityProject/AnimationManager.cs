using Microsoft.Xna.Framework;
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
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{

    
    internal class AnimationManager
    {

    
        

        public void itemsAnimation(Items[] items, ContentManager Content)
        {

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    items[i].coinsTexture = Content.Load<Texture2D>($"coin{items[i].CoinCounter}");
                    if (items[i].CoinCounter == 11 )
                    {
                     items[i].coinsTexture = Content.Load<Texture2D>("coin11");
                     items[i].CoinCounter = 1;
                    }
                    items[i].CoinCounter += 1;
                }

            }

        }  
        
        public void enemyAnimation(Enemy[] enemies, ContentManager Content)
        {

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].enemyTexture = Content.Load<Texture2D>($"EnemyMoving{enemies[i].counter}");
                    if (enemies[i].counter == 4)
                    {
                      enemies[i].enemyTexture = Content.Load<Texture2D>("EnemyMoving4");
                      enemies[i].counter = 1;
                    }
                    enemies[i].counter += 1;
                }
            }

        }

        public void playerAnimationIdle(Player player , ContentManager Content)
        {
            if (!Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.playerTexture = Content.Load<Texture2D>($"animations/playerMovement{player.PlayerAnimationCounter}");
            }
            player.PlayerAnimationCounter += 1;
            if(player.PlayerAnimationCounter == 8)
            {
                player.PlayerAnimationCounter = 1;
            }
               
        }












    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

    
        

        public void ItemsAnimation(Items[] items, float waitingTime, float Counter , ContentManager Content)
        {

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    items[i].coinsTexture = Content.Load<Texture2D>($"coin{Counter}");
                    if (waitingTime >= 1)
                    {
                        if (items[i] != null)
                        {
                            items[i].coinsTexture = Content.Load<Texture2D>("coin11");
                            Counter = 1;
                        }
                    }
                }

            }

            }
            //public Texture2D EnemyAnimation(Enemy[] enemies)
            //{

            //    return enemies;

            //}








        }
}

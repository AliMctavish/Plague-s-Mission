using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gravityProject
{
    public class Player 
    {
        public Texture2D playerTexture;

        public Rectangle playerPos;

        public bool hasJump = false;

        public int playerVelocity = 10;

        public int PlayerAnimationCounter = 1;
        public int playerAttackAnimationCounter = 1; 

        public double timePassed = 2d;

        public bool isTakingDamage = false;

        public Texture2D HealthBar;

        public Color playerColor = Color.White;

        public int playerHealth = 100;

        public bool isDead = false;
        public bool isShooting = false;
        public bool isFlipped = false;
        public bool hasSyringe = false;
        public bool isClimbing = false;
        public void Update()
        {
            
        }


        public void Draw(int flipState)
        {
            if (flipState == 0)
                Globals.spriteBatch.Draw(playerTexture, new Rectangle(playerPos.X - 34, playerPos.Y - 34, playerPos.Width, playerPos.Height), null, playerColor, 0, Vector2.Zero, SpriteEffects.None, 0);
            else
                Globals.spriteBatch.Draw(playerTexture, new Rectangle(playerPos.X - 34, playerPos.Y - 34, playerPos.Width, playerPos.Height), null, playerColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }
        public void DrawHealthBar()
        {
            for (int i = 0; i < playerHealth; i++)
            {
                if (i == 80)
                {
                    Globals.spriteBatch.Draw(HealthBar, new Rectangle(1400, 10, 40, 40), color: Color.White);
                }
                if (i == 60)
                {
                    Globals.spriteBatch.Draw(HealthBar, new Rectangle(1440, 10, 40, 40), color: Color.White);
                }
                if (i == 20)
                {
                    Globals.spriteBatch.Draw(HealthBar, new Rectangle(1480, 10, 40, 40), color: Color.White);
                }
                if (i == 5)
                {
                    Globals.spriteBatch.Draw(HealthBar, new Rectangle(1520, 10, 40, 40), color: Color.White);
                }
            }
        }
    }
}

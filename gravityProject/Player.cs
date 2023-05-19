using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace gravityProject
{
    internal class Player 
    {
        public Texture2D playerTexture;

        public Rectangle playerPos;

        public bool hasJump = false;

        public int playerVelocity = 10;

        public int PlayerAnimationCounter = 1;

        public double timePassed = 2d;

        public bool isTakingDamage = false;

        public Color playerColor = Color.White;

        public int playerHealth = 100;

        public bool isDead = false;
    }
}

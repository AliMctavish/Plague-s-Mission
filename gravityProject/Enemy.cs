using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    public class Enemy : IDisposable
    {
        public Texture2D enemyTexture;

        public Rectangle enemyPos;

        public bool enemyIsFlipped = false;

        public int counter = 1;

        public int attackCounter = 1;

        public bool isStopped = false;

        public int Health = 100;

    }
}

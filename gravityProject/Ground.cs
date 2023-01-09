using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace gravityProject
{
    internal class Ground 
    {
        public Rectangle GroundPos;
        public Texture2D groundTexture = null;


      
        

        public Ground(int posX, int posY)
        {
            
            this.GroundPos = new Rectangle(posX, posY, 64 ,64);

        }







        public void MainGround()
        {
            
         
        }
     
            




    }
}

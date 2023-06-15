using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace gravityProject
{
    public  class MainMenu
    {
        //DECLARING MOUSE POSITION
        private Rectangle mousePos;

        //DECLARING BUTTONS
        private MainMenuButtons startButton = new MainMenuButtons();
        private MainMenuButtons aboutButton = new MainMenuButtons();
        private MainMenuButtons exitButton = new MainMenuButtons();

        //DECLARING HIGHT AND WIDTH FOR THE BUTTONS
        private int hight = 100;
        private int width = 200;


        //DECLARING GLOBALS
        private ContentManager Content;
        private SpriteBatch SpriteBatch;
        
        public void init()
        {
            //INIT GLOBALS
            Content = Globals.Content;
            SpriteBatch = Globals.spriteBatch;

            //MOUSE POSITION
            mousePos = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 20, 20);

            //INIT VARIABLES
            startButton.position = new Rectangle(1400 / 2, 600 / 2, width, hight);
            startButton.texture = Content.Load<Texture2D>("start-export");

            aboutButton.position = new Rectangle(1400 / 2, 900 / 2, width, hight);
            aboutButton.texture = Content.Load<Texture2D>("about-export");

            exitButton.position = new Rectangle(1400 / 2, 1200 / 2, width, hight);
            exitButton.texture = Content.Load<Texture2D>("exit-export");
        }
        public void ButtonSelect()
        {
            this.UseBackGround();

            if (Game1.gameInfo == true)
                this.GameInfoMenu();

            this.init();

            if (mousePos.Intersects(startButton.position))
            {
                SpriteBatch.Draw(startButton.texture, startButton.position, Color.Yellow);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.gameStarted = true;
            }
            else
                SpriteBatch.Draw(startButton.texture,startButton.position, Color.White);

            if(mousePos.Intersects(aboutButton.position))
            {
                SpriteBatch.Draw(aboutButton.texture,aboutButton.position, Color.Yellow);
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.gameInfo = true;
            }
            else
                SpriteBatch.Draw(aboutButton.texture,aboutButton.position, Color.White);

            if(mousePos.Intersects(exitButton.position))
            {
                SpriteBatch.Draw(exitButton.texture, exitButton.position,Color.Yellow);
                if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.exitGame = true;
            }
            else
                SpriteBatch.Draw(exitButton.texture, exitButton.position,Color.White);


            //MOUSE CURSOR
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Health"), new Rectangle(Mouse.GetState().X + 20, Mouse.GetState().Y + 150, 160, 160), null, Color.White, 193, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void UseBackGround()
        {
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("backJana"), new Rectangle(0, 0, 1600, 900), Color.White);
        }

        public void GameInfoMenu()
        {
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("background-export"), new Rectangle(0, 0, 1600, 900), Color.White);

        }

    }
}

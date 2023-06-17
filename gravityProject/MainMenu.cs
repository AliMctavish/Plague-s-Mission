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
    public class MainMenu
    {
        //DECLARING MOUSE POSITION
        private Rectangle mousePos;

        //DECLARING BUTTONS
        private MainMenuButtons startButton = new MainMenuButtons();
        private MainMenuButtons aboutButton = new MainMenuButtons();
        private MainMenuButtons exitButton = new MainMenuButtons();
        private MainMenuButtons backButton = new MainMenuButtons();

        //DECLARING HIGHT AND WIDTH FOR THE BUTTONS
        private int hight = 90;
        private int width = 170;


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
            startButton.position = new Rectangle(1400 / 2, 880 / 2, width, hight);
            startButton.texture = Content.Load<Texture2D>("start-export");

            aboutButton.position = new Rectangle(1400 / 2, 1080 / 2, width, hight);
            aboutButton.texture = Content.Load<Texture2D>("about-export");

            exitButton.position = new Rectangle(1400 / 2, 1280 / 2, width, hight);
            exitButton.texture = Content.Load<Texture2D>("exit-export");

            backButton.position = new Rectangle(1000 / 2, 1200 / 2, width, hight);
            backButton.texture = Content.Load<Texture2D>("backButton-export");
        }
        public void ButtonSelect()
        {
            this.UseBackGround();

            this.init();

            if (mousePos.Intersects(startButton.position) && !Game1.gameInfo)
            {
                SpriteBatch.Draw(startButton.texture, startButton.position, Color.Yellow);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.gameStarted = true;
            }
            else
                SpriteBatch.Draw(startButton.texture, startButton.position, Color.White);

            if (mousePos.Intersects(aboutButton.position))
            {
                SpriteBatch.Draw(aboutButton.texture, aboutButton.position, Color.Yellow);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.gameInfo = true;
            }
            else
                SpriteBatch.Draw(aboutButton.texture, aboutButton.position, Color.White);

            if (mousePos.Intersects(exitButton.position) && !Game1.gameInfo)
            {
                SpriteBatch.Draw(exitButton.texture, exitButton.position, Color.Yellow);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.exitGame = true;
            }
            else
                SpriteBatch.Draw(exitButton.texture, exitButton.position, Color.White);

            //SCREEN TEXT
            Globals.spriteBatch.DrawString(Game1._font, "Alpha version 0.1v", new Vector2(10, 750), color: Color.White);
            Globals.spriteBatch.DrawString(Game1._font, "Created by : ali & jana", new Vector2(10, 770), color: Color.White);
            Globals.spriteBatch.DrawString(Game1._font, "music by : liana flores - rises the moon ", new Vector2(1240, 770), color: Color.White);


            if (Game1.gameInfo == true)
                this.GameInfoMenu();


            //MOUSE CURSOR
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("Health"), new Rectangle(Mouse.GetState().X + 20, Mouse.GetState().Y + 150, 160, 160), null, Color.White, 193, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void UseBackGround()
        {
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("backJana"), new Rectangle(0, 0, 1600, 900), Color.White);
            Globals.spriteBatch.Draw(Globals.Content.Load<Texture2D>("backgroundPicture-export"), new Rectangle(200, -20, 1000, 500), Color.White);
        }

        public void GameInfoMenu()
        {
            SpriteBatch.Draw(Content.Load<Texture2D>("backJanaAbout"), new Rectangle(0, 0, 1600, 900), Color.White);

            if (mousePos.Intersects(backButton.position))
            {
                SpriteBatch.Draw(backButton.texture, backButton.position, Color.Yellow);

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    Game1.gameInfo = false;
            }
            else
                SpriteBatch.Draw(backButton.texture, backButton.position, Color.White);
        }
    }
}

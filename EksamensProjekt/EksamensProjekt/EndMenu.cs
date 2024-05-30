using EksamensProjekt.DesignPatterns.ComponentPattern;
using EksamensProjekt.MapGeneration;
using EksamensProjekt.Towers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt
{
    internal class EndMenu
    {



        private Texture2D background;
        private Texture2D button;
        private Texture2D tile;
        private SpriteFont font;
        private Rectangle penguin1;
        private Rectangle penguin2;
        private Rectangle penguin3;
        private bool clicked;
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;
        private GameWorld gw;

       


        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public EndMenu(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch, GameWorld gw)
        {
            background = contentManager.Load<Texture2D>("Background 3");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("UIfont");
            tile = contentManager.Load<Texture2D>("iceTile");
            penguin1 = new Rectangle(screenWidth - button.Width - (int)(button.Width * 1.75f / 2), screenHeight / 2 - 20 - (int)(1.75f * button.Height / 2), (int)(button.Width * 2.5f), (int)(button.Height * 2.5f));
            penguin2 = new Rectangle(screenWidth - button.Width - (int)(button.Width * 1.75f / 2), screenHeight / 2 - 20 - 300 - (int)(1.75f * button.Height / 2), (int)(button.Width * 2.5f), (int)(button.Height * 2.5f));
            penguin3 = new Rectangle(screenWidth - button.Width - (int)(button.Width * 1.75f / 2), screenHeight / 2 - 20 + 300 - (int)(1.75f * button.Height / 2), (int)(button.Width * 2.5f), (int)(button.Height * 2.5f));
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;
            this.gw = gw;

        }
        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            


            // check if left button is pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {

                if (clicked == false)
                {
                    //Restart
                    if (penguin1.Contains(mouseState.Position))
                    {
                        gw = new GameWorld();
                    }
                    //Quit
                    else if (penguin2.Contains(mouseState.Position))
                    {
                        gw.Exit();
                    }

                    //else if (penguin3.Contains(mouseState.Position))
                    //{
                        
                    //}


                   
                    
                }

                clicked = true;
            }
            else
            {
                clicked = false;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {

            }


            



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(background, Vector2.Zero, Color.White);
            //spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2 - 20), null, Color.AliceBlue, 1.57079633f, new Vector2(button.Width / 2, button.Height / 2), 6, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth/2 - button.Width, screenHeight / 2 + 20), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth/2 - button.Width, screenHeight / 2 - 280), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2 + 320), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);


            spriteBatch.DrawString(font, "Restart", new Vector2(screenWidth/2 - button.Width - font.MeasureString("Restart").Length() * 1.6f / 2, screenHeight / 2 - 45), Color.White, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Quit", new Vector2(screenWidth/2 - button.Width - font.MeasureString("Quit").Length() * 1.6f / 2, screenHeight / 2 - 45 - 300), Color.White, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Total kills: " + Globals.TotalKills.ToString() + " Total Money: " + Globals.TotalMoney.ToString() + " Total Rounds: " + Globals.TotalRounds.ToString(), new Vector2(screenWidth / 2 - button.Width - font.MeasureString("Total kills: " + Globals.TotalKills.ToString() + " Total Money: " + Globals.TotalMoney.ToString() + " Total Rounds: " + Globals.TotalRounds.ToString()).Length() * 1.65f / 2, screenHeight / 2 - 45 - 400), Color.Black, 0, new Vector2(0, 0), 1.7f, SpriteEffects.None, 0);



        }

       
    }


}


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
using EksamensProjekt.Database;

namespace EksamensProjekt
{
    internal class EndMenu
    {
        private Texture2D background;
        private Texture2D button;
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
            //penguin1 = new Rectangle(screenWidth / 2 - (int)(button.Width * 1.25f), screenHeight / 2  - (int)(button.Height * 1.25f / 2), (int)(button.Width * 2f), (int)(button.Height * 2f));
            penguin2 = new Rectangle(screenWidth / 2 - (int)(button.Width * 1.25f), screenHeight / 2 - (int)(button.Height * 1.25f / 2), (int)(button.Width * 2f), (int)(button.Height * 2f));
            penguin3 = new Rectangle(screenWidth / 2 - (int)(button.Width * 1.25f), screenHeight / 2 - 280 - (int)(button.Height * 1.25f / 2), (int)(button.Width * 2f), (int)(button.Height * 2f));
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;
            this.gw = gw;
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            // Debugging output for mouse position and button rectangles
            //Console.WriteLine($"Mouse Position: {mouseState.Position}");
            //Console.WriteLine($"Penguin1 Rectangle: {penguin1}");
            //Console.WriteLine($"Penguin2 Rectangle: {penguin2}");

            if (penguin1.Contains(mouseState.Position))
            {
            }
            if (penguin2.Contains(mouseState.Position))
            {
            }

            // Check if left button is pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!clicked)
                {
                    // Restart
                    if (penguin1.Contains(mouseState.Position))
                    {
                        gw = new GameWorld();
                    }
                    // Quit
                    else if (penguin2.Contains(mouseState.Position))
                    {
                        gw.Exit();
                    }
                }
                clicked = true;
            }
            else
            {
                clicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(button, penguin1, Color.White);
            spriteBatch.Draw(button, penguin2, Color.White);

            Player player = DatabaseManager.GetPlayerStats(Globals.LoginId);
            string stats = "Total kills: " + player.TotalKills + " Total Money: " + player.TotalMoney + " Total Rounds: " + player.TotalRound;
            string stats2 = "Round kills: " + Globals.TotalKills + " Round Money: " + Globals.TotalMoney + " Round Rounds: " + Globals.TotalRounds;
            //DrawCenteredText(spriteBatch, font, "Restart", penguin1, Color.White);
            DrawCenteredText(spriteBatch, font, "Quit", penguin2, Color.White);
            //DrawCenteredText(spriteBatch, font, "Total kills: " + Globals.TotalKills.ToString() + " Total Money: " + Globals.TotalMoney.ToString() + " Total Rounds: " + Globals.TotalRounds.ToString(), penguin3, Color.Black);
            spriteBatch.DrawString(font, stats, new Vector2(penguin3.X-50-font.MeasureString(stats).X/2, penguin3.Y), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None,0);
            spriteBatch.DrawString(font, stats2, new Vector2(penguin3.X-50-font.MeasureString(stats).X/2, penguin3.Y+50), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None,0);
        }

        private void DrawCenteredText(SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle rectangle, Color color)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 position = new Vector2(
                rectangle.X + (rectangle.Width / 2) - (textSize.X / 2),
                rectangle.Y + (rectangle.Height / 2) - (textSize.Y / 2)
            );
            spriteBatch.DrawString(font, text, position, color);
        }
        
       
    }
}


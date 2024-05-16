using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt
{
    internal class BuildMenu
    {

        private Texture2D background;
        private Texture2D button;
        private SpriteFont font;
        private Rectangle penguin1;
        private Rectangle penguin2;
        private Rectangle penguin3;



        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public BuildMenu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Background");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("font");



            
        }
        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();



            // check if left button is pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {

            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(button, new Vector2(screenWidth-button.Width, screenHeight/2-20), null, Color.AliceBlue, 1.57079633f, new Vector2(button.Width / 2, button.Height / 2), 6, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2 - 20), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2 - 20-300), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            spriteBatch.Draw(button, new Vector2(screenWidth - button.Width, screenHeight / 2 - 20+300), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1.75f, SpriteEffects.None, 0);
            
            
            spriteBatch.DrawString(font, "Penguin1", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin1").Length()*1.5f / 2, screenHeight / 2 - 25), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin2", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin2").Length()*1.5f / 2, screenHeight / 2 - 25 - 300), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Penguin3", new Vector2(screenWidth - button.Width - font.MeasureString("Penguin3").Length()*1.5f / 2, screenHeight / 2 - 25 + 300), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            

            



        }



    }
}

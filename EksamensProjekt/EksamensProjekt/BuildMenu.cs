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
            spriteBatch.Draw(button, new Vector2(screenWidth/2, screenHeight/2), null, Color.White, 0, new Vector2(button.Width / 2, button.Height / 2), 1, SpriteEffects.None, 0);
            //spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);

            //spriteBatch.DrawString(font, "Registration", new Vector2(screenWidth / 2 - font.MeasureString("Registration").Length() / 2, buttonRegistration.Center.Y - 25), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //spriteBatch.DrawString(font, "Login", new Vector2(screenWidth / 2 - font.MeasureString("Login").Length() / 2, buttonLogin.Center.Y - 25), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        }



    }
}

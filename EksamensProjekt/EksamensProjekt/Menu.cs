using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Net.Mime;
//using System.Numerics;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

namespace EksamensProjekt
{
class Menu
    {
        private Texture2D background;
        private Texture2D button;
        private SpriteFont font;
        private Rectangle buttonRegistration;
        private Rectangle buttonLogin;
        private Vector2 original;

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Background");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("font");
            


          original= new Vector2( button.Width/2, button.Height/2 );
            buttonRegistration = new Rectangle (screenWidth / 2, screenHeight / 2,100,100);
            buttonLogin = new Rectangle(screenWidth / 2,screenHeight/2+100,100,100);
        }
        public void Update(GameTime gameTime)
        {
            MouseState mouseState= Mouse.GetState();

            // get mouse position
            int x= mouseState.X;
            int y= mouseState.Y;

            // check if lefr bottun is pressed
            if(mouseState.LeftButton==ButtonState.Pressed)
            {

            }

            if(mouseState.RightButton==ButtonState.Pressed)
            {

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background,Vector2.Zero,Color.White);
            spriteBatch.Draw(button, new Vector2(buttonRegistration.Center.X, buttonRegistration.Center.Y) , null, Color.White,0,original, 1,SpriteEffects.None,0);
            spriteBatch.Draw(button, new Vector2(buttonLogin.Center.X, buttonLogin.Center.Y), null, Color.White, 0, original, 1, SpriteEffects.None, 0);
          
            spriteBatch.DrawString(font, "Registration", new Vector2(screenWidth / 2 - font.MeasureString("Registration").Length()/2, buttonRegistration.Center.Y - 25), Color.White,0, new Vector2(0, 0),1,SpriteEffects.None,0);
            spriteBatch.DrawString(font,"Login", new Vector2(screenWidth / 2 - font.MeasureString("Login").Length() / 2, buttonLogin.Center.Y - 25), Color.White,0,new Vector2(0,0),1,SpriteEffects.None,0);
        }
    }
}

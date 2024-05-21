using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace EksamensProjekt.State_Pattern
{
    public class StartGame_State_Menu: I_State_Menu
    {
        private bool gameStart;
        public void Update(Menu menu, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (!menu.clicked && mouseState.LeftButton == ButtonState.Pressed)
            {

               
                    if (menu.firstButton.Contains(mouseState.Position))
                    {
                        gameStart = true;
                    }
                
            }
                if( mouseState.LeftButton != ButtonState.Pressed )
                {
                menu.clicked = false;
                }
        }
        public void Draw(Menu menu, SpriteBatch spriteBatch)
        {
            if (gameStart == false)
            {
                spriteBatch.Draw(menu.background, Vector2.Zero, Color.White);
                spriteBatch.Draw(menu.button, new Vector2(menu.firstButton.Center.X - menu.button.Width, menu.firstButton.Center.Y - menu.button.Height / 2), Color.White);
                Vector2 registrationTextPosition = new Vector2(menu.firstButton.Center.X - menu.font.MeasureString("Start").Length() - 45, menu.firstButton.Center.Y - menu.font.MeasureString("Start").Y / 2);
                spriteBatch.DrawString(menu.font, "Start", registrationTextPosition, Color.White);

            }
        }
    }
}

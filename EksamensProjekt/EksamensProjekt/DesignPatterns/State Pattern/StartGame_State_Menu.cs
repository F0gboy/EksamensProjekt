using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.MapGeneration;
using Microsoft.Xna.Framework.Input;

namespace EksamensProjekt.State_Pattern
{
    public class StartGame_State_Menu: I_State_Menu
    {
        //Vadym

        public Vector2 textPos;

        public void Update(Menu menu, GameTime gameTime)
        {
            // check if the start button is clicked
            MouseState mouseState = Mouse.GetState();
            if (!menu.clicked && mouseState.LeftButton == ButtonState.Pressed)
            {
                menu.clicked= true;
               
                if (menu.startButton.Contains(mouseState.Position))
                {
                    Globals.gameStarted = true;

                }
            }

            // check if the mouse is not pressed
            if (mouseState.LeftButton != ButtonState.Pressed)
            {
                menu.clicked = false;
            }
        }

        public void Draw(Menu menu, SpriteBatch spriteBatch)
        {
           // Draw the start button
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, 10), Color.White);
                Vector2 registrationTextPosition = new Vector2(Globals.WindowSize.X / 2 + 130 - menu.font.MeasureString("Start").Length() - 45, menu.startButton.Center.Y - menu.font.MeasureString("Start").Y / 2);
                textPos = new Vector2(registrationTextPosition.X - 49, registrationTextPosition.Y);
                spriteBatch.DrawString(menu.font, "Start", textPos, Color.White);
            
        }
    }
}

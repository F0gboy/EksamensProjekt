using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EksamensProjekt.MapGeneration;
using Microsoft.Xna.Framework.Input;

namespace EksamensProjekt.State_Pattern
{
    public class Main_State_Menu:I_State_Menu
    {
        public void Update(Menu menu, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            // check if left button is pressed
            if (!menu.clicked && mouseState.LeftButton == ButtonState.Pressed)
            {
                menu.clicked = true;
                // Registration 
                if (menu.firstButton.Contains(mouseState.Position))
                {
                    // Open game for registration
                    menu.GameState(new Registration_State_Menu());
                }
                //  Login
                else if (menu.secondButton.Contains(mouseState.Position))
                {
                   menu.GameState(new Login_State_Menu());
                }
                //Quit
                else if ( menu.thirdButton.Contains(mouseState.Position))
                {
                    Environment.Exit(0);
                }
            }
            if (mouseState.LeftButton != ButtonState.Pressed)
                menu.clicked = false;
        }
        public void Draw(Menu menu, SpriteBatch spriteBatch)
        {           
            // Draw the menu
                spriteBatch.Draw(menu.background, Vector2.Zero, Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2), Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2 + 100), Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2 + 200), Color.White);

                Vector2 registrationTextPosition = new Vector2(menu.firstButton.Center.X - menu.font.MeasureString("Registration").Length() + 80, menu.firstButton.Center.Y - menu.font.MeasureString("Registration").Y / 2);
                Vector2 loginTextPosition = new Vector2(menu.secondButton.Center.X - menu.font.MeasureString("Login").Length() + 40, menu.secondButton.Center.Y - menu.font.MeasureString("Login").Y / 2);
                Vector2 thirdButtonTextPosition = new Vector2(menu.thirdButton.Center.X - menu.font.MeasureString("Quit").Length() + 40, menu.thirdButton.Center.Y - menu.font.MeasureString("Quit").Y / 2);

                spriteBatch.DrawString(menu.font, "Registration", registrationTextPosition, Color.White);
                spriteBatch.DrawString(menu.font, "Login", loginTextPosition, Color.White);
                spriteBatch.DrawString(menu.font, "Quit", thirdButtonTextPosition, Color.White);
            
        }
    }
}

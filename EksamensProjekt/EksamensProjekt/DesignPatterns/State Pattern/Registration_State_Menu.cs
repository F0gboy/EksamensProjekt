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
    public class Registration_State_Menu : I_State_Menu
    {
        public Registration_State_Menu()
        {
        }

        public void Update(Menu menu, GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (!menu.clicked && mouseState.LeftButton == ButtonState.Pressed)
            {
                menu.clicked = true;
                // Names 
                if (menu.registration == false && menu.firstButton.Contains(mouseState.Position))
                {

                    menu.registrationTextName = "";
                    menu.stringIsActiveName = true;
                    menu.stringIsActivePassword = false;
                }
                // Passwords 
                else if (menu.secondButton.Contains(mouseState.Position))
                {
                    menu.registrationTextPassword = "";
                    menu.stringIsActiveName = false;
                    menu.stringIsActivePassword = true;
                }
                // Enter
                else if (menu.thirdButton.Contains(mouseState.Position))
                {
                    if (!string.IsNullOrEmpty(menu.stringName.ToString()) && !string.IsNullOrEmpty(menu.stringPassword.ToString()))
                    {
                        if (Database.DatabaseManager.RegisterUser(menu.stringName, menu.stringPassword) == true)
                        {
                            menu.GameState(new StartGame_State_Menu());

                            menu.gameStart = true;
                        }

                        menu.gameStart = true;
                    }

                }
            }


            if (mouseState.LeftButton != ButtonState.Pressed)
                menu.clicked = false;

            menu.currentKeyboardState = Keyboard.GetState();
            if (menu.currentKeyboardState.GetPressedKeyCount() > 0 && !menu.keyboardPressed)
            {
                menu.keyboardPressed = true;
                if (menu.stringIsActiveName)
                    menu.HandleInput(ref menu.stringName);//ref
                else if (menu.stringIsActivePassword)
                    menu.HandleInput(ref menu.stringPassword);//ref
            }
            else if (menu.currentKeyboardState.GetPressedKeyCount() < 1)
                menu.keyboardPressed = false;

            menu.previousKeyboardState = menu.currentKeyboardState;
        }

        public void Draw(Menu menu, SpriteBatch spriteBatch)
        {
            if (menu.registration == false)
            {

                spriteBatch.Draw(menu.background, Vector2.Zero, Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2), Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2 + 100), Color.White);
                spriteBatch.Draw(menu.button, new Vector2(Globals.WindowSize.X / 2 - 95, Globals.WindowSize.Y / 2 + 200), Color.White);

                Vector2 registrationTextPosition = new Vector2(menu.firstButton.Center.X - menu.font.MeasureString(menu.registrationTextName).Length() + 45, menu.firstButton.Center.Y - menu.font.MeasureString("Registration").Y / 2);
                Vector2 loginTextPosition = new Vector2(menu.secondButton.Center.X - menu.font.MeasureString(menu.registrationTextPassword).Length() + 70, menu.secondButton.Center.Y - menu.font.MeasureString("Login").Y / 2);
                Vector2 thirdButtonTextPosition = new Vector2(menu.thirdButton.Center.X - menu.font.MeasureString("Save").Length() + 40, menu.thirdButton.Center.Y - menu.font.MeasureString("Save").Y / 2);

                spriteBatch.DrawString(menu.font, menu.registrationTextName, registrationTextPosition, Color.White);
                spriteBatch.DrawString(menu.font, menu.registrationTextPassword, loginTextPosition, Color.White);
                spriteBatch.DrawString(menu.font, "Save", thirdButtonTextPosition, Color.White);

                // Users input
                Vector2 textName = new Vector2(menu.firstButton.Center.X - menu.font.MeasureString("Name").Length() + 50, menu.firstButton.Center.Y - menu.font.MeasureString("Name").Y / 2);
                Vector2 textPassword = new Vector2(menu.secondButton.Center.X - menu.font.MeasureString("Password").Length() + 90, menu.secondButton.Center.Y - menu.font.MeasureString("Login").Y / 2);

                spriteBatch.DrawString(menu.font, "" + menu.stringName.ToString(), textName, Color.White);
                spriteBatch.DrawString(menu.font, "" + menu.stringPassword.ToString(), textPassword, Color.White);

            }

        }
    }
}
    

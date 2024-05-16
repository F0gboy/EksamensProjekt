using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
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
        private SpriteFont secondFont;
        private Rectangle firstButton;
        private Rectangle secondButton;
        private Rectangle thirdButton;
        private string registrationTextName="Name";
        private string registrationTextPassword="Password";


        private Vector2 original;
        private bool login;
        private bool gameStart;
        private bool clicked;
        private bool registration;
        private bool stringIsActiveName;
        private bool stringIsActivePassword;
        private bool keyboardPressed;
        private bool checkLogin;
     
        StringBuilder stringName= new StringBuilder();
        StringBuilder stringPassword= new StringBuilder();
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;



        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Background");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("font");
            secondFont = contentManager.Load<SpriteFont>("secondFont");


           // original= new Vector2( button.Width/2, button.Height/2 );
            firstButton = new Rectangle (screenWidth / 2, screenHeight / 2,button.Width,button.Height);
            secondButton = new Rectangle(screenWidth / 2,screenHeight/2+100, button.Width, button.Height);
            thirdButton= new Rectangle(screenWidth/2, screenHeight/2+200, button.Width, button.Height);
         
        }
        public void Update(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();
            // check if left button is pressed
            if (clicked == false && mouseState.LeftButton == ButtonState.Pressed)
            {
                clicked = true;
                // Registration 
                if (login == false && firstButton.Contains(mouseState.Position))
                {
                    // Open game for registration
                    login = true;        
                }
                //  Login
                else if (login == false  && secondButton.Contains(mouseState.Position))
                {



                    registration = true;
                    login = true;
                    //checkLogin = false;
                }
                //Quit
                else if (login == false && thirdButton.Contains(mouseState.Position))
                {
                    Environment.Exit(0);
                }
                // Names 
                else if (registration== false && firstButton.Contains(mouseState.Position) || checkLogin == false && firstButton.Contains(mouseState.Position))
                {
                    registrationTextName = "";
                    stringIsActiveName = true;
                    stringIsActivePassword = false;
                }
                // Passwords 
                else if (registration == false && firstButton.Contains(mouseState.Position) || checkLogin == false && secondButton.Contains(mouseState.Position))
                {
                    registrationTextPassword = "";
                    stringIsActiveName = false;
                    stringIsActivePassword = true;
                }
                // Save
                else if (registration == false && thirdButton.Contains(mouseState.Position))
                {
                    // Logic for the database



                    if( ! string.IsNullOrWhiteSpace(stringName.ToString()) && ! string.IsNullOrWhiteSpace(stringPassword.ToString()))
                        {

                        checkLogin = true;
                        registration = true;

                    }
                }
                // Enter
                else if (   checkLogin== false && thirdButton.Contains(mouseState.Position))
                {
                    // Logic for the database


                    if (!string.IsNullOrWhiteSpace(stringName.ToString()) && !string.IsNullOrWhiteSpace(stringPassword.ToString()))
                    {

                    checkLogin = true;

                    }
                    
                }

                // Start
                else if (firstButton.Contains(mouseState.Position))
                {
                    gameStart = true;
                }
            }
            if (mouseState.LeftButton != ButtonState.Pressed)
            {
                clicked = false;
            }

            currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.GetPressedKeyCount() > 0 && keyboardPressed == false)
            {
                keyboardPressed = true;
                // Names string is active
                if (stringIsActiveName == true)
                {

                    HandleInput(stringName);

                }
                // Passwords string is active
                else if (stringIsActivePassword == true)
                {
                   
                    HandleInput(stringPassword);

                }
            }
            else if (currentKeyboardState.GetPressedKeyCount() < 1) 
            {
                keyboardPressed = false;
            }


            
        }
        private void HandleInput(StringBuilder input)
        {
            currentKeyboardState = previousKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Keys[] keys= currentKeyboardState.GetPressedKeys();

            foreach( Keys key in keys)
            {
                if (previousKeyboardState.IsKeyUp(key))
                {
                    if (key == Keys.Back && input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                    }
                    else if (key >= Keys.A && key <= Keys.Z) 
                    {
                        input.Append(key.ToString().ToLower());
                    }
                    else if (key>= Keys.D0 && key <= Keys.D9)
                    {
                        input.Append(key.ToString().Substring(1));
                    }
                    else if (key == Keys.Space)
                    {
                        input.Append(' ');
                    }
                }
            }



        }
        public void Draw(SpriteBatch spriteBatch)
        {
           
            if( login == false )
            {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(button, new Vector2(firstButton.Center.X - button.Width, firstButton.Center.Y - button.Height / 2), Color.White);
            spriteBatch.Draw(button, new Vector2(secondButton.Center.X - button.Width , secondButton.Center.Y - button.Height / 2), Color.White);
            spriteBatch.Draw(button, new Vector2(thirdButton.Center.X - button.Width, thirdButton.Center.Y - button.Height / 2), Color.White);

            Vector2 registrationTextPosition = new Vector2(firstButton.Center.X - font.MeasureString("Registration").Length() - 15, firstButton.Center.Y - font.MeasureString("Registration").Y / 2);
            Vector2 loginTextPosition = new Vector2(secondButton.Center.X - font.MeasureString("Login").Length() -55, secondButton.Center.Y - font.MeasureString("Login").Y / 2);
            Vector2 thirdButtonTextPosition = new Vector2(thirdButton.Center.X - font.MeasureString("Quit").Length() - 50, thirdButton.Center.Y - font.MeasureString("Quit").Y / 2);

            spriteBatch.DrawString(font, "Registration", registrationTextPosition, Color.White);
            spriteBatch.DrawString(font, "Login", loginTextPosition, Color.White);
            spriteBatch.DrawString(font, "Quit", thirdButtonTextPosition, Color.White);

            }
            else if (registration == false)
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.Draw(button, new Vector2(firstButton.Center.X - button.Width, firstButton.Center.Y - button.Height / 2), Color.White);
                spriteBatch.Draw(button, new Vector2(secondButton.Center.X - button.Width, secondButton.Center.Y - button.Height / 2), Color.White);
                spriteBatch.Draw(button,new Vector2 (thirdButton.Center.X - button.Width, thirdButton.Center.Y-button.Height / 2), Color.White);

                Vector2 registrationTextPosition = new Vector2(firstButton.Center.X - font.MeasureString("Name").Length() - 45, firstButton.Center.Y - font.MeasureString("Registration").Y / 2);
                Vector2 loginTextPosition = new Vector2(secondButton.Center.X - font.MeasureString("Password").Length() - 25, secondButton.Center.Y - font.MeasureString("Login").Y / 2);
                Vector2 thirdButtonTextPosition= new Vector2( thirdButton.Center.X- font.MeasureString("Save").Length()-50, thirdButton.Center.Y - font.MeasureString("Save").Y/2);

                spriteBatch.DrawString(font, registrationTextName, registrationTextPosition, Color.White);
                spriteBatch.DrawString(font, registrationTextPassword, loginTextPosition, Color.White);
                spriteBatch.DrawString(font, "Save", thirdButtonTextPosition, Color.White);

              // Users input
                Vector2 textName = new Vector2(firstButton.Center.X - font.MeasureString("Name").Length() - 45, firstButton.Center.Y - font.MeasureString("Name").Y / 2);
                Vector2 textPassword = new Vector2(secondButton.Center.X - font.MeasureString("Password").Length() - 25, secondButton.Center.Y - font.MeasureString("Login").Y / 2);

                spriteBatch.DrawString(secondFont, "" + stringName.ToString(), textName, Color.White);
                spriteBatch.DrawString(secondFont, "" + stringPassword.ToString(), textPassword, Color.White);

            }
            else if (checkLogin == false)
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.Draw(button, new Vector2(firstButton.Center.X - button.Width, firstButton.Center.Y - button.Height / 2), Color.White);
                spriteBatch.Draw(button, new Vector2(secondButton.Center.X - button.Width, secondButton.Center.Y - button.Height / 2), Color.White);
                spriteBatch.Draw(button, new Vector2(thirdButton.Center.X - button.Width, thirdButton.Center.Y - button.Height / 2), Color.White);

                Vector2 registrationTextPosition = new Vector2(firstButton.Center.X - font.MeasureString("Name").Length() - 45, firstButton.Center.Y - font.MeasureString("Registration").Y / 2);
                Vector2 loginTextPosition = new Vector2(secondButton.Center.X - font.MeasureString("Password").Length() - 25, secondButton.Center.Y - font.MeasureString("Login").Y / 2);
                Vector2 thirdButtonTextPosition = new Vector2(thirdButton.Center.X - font.MeasureString("Enter").Length() - 50, thirdButton.Center.Y - font.MeasureString("Enter").Y / 2);

                spriteBatch.DrawString(font, registrationTextName, registrationTextPosition, Color.White);
                spriteBatch.DrawString(font, registrationTextPassword, loginTextPosition, Color.White);
                spriteBatch.DrawString(font, "Enter", thirdButtonTextPosition, Color.White);

                // Users input
                Vector2 textName = new Vector2(firstButton.Center.X - font.MeasureString("Name").Length() - 45, firstButton.Center.Y - font.MeasureString("Name").Y / 2);
                Vector2 textPassword = new Vector2(secondButton.Center.X - font.MeasureString("Password").Length() - 25, secondButton.Center.Y - font.MeasureString("Login").Y / 2);

                spriteBatch.DrawString(secondFont, "" + stringName.ToString(), textName, Color.White);
                spriteBatch.DrawString(secondFont, "" + stringPassword.ToString(), textPassword, Color.White);

            }

            else if (gameStart == false ) 
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
                spriteBatch.Draw(button, new Vector2(firstButton.Center.X - button.Width, firstButton.Center.Y - button.Height / 2), Color.White);
                Vector2 registrationTextPosition = new Vector2(firstButton.Center.X - font.MeasureString("Start").Length() -45, firstButton.Center.Y - font.MeasureString("Start").Y / 2);
                spriteBatch.DrawString(font, "Start", registrationTextPosition, Color.White);

            }
      

        }
    }
}

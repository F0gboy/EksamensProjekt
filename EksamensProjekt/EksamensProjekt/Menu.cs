using EksamensProjekt.State_Pattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;


namespace EksamensProjekt
{
public class Menu
    {
        private I_State_Menu currentState;
        public Texture2D background;
        public Texture2D button;
        public SpriteFont font;
        public SpriteFont secondFont;
        public Rectangle firstButton;
        public Rectangle secondButton;
        public Rectangle thirdButton;
        public string registrationTextName="Name";
        public string registrationTextPassword="Password";
        public bool login;
        public bool gameStart;
        public bool clicked;
        public bool registration;
        public bool stringIsActiveName;
        public bool stringIsActivePassword;
        public bool keyboardPressed;
        public bool checkLogin;
        public StringBuilder stringName= new StringBuilder();
        public StringBuilder stringPassword= new StringBuilder();
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState mouseState = Mouse.GetState();

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Background");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("font");
            secondFont = contentManager.Load<SpriteFont>("secondFont");
            
            firstButton = new Rectangle (screenWidth / 2, screenHeight / 2,button.Width,button.Height);
            secondButton = new Rectangle(screenWidth / 2,screenHeight/2+100, button.Width, button.Height);
            thirdButton= new Rectangle(screenWidth/2, screenHeight/2+200, button.Width, button.Height);
            currentState =new Main_State_Menu();
        }
        public void GameState (I_State_Menu newState)
        {
            currentState= newState;
        }
        public void Update(GameTime gameTime)
        {
            
            currentState.Update(this,gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
          currentState.Draw(this, spriteBatch);
        }
        public void HandleInput(StringBuilder input)
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
    }
}

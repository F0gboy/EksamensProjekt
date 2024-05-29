using EksamensProjekt.State_Pattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text;
using EksamensProjekt.MapGeneration;


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
        public Rectangle startButton;
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
        public string stringName = "";
        public string stringPassword = "";
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState mouseState = Mouse.GetState();

        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        public Menu(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            background = contentManager.Load<Texture2D>("Background 3");
            button = contentManager.Load<Texture2D>("Button");
            font = contentManager.Load<SpriteFont>("UIfont");
            secondFont = contentManager.Load<SpriteFont>("secondFont");
            
            firstButton = new Rectangle(Globals.WindowSize.X / 2 - 100, Globals.WindowSize.Y / 2,button.Width,button.Height);
            secondButton = new Rectangle(Globals.WindowSize.X / 2 - 100,Globals.WindowSize.Y /2+100, button.Width, button.Height);
            thirdButton= new Rectangle(Globals.WindowSize.X / 2 - 100, Globals.WindowSize.Y /2+200, button.Width, button.Height);
            startButton= new Rectangle(Globals.WindowSize.X / 2 - 100,10, button.Width, button.Height);
            currentState =new Main_State_Menu();
        }
        public void GameState (I_State_Menu newState)
        {
            currentState= newState;
        }
        public void Update(GameTime gameTime)
        {

            currentState.Update(this, gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
          currentState.Draw(this, spriteBatch);
        }

        public void HandleInput(ref string input)
        {
            currentKeyboardState = Keyboard.GetState();

            Keys[] keys = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in keys)
            {
                if (previousKeyboardState.IsKeyUp(key))
                {
                    if (key == Keys.Back && input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1, 1);
                    }
                    else if (key >= Keys.A && key <= Keys.Z)
                    {
                        input += key.ToString().ToLower();
                    }
                    else if (key >= Keys.D0 && key <= Keys.D9)
                    {
                        input += key.ToString().Substring(1);
                    }
                    else if (key == Keys.Space)
                    {
                        input += ' ';
                    }
                }
            }
            previousKeyboardState = currentKeyboardState;
        }
    }
}

using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EksamensProjekt
{
    public static class InputManager
    {
        //Jasper

        private static MouseState _lastMouseState;
        public static bool MouseClicked { get; private set; }
        public static bool MouseRightClicked { get; private set; }
        public static Rectangle MouseRectangle { get; private set; }

        public static void Update()
        {
            var mouseState = Mouse.GetState();

            // Check if the mouse is clicked
            MouseClicked = mouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released; 
            MouseRightClicked = mouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released;
            MouseRectangle = new(mouseState.Position.X, mouseState.Position.Y, 1, 1);

            _lastMouseState = mouseState;
        }
    }
}

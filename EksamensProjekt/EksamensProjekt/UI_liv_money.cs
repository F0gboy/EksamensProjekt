﻿using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace EksamensProjekt
{
    public class UI_liv_money
    {
        private Texture2D fishTexture;
        private Texture2D heartTexture;
        private Rectangle fishPosition;
        private Rectangle heartPosition;
        private int life=100;
        private SpriteFont UIfont;
        public UI_liv_money(/*GraphicsDevice graphicsDevice,*/ ContentManager contentManager) 
        {
            fishTexture = contentManager.Load<Texture2D>("Skærmbillede 2024-05-21 115214");
            fishPosition = new Rectangle(  10, 10, 80, 80);
            heartTexture = contentManager.Load<Texture2D>("heart");
            heartPosition = new Rectangle(250, 10, 80, 80);
            UIfont = contentManager.Load<SpriteFont>("UIfont");
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fishTexture, fishPosition,Color.White);
            spriteBatch.Draw(heartTexture, heartPosition,Color.White);
            spriteBatch.DrawString(UIfont, life.ToString(),new Vector2(320,50), Color.Black);
        }
        public void Life() {life--;}
    }
}
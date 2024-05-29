using EksamensProjekt.DesignPatterns.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using EksamensProjekt.MapGeneration;

namespace EksamensProjekt
{
    public class UI_liv_money
    {
        private Texture2D fishTexture;
        private Texture2D heartTexture;
        private Rectangle fishPosition;
        private Rectangle heartPosition;
        private SpriteFont UIfont;
        private Vector2 fontScale;
        public UI_liv_money(/*GraphicsDevice graphicsDevice,*/ ContentManager contentManager) 
        {
            fishTexture = contentManager.Load<Texture2D>("Skærmbillede 2024-05-21 115214");
            fishPosition = new Rectangle(  10, 10, 80, 80);
            heartTexture = contentManager.Load<Texture2D>("heart");
            heartPosition = new Rectangle(250, 10, 80, 80);
            UIfont = contentManager.Load<SpriteFont>("UIfont");
            fontScale = new Vector2(1.8f, 1.8f);
            Globals.life = 1;
            Globals.money = 150;
        }
        public void Update(GameTime gameTime)
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(fishTexture, fishPosition,Color.White);
            spriteBatch.DrawString(UIfont, Globals.life.ToString(), new Vector2(350, 25), Color.Black, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(UIfont, Globals.money.ToString(), new Vector2(120, 25), Color.Black, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
            spriteBatch.Draw(heartTexture, heartPosition,Color.White);

        }
        public void Life() {Globals.life--;}
    }
}

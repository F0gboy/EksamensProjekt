using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.Towers
{
    internal class BasicPenguin : GameObject
    {
        public Texture2D sael;
        private GraphicsDevice graphicsDevice; 
        private ContentManager contentManager; 
        private SpriteBatch spriteBatch;
        public Vector2 pos { get; private set; }
        public BasicPenguin(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch) : base(graphicsDevice, contentManager, spriteBatch)
        {
            MouseState mouseState = Mouse.GetState();
            sael = contentManager.Load<Texture2D>("sæl");
            pos = new Vector2(mouseState.Position.X-sael.Width/2, mouseState.Position.Y-sael.Height/2);
            this.graphicsDevice = graphicsDevice;
            this.contentManager = contentManager;
            this.spriteBatch = spriteBatch;
            rect = new Rectangle((int)pos.X, (int)pos.Y, sael.Width, sael.Height);



        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sael, pos, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

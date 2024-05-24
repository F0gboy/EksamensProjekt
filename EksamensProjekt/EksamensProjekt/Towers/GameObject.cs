using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace EksamensProjekt.Towers
{
    abstract class GameObject
    {

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice; 
        private ContentManager contentManager;
        public Rectangle rect;



        public GameObject(GraphicsDevice graphicsDevice, ContentManager contentManager, SpriteBatch spriteBatch) 
        { 
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;   
            this.contentManager = contentManager;

        }

        abstract public void Update(GameTime gameTime);
        abstract public void Draw(SpriteBatch spriteBatch);
    }
}

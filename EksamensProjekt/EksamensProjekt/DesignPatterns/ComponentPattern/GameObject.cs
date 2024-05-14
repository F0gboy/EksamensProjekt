using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjekt.DesignPatterns.ComponentPattern
{
    public class GameObject
    {
        private SpriteRenderer spriteRenderer; // SpriteRenderer component for drawing

        public GameObject(Game game, Texture2D texture, Vector2 position)
        {
            spriteRenderer = new SpriteRenderer(game, texture, position);
        }

        public void Update(GameTime gameTime)
        {
            // Update logic goes here
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteRenderer.Draw(gameTime, spriteBatch);
        }
    }
}
